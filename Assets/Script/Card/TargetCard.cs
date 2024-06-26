using DG.Tweening;
using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class TargetCard : MonoBehaviour,CardInterface
{
    
    public TextMeshPro description;
    public TextMeshPro cardnname;
    int rarity;
    string targetTag;
    int reinforcelevel;
    int cost;
    bool isdiscarding;
    string type;
    string mode;
    State currentState;
    Collider2D collider;
    Vector3 point;
    [SerializeField]
    SpriteRenderer cardsprite;
    public SAInformation saInformation;
    Player player;
    public PRS originPRS;//원래의 위치로 되돌아 가도록
    int curturn;
    public EnemyBase enemy;
    //public GameObject dot;
    //GameObject []dots;
    //public int numberOfDot;
    //public float spaceBetweenPoints;
    public void MoveTransform(PRS prs,bool useDotween, float dotweenTime=0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot,dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    
    public void setUp(Card card)
    {
        description.text = card.description.ToString();
        cardnname.text = card.name.ToString();
        rarity = card.rarity;
        targetTag = card.targetTag;
        type = card.type;
        mode = card.mode;
        reinforcelevel = card.reinforcelevel;
        cost = card.cost;
        isdiscarding = card.isdiscarding;
        cardsprite.sprite = card.sprite;
        saInformation = new SAInformation(card.target, card.turn, card.category, card.amount);
    }
    public Card getCard()
    {
        Card card = new Card(cardsprite.sprite, cardnname.text, description.text, rarity, targetTag, saInformation.target, type, saInformation.turn, saInformation.category, saInformation.amount, reinforcelevel, cost, isdiscarding, mode);
        return card;
    }

    [SerializeField]
    private LineRenderer visualizerLine;

    enum State
    {
        Idle,
        Hover,
        Select,
        Drag,
        Apply,
        UnVisibleApply,
        Rest,
        Destroyed
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        description.enabled = true;
        collider = GetComponent<Collider2D>();
        currentState = State.Idle;
    }
    public void SetStateDestroy() {  currentState = State.Destroyed; }
    public void handleinput()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    if (isInside(point))
                        currentState = State.Hover;
                }
                break;
            case State.Hover:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        currentState = State.Select;
                        break;
                    }
                    if (!isInside(point))
                    {
                        currentState = State.Idle;
                        CardManager.Inst.SmallerCard(this);
                        break;
                    }
                }
                break;
            case State.Select:
                {
                    if (CardManager.Inst.GetEnergy() < cost)
                    {
                        Debug.Log("에너지가 부족합니다");
                        currentState = State.Idle;
                        break;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        currentState = State.Idle;
                        CardManager.Inst.SmallerCard(this);
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        currentState = State.Idle;
                        CardManager.Inst.SmallerCard(this);
                        //dotsVisibleSetting(0,numberOfDot, false);
                        break;
                    }
                    if(!checkCardBoundary())
                    {
                        currentState = State.Drag;
                        Vector3 cardlt = CardManager.Inst.CardLeft.position;
                        Vector3 cardrt = CardManager.Inst.CardRight.position;
                        MoveTransform(new PRS(new Vector3((cardlt.x + cardrt.x) / 2, 1.5f, -2), Quaternion.identity, Vector3.one * 1.7f), true, 0.5f);
                        break;
                    }
                }
                break;
            case State.Drag:
                {
                    
                    if (Input.GetMouseButtonDown(1))
                    {
                        currentState = State.Idle;
                        CardManager.Inst.SmallerCard(this);
                        //dotsVisibleSetting(0,numberOfDot, false);
                        break;
                    }

                    if(Input.GetMouseButtonUp(0))
                    {
                        if (detectEnemy(point))
                        {
                            currentState = State.Apply;
                            break;
                        }
                        currentState=State.Idle;
                        CardManager.Inst.SmallerCard(this);
                        break;
                    }

                    

                }
                break;
            case State.Apply:
                {
                    if(saInformation.turn==0)
                    {
                        currentState=State.Destroyed; 
                        break;
                    }
                    else 
                    {
                        //saInformation.turn -= 1;
                        currentState = State.Rest;
                        break;
                    }
                    //currentState = State.Destroyed;
                }

            case State.Rest: 
                {
                    if(curturn!=CardManager.Inst.turnManager.GetTurnCount()) 
                    { 
                        currentState=State.UnVisibleApply; break;
                    }
                }break;
            case State.UnVisibleApply: 
                {
                    if(saInformation.turn==0)
                    {
                        currentState = State.Destroyed;
                        break;
                    }
                    else
                        currentState = State.Rest;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Destroyed) { Destroy(this.gameObject); }

        //Debug.Log(currentState);
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        handleinput();
        if (currentState == State.Idle)
        {
            //CardManager.Inst.SmallerCard(this);
            visualizerLine.enabled = false;
            //button.SetActive(false);
            
        }
        else if (currentState == State.Hover)
        {
            CardManager.Inst.LagerCard(this);
            //Debug.Log("mouse on");
        }
        else if (currentState == State.Select)
        {
            
            //button.SetActive(true);
            //description.enabled = false;
        }
        else if (currentState == State.Drag)
        {
            //카드가 중앙으로 이동하도록
            
            calculate();
           
        }
        else if (currentState == State.Apply)
        {
            //StatusAdjustment(Card,saInformation)//(적용대상과,적용되야하는 값 정보) CATEGORY (DEFENSE,ATTACK,WEAKNESS,
            StatusAdjustment.SetFunction(this,enemy, saInformation,player,true);
            CardManager.Inst.PlusEnergy(-cost);
            CardManager.Inst.SwapPop(this);
            CardManager.Inst.CardAlignment();
            if (isdiscarding)
                CardManager.Inst.GotoBurnPile(this); 
            else
                CardManager.Inst.GotoDiscardPile(this);
            CardManager.Inst.RemoveHandofCard(this);
            //시작 턴과 현재 턴의 차이가 amount일때 효과가 취소되도록 설정.
            //saInformation.turn -= 1;
            //턴 차이가 -1이라면 visible 처리만 안보이게
            curturn=CardManager.Inst.turnManager.GetTurnCount();
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<LineRenderer>().enabled = false;
            description.enabled = false;
        }
        else if (currentState == State.Rest) 
        {
            curturn = CardManager.Inst.turnManager.GetTurnCount();
        }
        else if (currentState == State.UnVisibleApply) 
        {
            saInformation.turn -= 1;
            StatusAdjustment.SetFunction(this, enemy, saInformation, player,false);
        }
        else if (currentState == State.Destroyed) { Destroy(this.gameObject); }
    }

    bool detectEnemy(Vector2 mousePoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePoint, transform.forward, 400f);
        for(int i=0;i<hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == "Enemy")
            {           
                enemy = hits[i].collider.gameObject.GetComponent<EnemyBase>();
                return true;
            }
            return false;
        }

        return false;
    }
    bool isInside(Vector2 mousepoint)
    {
        if (collider.OverlapPoint(mousepoint))
        {
            return true;
        }
        return false;
    }
    bool checkCardBoundary()
    {
        RaycastHit2D hit = Physics2D.Raycast(point, Vector3.forward,200f,LayerMask.GetMask("CardArea"));
        if(hit)
        {
            return true;
        }
        return false;
    }

    public void applyEvent()
    {
        currentState = State.Drag;
    }

    public void cancleEvent()
    {
        currentState = State.Idle;
    }

    void calculate()
    {
        Vector3 directionnormal = (new Vector3(point.x,point.y,0) - new Vector3(transform.position.x,transform.position.y,0)).normalized;
        int count = 0;
        while ((directionnormal * count).sqrMagnitude <= (new Vector3(point.x,point.y,0) - new Vector3(transform.position.x, transform.position.y, 0)).sqrMagnitude)
        {
            count++;
        }
        visualizerLine.enabled = true;
        visualizerLine.positionCount = count;
        for (int i = 0; i < count; i++)
        {
            visualizerLine.SetPosition(i, transform.position + directionnormal * i);
        }

    }
}
