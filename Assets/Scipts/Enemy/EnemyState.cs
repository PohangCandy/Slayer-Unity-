using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyState : MonoBehaviour
{
    public enum EEnemyStateType
    {
        IdleState,
        AttackState,
        SKillState,
        DeadState,
    }

    EEnemyStateType CurrentState = EEnemyStateType.IdleState;
}


