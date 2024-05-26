using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] Vector2 aggroRange;
    [SerializeField] Collider2D enemyCollider2d;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject player;
    [SerializeField] EnemyStates state;
    [SerializeField] float speed = 1;
    private Rigidbody2D rb;
    private bool faceRight = true;
    



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (EnemyAggroRange())
        {
            state.SetState(EnemyStates.enemyState.Chase);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        
        if (player.transform.position.x > transform.position.x && EnemyAggroRange()) 
        {
            if(!faceRight) 
            {
                Flip();
            }
        }
        else if(player.transform.position.x < transform.position.x && EnemyAggroRange())
        {
            if(faceRight)
            {
                Flip();
            }

        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }


    public bool EnemyAggroRange()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(enemyCollider2d.bounds.center, aggroRange, 0f, playerLayer);

        if (playerCollider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCollider2d.bounds.center, aggroRange);
    }

}
