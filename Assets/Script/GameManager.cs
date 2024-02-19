using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject[] enemyBases;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        enemyBases = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) 
        {
            if(player.getCurrentHp()<=0)
            {

            }
        }

        if(enemyBases!=null)
        {
            for(int i=0;i<enemyBases.Length; i++) 
            {
                if ()
                {
                    
                }
                enemyBases[i].GetComponent<EnemyBase>();
            }
        }
    }
}
