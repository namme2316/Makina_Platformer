using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRoidController : MonoBehaviour
{
    CanonRoidAnim anim;
    CanonRoidCombat combat;
    Enemy enemyHealth;

    [Header("Chase")]
    [SerializeField] float speed = 2.0f;
    [SerializeField] Vector2 aggroRange;
    [SerializeField] Collider2D enemyCollider2d;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject player;

    private bool faceRight = false;

    enum State
    {
        Idle,
        Run,
        Attack,
        Death,
    }

    private void Awake()
    {
        anim = GetComponent<CanonRoidAnim>();
        combat = GetComponent<CanonRoidCombat>();
        enemyHealth = GetComponent<Enemy>();
    }

    void Start()
    {

    }

    void Update()
    {
        combat.cooldownTimer += Time.deltaTime;


        CanonRoidBrain();
        FaceDirection();
    }

    private void CanonRoidBrain()
    {
        State currentState = State.Idle;


        if (enemyHealth.currentHealth <= 0)
        {
           currentState = State.Death;
        }
        else if (combat.PlayerInSight() && combat.cooldownTimer >= combat.attackCooldown)
        {
            currentState = State.Attack;
        }
        else if (EnemyAggroRange() && !combat.PlayerInSight())
        {
            currentState = State.Run;
        }
        else
        {
            currentState = State.Idle;
        }


        switch (currentState)
        {
            case State.Idle:
                currentState = State.Idle;
                break;
            case State.Run:
                Chase();
                break;
            case State.Attack:

                break;
            case State.Death:
                currentState = State.Death;
                break;
        }

        anim.SetState((int)currentState);

        Debug.Log(currentState);
    }

    public void ApplyCD()
    {
        combat.cooldownTimer = 0;
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    void FaceDirection() 
    {
        if (player.transform.position.x > transform.position.x && (EnemyAggroRange() || combat.PlayerInSight()))
        {
            if (!faceRight)
            {
                Flip();
            }
        }
        else if (player.transform.position.x < transform.position.x && (EnemyAggroRange() || combat.PlayerInSight()))
        {
            if (faceRight)
            {
                Flip();
            }

        }
    }


    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }


    bool EnemyAggroRange()
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
