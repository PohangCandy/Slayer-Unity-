using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject defect;
    public GameObject victory;
    public Player player;
    public GameObject[] enemyBases;
    public DontDestroyDeck dont;
    public GameObject enemySocket;
    public GameObject playerSocket;
    public GameObject battleSocket;
    public string nextScenename;
    // Start is called before the first frame update
    void Start()
    {
        defect.SetActive(false);
        victory.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        enemyBases = GameObject.FindGameObjectsWithTag("Enemy");
        dont = GameObject.Find("DeckDontDestroy").GetComponent<DontDestroyDeck>();
    }
    public void endGame()
    {
        dont.endgame();
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
        CardManager.Inst.BattleEnd();
        DontDestroyDeck.instance.playerhp = player.getCurrentHp();
        PotionManager.instance.EndBattle();
        battleSocket.SetActive(false);
        victory.SetActive(true);
        
        
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene(nextScenename);
    }

    public void GoTitle()
    {
        dont.startgame();
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {
        if(dont==null)
            dont = GameObject.Find("DeckDontDestroy").GetComponent<DontDestroyDeck>();
        if (Input.GetKeyDown(KeyCode.C))
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
