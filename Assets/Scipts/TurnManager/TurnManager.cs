using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    public enum TurnType { Playerturn, Enemyturn }
    TurnType Curturn;
    [SerializeField]
    public EnemyBase[] Enemy;
    int turnCount;
    int t_curEnemyturnCount;
    int t_curPlayerturnCount;
    int t_AllturnCount;
    int endturn_Enemy;
    void Start()
    {
        endturn_Enemy = 0;
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
    }

    void StartCountTurn(ref int turn)
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
        return countturn;
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
        t_AddTurnCount();
        for(int i = 0;i < Enemy.Length;i++)
        {
            Enemy[i].UpdateSALastTurnWhenEnemyTurnStart();
        }
        CardManager.Inst.TurnOver();
        //CardManager.Inst.Invoke("TurnOver", 1);
        //Invoke("CardManager.Inst.TurnOver",2);
    }

    public void CheckAllEnemyTurnOver(int add_endEnemy)
    {
        endturn_Enemy += add_endEnemy;
        if (Enemy.Length == endturn_Enemy)
        {
            endturn_Enemy = 0;
            EnemyTurnOver();
        }

    }

    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
        t_AddTurnCount();
        for (int i = 0; i < Enemy.Length; i++)
        {
            Enemy[i].UpdateSALastTurnWhenPlayerTurnStart();
        }
        turnCount++;
        CardManager.Inst.Invoke("MyTurn", 1);
        //*Invoke("CardManager.Inst.MyTurn", 2);
        //CardManager.Inst.MyTurn();
    }

    public void StartCountEnemyTurn()
    {
        StartCountTurn(ref t_curEnemyturnCount);
    }

    public void StartCountPlayerTurn()
    {
        StartCountTurn(ref t_curPlayerturnCount);
    }

    public int GetEnemyTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        UpdateEnemyturnCount = GetCurcount(t_curEnemyturnCount);
        return UpdateEnemyturnCount;
    }
    public int GetPlayerTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        UpdateEnemyturnCount = GetCurcount(t_curPlayerturnCount);
        return UpdateEnemyturnCount;
    }
}
