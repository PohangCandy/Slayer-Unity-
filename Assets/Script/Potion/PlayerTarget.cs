using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using StatusAdjustmentInformationNameSpace;

public class PlayerTarget :MonoBehaviour, PotionInterface
{
    public Text description;
    public Text potionname;
    int rarity;
    State currentState;
    Collider2D collider;
    Vector3 point;
    [SerializeField]
    SpriteRenderer potionsprite;
    public GameObject button;
    SAInformation saInformation;
    public Player player;

    public void setUp(Potion potion)
    {
        description.text = potion.description.ToString();
        potionname.text = potion.name.ToString();
        rarity = potion.rarity;
        potionsprite.sprite = potion.sprite;
        saInformation = new SAInformation(potion.target, potion.turn, potion.category, potion.amount);
    }


    enum State
    {
        Idle,
        Hover,
        Select,
        Apply
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Idle;
        collider = GetComponent<Collider2D>();
        saInformation = new SAInformation("defense", 3, "Defense", 10);
        //saInformation = new SAInformation();    데이터베이스로 부터 가져옴
    }
    public void handleinput()//상태를 바꾸는 함수
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
                break;
            case State.Apply:
                break;
        }
    }

    // Update is called once per frame
    void Update()//상태가 바뀌면 처리해야하는 것들을 여기에 넣어둠
    {
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        //Debug.Log(currentState);
        //print("current state %s", currentState);
        handleinput();

        if (currentState == State.Idle)
        {
            button.SetActive(false);
            description.enabled = false;
        }
        else if (currentState == State.Hover)
        {
            description.enabled = true;
        }
        else if (currentState == State.Select)
        {
            button.SetActive(true);
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
        currentState = State.Apply;
    }

    public void cancleEvent()
    {
        currentState = State.Idle;
    }
}
