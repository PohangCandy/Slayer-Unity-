using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTarget :MonoBehaviour, Potion
{
    public Text description;
    State currentState;
    Collider2D collider;
    Vector3 point;
    public GameObject button;
    SAInformation saInformation;

    public struct SAInformation
    {
        public SAInformation(string _target, int _turn, string _category, int _damage)
        {
            target = _target;
            turn = _turn;
            category = _category;

            damage = _damage;
        }
        public string target;
        public int turn;
        public string category;
        public int damage;
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
        //saInformation = new SAInformation();    데이터베이스로 부터 가져옴
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
                break;
            case State.Apply:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Debug.Log(currentState);
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
            //이펙트 effect(Card,saInformation)
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
