using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public TurnManager turnManager;
    public static PotionManager instance{  get; private set; }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] PotionSO potionSO;
    [SerializeField] List<GameObject> potionPrefab;
    public GameObject sockect;

    List<Potion> potionBuffer;
    List<CardTarget> cardPotionbuffer;
    List<PlayerTarget> playerPotionbuffer;
    List<EnemyTarget> enemyPotionbuffer;
    void Start()
    {
        SetupBuffer();
    }
    void SetupBuffer()
    {
        potionBuffer=new List<Potion> ();
        for(int i=0;i<potionSO.potions.Length+8;i++) 
        {
            int rand = Random.Range(0, potionSO.potions.Length);
            Potion potion = potionSO.potions[rand];
           potionBuffer.Add(potion);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) 
        {
            RandomPotionAdd();


        }
        //if (Input.GetKeyDown(KeyCode.Q))
        //    addPotion(0);
        //if (Input.GetKeyDown(KeyCode.W))
        //    addPotion(1);
        //if (Input.GetKeyDown(KeyCode.E))
        //    addPotion(2);
        if (Input.GetKeyDown(KeyCode.R))
            Debug.Log(PopPotion().name);
    }

    public void RandomPotionAdd()
    {
        if (potionBuffer.Count <= 0) return;
        int rand = Random.Range(0, potionBuffer.Count - 1);
        addPotion(potionBuffer[rand]);
        potionBuffer.RemoveAt(rand);
    }
    public Potion PopPotion()
    {
        if( potionBuffer.Count <= 0 ) 
        {
            SetupBuffer ();
        }
        Potion potion = potionBuffer[0];
        potionBuffer.RemoveAt(0);
        return potion;
    }
    
    void addPotion(Potion potion)
    {
        
       switch (potion.targetType)
        {
            case "Card":
                {
                    
                    var potionObject = Instantiate(potionPrefab[0],new Vector3(0,0,0), Quaternion.identity);
                var cardTarget=potionObject.GetComponent<CardTarget>();
                    cardTarget.transform.SetParent(sockect.transform, false);
                    cardTarget.setUp(potion);
                }
                break;
            case "Player":
                {
                    var potionObject = Instantiate(potionPrefab[1], new Vector3(3, 0,0), Quaternion.identity);
                    var playerTarget = potionObject.GetComponent<PlayerTarget>();
                    playerTarget.transform.SetParent(sockect.transform, false);
                    playerTarget.setUp(potion);
                }
                break;
            case "Enemy":
                {
                    var potionObject = Instantiate(potionPrefab[2], new Vector3(-3,0,0), Quaternion.identity);
                    var enemyTarget = potionObject.GetComponent<EnemyTarget>();
                    enemyTarget.transform.SetParent(sockect.transform, false);
                    enemyTarget.setUp(potion);
                }
                break;
        }
        
    }
}
