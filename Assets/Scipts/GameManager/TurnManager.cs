using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    public enum TurnType { Playerturn, Enemyturn }
    public TurnType Curturn;
    void Start()
    {
        Curturn = TurnType.Playerturn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PalyerTurnOver()
    {
        Curturn = TurnType.Enemyturn;
    }
    public void EnemyTurnOver()
    {
        Curturn = TurnType.Playerturn;
    }
}
