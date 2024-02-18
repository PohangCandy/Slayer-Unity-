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
    int t_curEnemyturnCount;
    int t_curPlayerturnCount;
    int t_AllturnCount;
    void Start()
    {
        turnCount = 0;
        t_AllturnCount = 0;
        t_curEnemyturnCount = 0;
        t_curPlayerturnCount = 0;
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
        Debug.Log("Allturn : "+t_AllturnCount);
    }

    void StartCountTurn(int turn)
    {
        turn = t_AllturnCount;
    }



    int GetCurcount(int turn)
    {
        int countturn = 0;
        if (turn % 2 == t_AllturnCount % 2)
        {
            countturn = (t_AllturnCount - turn) / 2;
        }
        else
        {
            Debug.Log("Enemyturn or Playerturn is Left");
        }
        Debug.Log("Curturn" + turn);
        Debug.Log("Allturn" + t_AllturnCount);
        return countturn;
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
        t_AddTurnCount();
        Enemy.EnemyTurnStart();
        CardManager.Inst.TurnOver();
    }


    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
        t_AddTurnCount();
        Enemy.EnemyTurnOver();
        turnCount++;
        CardManager.Inst.MyTurn();
    }

    public void StartCountEnemyTurn()
    {
        StartCountTurn(t_curEnemyturnCount);
    }

    public void StartCountPlayerTurn()
    {
        StartCountTurn(t_curPlayerturnCount);
    }

    public int GetEnemyTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        Debug.Log("Here is Enemy count");
        UpdateEnemyturnCount = GetCurcount(t_curEnemyturnCount);
        return UpdateEnemyturnCount;
    }
    public int GetPlayerTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        Debug.Log("Here is Player count");
        UpdateEnemyturnCount = GetCurcount(t_curPlayerturnCount);
        return UpdateEnemyturnCount;
    }
}
