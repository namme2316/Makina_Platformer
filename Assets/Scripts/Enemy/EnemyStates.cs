using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    private Animator anim;


    public enum enemyState
    {
        Idle,
        Chase,
    }

    public enemyState currentState;

    private void Awake()
    {
        currentState = enemyState.Idle;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        EnemyUpdateAnim();
    }

    public void SetState(enemyState state)
    {
        this.currentState = state;
    }

    private void EnemyUpdateAnim()
    {
        anim.SetInteger("enemyState", (int)currentState);

    }

    
        


}
