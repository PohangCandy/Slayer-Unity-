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
    enum State
    {
        Idle,
        Hover,
        Apply
    }
    // Start is called before the first frame update
    void Start()
    {
 
        currentState = State.Idle;
    }
    public void handleinput()
    {
        switch(currentState)
        {
            case State.Idle:
                description.enabled = false;
                break;
            case State.Hover:
                description.enabled = true;
                break;
            case State.Apply:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleinput();
    }
    private void OnMouseEnter()
    {
        Debug.Log("�÷��̾�� �� ��ġ 2��");
        currentState = State.Hover;

    }

    private void OnMouseExit()
    {
        currentState=State.Idle;
       
    }

    void OnMouseDown()
    {
        Debug.Log("��ü�� Ŭ���Ǿ����ϴ�!");
        
    }
}
