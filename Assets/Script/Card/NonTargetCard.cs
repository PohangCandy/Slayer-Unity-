using DG.Tweening;
using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonTargetCard : MonoBehaviour,CardInterface
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
    SpriteRenderer  cardsprite;
    public SAInformation saInformation;
    Player player;
    public PRS originPRS;//원래의 위치로 되돌아 가도록

    //public GameObject dot;
    //GameObject []dots;
    //public int numberOfDot;
    //public float spaceBetweenPoints;
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
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


    enum State
    {
        Idle,
        Hover,
        Select,
        Drag,
        Apply,
        Destroyed
    }
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player").GetComponent<Player>();
        description.enabled = true;
        collider = GetComponent<Collider2D>();
        currentState = State.Idle;
    }
    public void SetStateDestroy() { currentState = State.Destroyed; }
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
                    if (checkCardBoundary())
                    {
                        currentState = State.Drag;
                        break;
                    }
                }
                break;
            case State.Drag:
                {
                    if (CardManager.Inst.GetEnegy() < cost)
                    {
                        Debug.Log("에너지가 부족합니다");
                        currentState = State.Idle;
                        break;
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        currentState = State.Idle;
                        CardManager.Inst.SmallerCard(this);
                        //dotsVisibleSetting(0,numberOfDot, false);
                        break;
                    }
                    if(Input.GetMouseButtonUp(0))
                    {
                        currentState= State.Apply;
                        break;
                    }

                }
                break;
            case State.Apply:
                {
                    if(saInformation.turn==0)
                    currentState = State.Destroyed;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Destroyed) { Destroy(this.gameObject); }
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        handleinput();
        if (currentState == State.Idle)
        {
            //CardManager.Inst.SmallerCard(this);

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
            //calculate();
            transform.position = point;
        }
        else if (currentState == State.Apply)
        {
            //StatusAdjustment(Card,saInformation)//(적용대상과,적용되야하는 값 정보) CATEGORY (DEFENSE,ATTACK,WEAKNESS,
            StatusAdjustment.SetFunction(this,player, saInformation,player);
            //CardManager.Inst.CardAlignment();
            //int index = CardManager.Inst.GetHandOfCard().FindIndex(card => card == this);//람다 방정식
            //if(index == CardManager.Inst.GetHandOfCard().Count) 
            //{
            //    CardManager.Inst.GetHandOfCard().RemoveAt(index);
            //}
            //else
            //{
            //    CardManager.Inst.GetHandOfCard()[index]=
            //}
            CardManager.Inst.PlusEnergy(-cost);
            CardManager.Inst.SwapPop(this);
            CardManager.Inst.CardAlignment();
            if (isdiscarding)
                CardManager.Inst.GotoBurnPile(this);
            else
                CardManager.Inst.GotoDiscardPile(this);

            CardManager.Inst.RemoveHandofCard(this);
            
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if(currentState==State.Destroyed) { Destroy(this.gameObject); }
        
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
        RaycastHit2D hit = Physics2D.Raycast(point, Vector3.forward, 200f, LayerMask.GetMask("CardArea"));
        if (hit)
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


}

