using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defect;
    public GameObject victory;
    public Player player;
    public GameObject[] enemyBases;

    public GameObject enemySocket;
    public GameObject playerSocket;
    public GameObject battleSocket;
    // Start is called before the first frame update
    void Start()
    {
        defect.SetActive(false);
        victory.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        enemyBases = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void PlayerLose()
    {
        //playerSocket.SetActive(false);
        defect.SetActive(true);
        battleSocket.SetActive(false);
        CardManager.Inst.BattleEnd();
    }
    void PlayerWin()
    {
        //enemySocket.SetActive(false);
        //enemyBases[i].SetActive(false);
        battleSocket.SetActive(false);
        victory.SetActive(true);
        CardManager.Inst.BattleEnd();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.C))
        { PlayerLose(); }
        if (Input.GetKeyDown(KeyCode.D))
        {   PlayerWin(); }

        if (player != null) 
        {
            if(player.getCurrentHp()<=0)
            {
                PlayerLose();
            }
        }

        if(enemyBases!=null)
        {
            for(int i=0;i<enemyBases.Length; i++) 
            {
                if (enemyBases[i].GetComponent<EnemyBase>().GetEnemyCurHp()>0)
                {
                    return;
                }
                    PlayerWin();
            }
        }
    }
}
