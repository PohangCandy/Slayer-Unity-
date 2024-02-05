
public interface Potion
{
    // Start is called before the first frame update
    //public string name;
    ////List<class StatusAdjustment> add;
    //public int rarity;
    //public string target_tag;
    //public string description;
    ////public GameObject target;

    //////ApplyMode currentMode;
    //State currentState;
    //enum ApplyMode
    //{
    //    one,
    //    all
    //};
    public abstract void handleinput();
    //public void Awake()
    //{

    //}
    //void Start()
    //{
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    switch (currentState) 
    //    {
    //        case State.idle:
    //            { //입력을 받아서 다음상태로 넘어갈 수 있도록 처리
    //            }
    //            break;
    //        case State.drag:
    //            //드래그 하고 나서 마우스를 놓은 이벤트가 발생한 경우에 확인해서 다음상태로 세팅한다.
    //            {
    //                if(Input.GetMouseButtonUp(0))
    //                {
    //                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //                    RaycastHit hit;

    //                    if (Physics.Raycast(ray, out hit))
    //                    {
    //                        //태그를 비교해서 사용함.
    //                        if (hit.collider.gameObject.tag==target_tag)
    //                        {
    //                            Debug.Log("적 찾음");
    //                            target = hit.collider.gameObject;
    //                            //currentState=State.apply;
    //                        }

    //                    }
    //                }
    //            }
    //            break;
    //        case State.apply:
    //            {
    //                Debug.Log("적 적용");

    //            }
    //            //해당하는 객체에 적용한다. 넘길때 자신의 객체를 넘겨서 사용한뒤에 가르키는 오브젝트를 찾아낸다.
    //            break;


    //    }
    //}
    //void OnMouseDown()
    //{
    //    Debug.Log("객체가 클릭되었습니다!");
    //    currentState = State.drag;
    //}

}
