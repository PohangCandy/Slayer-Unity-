using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager instance{  get; private set; }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] PotionSO potionSO;
    [SerializeField] List<GameObject> potionPrefab;


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
        for(int i=0;i<potionSO.potions.Length;i++) 
        {
            Potion potion = potionSO.potions[i];
            //switch(potionSO.potions[i].targetType) 
            //{
            //    case "Player":
            //        {
            //            playerPotionbuffer.Add(potion);
            //        }
            //        break;
            //    case "Enemy":
            //        {

            //        }
            //        break;
            //    case "Card":
            //        {

            //        }
            //        break;


            //}
           potionBuffer.Add(potion);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            addPotion(0);
        if (Input.GetKeyDown(KeyCode.W))
            addPotion(1);
        if (Input.GetKeyDown(KeyCode.E))
            addPotion(2);
        if (Input.GetKeyDown(KeyCode.R))
            Debug.Log(PopPotion().name);
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
    void addPotion(int type)
    {
        
       switch (type)
        {
            case 0:
                {
                    var potionObject = Instantiate(potionPrefab[0],new Vector3(0,0,0), Quaternion.identity);
                var cardTarget=potionObject.GetComponent<CardTarget>();
                    cardTarget.setUp(PopPotion());
                }
                break;
            case 1:
                {
                    var potionObject = Instantiate(potionPrefab[1], new Vector3(3, 0,0), Quaternion.identity);
                    var playerTarget = potionObject.GetComponent<PlayerTarget>();
                    playerTarget.setUp(PopPotion());
                }
                break;
            case 2:
                {
                    var potionObject = Instantiate(potionPrefab[2], new Vector3(-3,0,0), Quaternion.identity);
                    var enemyTarget = potionObject.GetComponent<EnemyTarget>();
                    enemyTarget.setUp(PopPotion());
                }
                break;
        }
        
    }
}
