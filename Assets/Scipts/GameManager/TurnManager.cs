using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    public enum TurnType { Playerturn, Enemyturn }
    TurnType Curturn;
    public EnemyBase Enemy;
    int turnCount;
    int t_turnCount;
    int t_AllturnCount;
    void Start()
    {
        turnCount = 0;
        t_AllturnCount = 0;
        Curturn = TurnType.Playerturn;
    }
    public int GetTurnCount()
    {
        return turnCount;
    }
    public void SetTurnCount(int newturnCount)
    {
        turnCount = newturnCount;
    }

    public int t_GetAllTurnCount()
    {
        return t_AllturnCount;
    }
    public void t_GetTurnCount(TurnType curturn)
    {
        curturn = Curturn;
        if(curturn == TurnType.Playerturn)
        {
            int allturncount = t_GetAllTurnCount();
            if(allturncount % 2 == 0 && turnCount != 0)
            {
                turnCount++;
            }
        }
        else if(curturn == TurnType.Enemyturn)
        {
            int allturncount = t_GetAllTurnCount();
            if (allturncount % 2 == 1 && turnCount != 0)
            {
                turnCount++;
            }
        }

    }
    void t_AddTurnCount()
    {
        t_AllturnCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
        Enemy.EnemyTurnStart();
        t_AddTurnCount();
    }
    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
        turnCount++;
        t_AddTurnCount();
    }
}
