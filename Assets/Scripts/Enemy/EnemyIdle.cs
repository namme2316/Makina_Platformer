using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    EnemyChase chase;
    EnemyStates enemy;
    private void Awake()
    {
        chase = GetComponent<EnemyChase>();
        enemy = GetComponent<EnemyStates>();
    }

    private void Update()
    {
        if(chase.EnemyAggroRange() == false)
        {
            enemy.SetState(EnemyStates.enemyState.Idle);
        }
    }
}
