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
    int t_curCount;
    int t_AllturnCount;
    void Start()
    {
        turnCount = 0;
        t_AllturnCount = 0;
        t_curCount = 0;
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


    void t_AddTurnCount()
    {
        t_AllturnCount++;
    }

    void StartCountTurn()
    {
        t_curCount = t_AllturnCount;
    }



    int GetCurcount()
    {
        int countturn = 0;
        if (t_curCount % 2 == t_AllturnCount % 2)
        {
            countturn = (t_AllturnCount - t_curCount) / 2;
        }
        else
        {
            Debug.Log("Enemyturn or Playerturn is Left");
        }
        return countturn;
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
        Enemy.EnemyTurnStart();
        t_AddTurnCount();
        CardManager.Inst.TurnOver();
    }


    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
        turnCount++;
        t_AddTurnCount();
        CardManager.Inst.MyTurn();
    }

    public void StartCountEnemyTurn()
    {
        StartCountTurn();
    }

    public int GetEnemyTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        UpdateEnemyturnCount = GetCurcount();
        return UpdateEnemyturnCount;
    }
}
