using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    enum TurnType { Playerturn, Enemyturn }
    TurnType Curturn;
    public EnemyBase Enemy;
    int turnCount;
    void Start()
    {
        turnCount = 0;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
        Enemy.EnemyTurnStart();
    }
    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
        turnCount++;
    }
}
