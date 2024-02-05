using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public class EnemyTarget : MonoBehaviour, Potion
{
    public Text description;
    State currentState;
    string target_tag;
    

    [SerializeField]
    private LineRenderaerAtoB visualizerLine;

    enum State
    {
        Idle,
        Hover,
        Drag,
        Apply
    }
    // Start is called before the first frame update
    void Start()
    {
        description.enabled = false;
    
        currentState = State.Idle;
    }
    public void handleinput()
    {
        switch (currentState)
        {
            case State.Idle:
                description.enabled = false;
                break;
            case State.Hover:
                {
                    description.enabled = true;
                    if(Input.GetMouseButtonDown(0))
                    {
                        currentState = State.Drag;
                    }
                }
                break;
            case State.Drag:
                {
                    if (Input.GetMouseButton(0))
                    {
                        Vector2 traget = Vector2.zero;
                        traget.x=Input.mousePosition.x-Screen.width/2;
                        traget.y=Input.mousePosition.y-Screen.height/2;
                        Vector2 direction=(traget-(Vector2)this.transform.position).normalized;
                        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,direction, Mathf.Infinity);// ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        //RaycastHit hit;
                        
                        if (hit)
                        {
                            //태그를 비교해서 사용함.
                           // if (hit.collider.gameObject.tag == "Enemy")
                            {
                                Debug.Log("적 찾음");
                                visualizerLine.Play(this.gameObject.transform.position, hit.point);
                            }
                        }
                        else visualizerLine.stop();
                        break;
                    }
                    
                    if(Input.GetMouseButtonUp(0))
                    {
                        currentState = State.Idle;
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
        handleinput();
    }
    private void OnMouseEnter()
    {
        Debug.Log("몬스터에게 데미지 10");
        currentState = State.Hover;
        description.enabled = true;
    }

    private void OnMouseExit()
    {
        if(currentState!=State.Drag)
        currentState = State.Idle;
        description.enabled = false;
    }

    void OnMouseDown()
    {
        Debug.Log("객체가 클릭되었습니다!");

    }
}
