using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public class EnemyTarget : MonoBehaviour, PotionInterface
{
    public TextMeshProUGUI description;
    public TextMeshProUGUI potionname;
    public int rarity;
    State currentState;
    Collider2D collider;
    Vector3 point;
    [SerializeField]
    public SpriteRenderer potionsprite;
    public GameObject button;
    public SAInformation saInformation;
    public Player player;
    public EnemyBase enemy;

    //public GameObject dot;
    //GameObject []dots;
    //public int numberOfDot;
    //public float spaceBetweenPoints;

    public void setUp(Potion potion)
    {
        description.text = potion.description.ToString();
        potionname.text = potion.name.ToString();
        rarity = potion.rarity;
        potionsprite.sprite = potion.sprite;
        saInformation = new SAInformation(potion.target, potion.turn, potion.category, potion.amount);

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
        if (GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //dots=new GameObject[numberOfDot];
        //for (int i = 0; i < numberOfDot; i++) 
        //{
        //    dots[i] = Instantiate(dot, transform.position, Quaternion.identity);
        //    dots[i].SetActive(false);
        //}
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
                    if(Input.GetMouseButtonDown(1))
                    {
                        currentState = State.Idle;
                        //dotsVisibleSetting(0,numberOfDot, false);
                        break;
                    }
                    if (detectEnemy(point))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            currentState = State.Apply;
                            break;
                        }
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
        if (!player && GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        handleinput();
        if (currentState == State.Idle)
        {
            visualizerLine.enabled = false;
            button.SetActive(false);
            description.enabled = false;
            potionname.enabled = false;
        }
        else if (currentState == State.Hover)
        {
            description.enabled = true;
            potionname.enabled = true;
        }
        else if (currentState == State.Select)
        {
            button.SetActive(true);
            description.enabled = false;
        }
        else if(currentState==State.Drag)
        {
            button.SetActive(false);
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
            StatusAdjustment.SetFunction(this,enemy, saInformation);
            Destroy(this.gameObject);
        }
    }

    bool detectEnemy(Vector2 mousePoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePoint, transform.forward, 400f);
        for (int i = 0; i < hits.Length; i++)
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

        //Vector3 directionnormal = (point - this.gameObject.transform.position).normalized;
        //int count = 0;
        //while((directionnormal * count).sqrMagnitude <= (point-transform.position).sqrMagnitude)
        //{
        //    count++;
        //}
        //visualizerLine.enabled = true;
        //visualizerLine.positionCount = count;
        //for (int i=0; i < count;i++) 
        //{
        //    visualizerLine.SetPosition(i,transform.position+directionnormal*i);
        //}
        Vector3 directionnormal = (new Vector3(point.x, point.y, 0) - new Vector3(transform.position.x, transform.position.y, -0)).normalized;
        int count = 0;
        while ((directionnormal * count).sqrMagnitude <= (new Vector3(point.x, point.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).sqrMagnitude)
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

    //public void dotsVisibleSetting(int start,int end,bool condition)
    //{
    //    for (int i = start; i < end-start; i++) dots[i].SetActive(condition);
    //}
}
