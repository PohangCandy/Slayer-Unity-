
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
    //            { //�Է��� �޾Ƽ� �������·� �Ѿ �� �ֵ��� ó��
    //            }
    //            break;
    //        case State.drag:
    //            //�巡�� �ϰ� ���� ���콺�� ���� �̺�Ʈ�� �߻��� ��쿡 Ȯ���ؼ� �������·� �����Ѵ�.
    //            {
    //                if(Input.GetMouseButtonUp(0))
    //                {
    //                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //                    RaycastHit hit;

    //                    if (Physics.Raycast(ray, out hit))
    //                    {
    //                        //�±׸� ���ؼ� �����.
    //                        if (hit.collider.gameObject.tag==target_tag)
    //                        {
    //                            Debug.Log("�� ã��");
    //                            target = hit.collider.gameObject;
    //                            //currentState=State.apply;
    //                        }

    //                    }
    //                }
    //            }
    //            break;
    //        case State.apply:
    //            {
    //                Debug.Log("�� ����");

    //            }
    //            //�ش��ϴ� ��ü�� �����Ѵ�. �ѱ涧 �ڽ��� ��ü�� �Ѱܼ� ����ѵڿ� ����Ű�� ������Ʈ�� ã�Ƴ���.
    //            break;


    //    }
    //}
    //void OnMouseDown()
    //{
    //    Debug.Log("��ü�� Ŭ���Ǿ����ϴ�!");
    //    currentState = State.drag;
    //}

}
