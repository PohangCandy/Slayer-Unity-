using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackCard : MonoBehaviour
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

    //public GameObject dot;
    //GameObject []dots;
    //public int numberOfDot;
    //public float spaceBetweenPoints;

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
        description.enabled = false;
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
                        break;
                    }
                }
                break;
            case State.Select:
                {

                }
                break;
            case State.Drag:
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        currentState = State.Idle;
                        //dotsVisibleSetting(0,numberOfDot, false);
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
            visualizerLine.enabled = false;
            //button.SetActive(false);
            description.enabled = false;
        }
        else if (currentState == State.Hover)
        {
            description.enabled = true;
        }
        else if (currentState == State.Select)
        {
            //button.SetActive(true);
            description.enabled = false;
        }
        else if (currentState == State.Drag)
        {
            calculate();
            //visualizerLine.enabled = true;
            //visualizerLine.positionCount = 10;
            //visualizerLine.SetPosition(0, new Vector3(transform.position.x,transform.position.y,-1f));
            //visualizerLine.SetPosition(1, new Vector3((point.x- transform.position.x)/2,(point.y-transform.position.y)/2,-1f));
            //visualizerLine.SetPosition(2, new Vector3(point.x,point.y,-1f));
        }
        else if (currentState == State.Apply)
        {
            //StatusAdjustment(Card,saInformation)//(적용대상과,적용되야하는 값 정보) CATEGORY (DEFENSE,ATTACK,WEAKNESS,
            StatusAdjustment.SetFunction(player, saInformation);
            Destroy(this.gameObject);
        }
    }


    bool isInside(Vector2 mousepoint)
    {
        if (collider.OverlapPoint(mousepoint))
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
        Vector3 directionnormal = (point - this.gameObject.transform.position).normalized;
        int count = 0;
        while ((directionnormal * count).sqrMagnitude <= (point - transform.position).sqrMagnitude)
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
