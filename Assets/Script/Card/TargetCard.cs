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
    public string targetTag;
    public int reinforcelevel;
    public int cost;
    public bool isdiscarding;
    State currentState;
    Collider2D collider;
    Vector3 point;
    [SerializeField]
    SpriteRenderer attackcardsprite;
    public SAInformation saInformation;
    public Player player;
    public PRS originPRS;//원래의 위치로 되돌아 가도록

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
        reinforcelevel = card.reinforcelevel;
        cost = card.cost;
        isdiscarding = card.isdiscarding;
        attackcardsprite.sprite = card.sprite;
        saInformation = new SAInformation(card.target, card.turn, card.category, card.amount);
    }

    [SerializeField]
    private LineRenderer visualizerLine;

    enum State
    {
        Idle,
        Hover,
        Select,
        Drag,
        Apply
    }
    // Start is called before the first frame update
    void Start()
    {
        description.enabled = true;
        collider = GetComponent<Collider2D>();
        currentState = State.Idle;
    }
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
                    if(Input.GetMouseButtonUp(0))
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
                    if(checkCardBoundary())
                    {
                        currentState = State.Drag;
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

                    if(detectEnemy(point))
                    {
                        currentState=State.Apply; 
                        break;
                    }

                }
                break;
            case State.Apply:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("mouse on");
        }
        else if (currentState == State.Select)
        {
            //button.SetActive(true);
            //description.enabled = false;
        }
        else if (currentState == State.Drag)
        {
            calculate();
           
        }
        else if (currentState == State.Apply)
        {
            //StatusAdjustment(Card,saInformation)//(적용대상과,적용되야하는 값 정보) CATEGORY (DEFENSE,ATTACK,WEAKNESS,
            StatusAdjustment.SetFunction(player, saInformation);
            CardManager.Inst.SwapPop(this);
            CardManager.Inst.CardAlignment();
            Destroy(this.gameObject);
        }
    }

    bool detectEnemy(Vector2 mousePoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePoint, transform.forward, 200f);
        for(int i=0;i<hits.Length; i++)
        {
            if (hits[i].collider.gameObject.layer == 6)
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
        //Ray ray = Camera.main.ScreenPointToRay(mousepoint);
        //RaycastHit2D hit= Physics2D.Raycast(mousepoint, transform.forward, 100f);
        //if(hit)
        //{
        //    if (hit.collider.gameObject.layer == 6)
        //    {
        //        CardManager.Inst.LagerCard(hit.collider.gameObject.GetComponent<AttackCard>());
        //        return true;
        //    }
        //    return false;
        //}
        //return false;
        if (collider.OverlapPoint(mousepoint))
        {
            //CardManager.Inst.istriggered
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
