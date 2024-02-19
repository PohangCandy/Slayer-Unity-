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

    void StartCountTurn(ref int turn)
    {
        turn = t_AllturnCount;
        Debug.Log("UpdateCurturn is : " + t_AllturnCount);
        Debug.Log("Add allturn to curEnemyTurn" + t_curEnemyturnCount);
    }



    int GetCurcount(int turn)
    {
        Debug.Log("before divide % 2 EnemyCurturn" + t_curEnemyturnCount);
        Debug.Log("PlayerCurturn" + t_curPlayerturnCount);
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
        Debug.Log("before add 2 to allturn" + t_curEnemyturnCount);
        t_AddTurnCount();
        Enemy.EnemyTurnOver();
        turnCount++;
        CardManager.Inst.MyTurn();
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
        Debug.Log("Here is Enemy count");
        Debug.Log("before update % 2 EnemyCurturn" + t_curEnemyturnCount);
        UpdateEnemyturnCount = GetCurcount(t_curEnemyturnCount);
        return UpdateEnemyturnCount;
    }
    public int GetPlayerTurnCount()
    {
        int UpdateEnemyturnCount = 0;
        Debug.Log("Here is Player count");
        Debug.Log("before update % 2 PlayerCurturn" + t_curEnemyturnCount);
        UpdateEnemyturnCount = GetCurcount(t_curPlayerturnCount);
        return UpdateEnemyturnCount;
    }
}
