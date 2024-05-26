using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRoidController : MonoBehaviour
{
    SwordRoidAnim anim;
    SwordRoidCombat combat;
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
        anim = GetComponent<SwordRoidAnim>();
        combat = GetComponent<SwordRoidCombat>();
        enemyHealth = GetComponent<Enemy>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        combat.cooldownTimer += Time.deltaTime;


        SwordRoidBrain();
    }

    private void SwordRoidBrain()
    {
        State currentState = State.Idle;


        if (enemyHealth.currentHealth <= 0)
        {
            currentState = State.Death;
            Debug.Log("NO PROBLEM");
        }
        else if(combat.PlayerInSight() && combat.cooldownTimer >= combat.attackCooldown)
        {
			
			currentState = State.Attack;
        }
        else if(EnemyAggroRange()) 
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
    }

    public void ApplyDamageAndCD()
    {
        combat.ApplyDamage();
        combat.cooldownTimer = 0;
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (player.transform.position.x > transform.position.x && EnemyAggroRange())
        {
            if (!faceRight)
            {
                Flip();
            }
        }
        else if (player.transform.position.x < transform.position.x && EnemyAggroRange())
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

    public void AttackSound()
    {
		GameManager.Instance.EnemyMeleeAttackSound();
	}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCollider2d.bounds.center, aggroRange);
    }

}
