using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Transform attackArea;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask attackableLayers;
    [SerializeField] int attackDamage = 25;
    [SerializeField] float attackCooldown = 0.5f;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectilePosition;

    private bool canAttack = true;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputManager.AttackEvent += PlayerAttack;
        inputManager.ThrowEvent += PlayerThrow;
    }

    private void OnDisable()
    {
        inputManager.AttackEvent -= PlayerAttack;
        inputManager.ThrowEvent -= PlayerThrow;
    }

    private void PlayerAttack()
    {
        if (canAttack)
        {
            GameManager.Instance.PlayerMeleeAttackSound();
            anim.SetTrigger("Attack");    
            StartCoroutine(AttackCooldown());
        }
       
    }

    private void PlayerThrow()
    {
        if(canAttack)
        {
            GameManager.Instance.PlayerRangeAttackSound();
            anim.SetTrigger("Throw");
            StartCoroutine(AttackCooldown());
        }
    }

    public void SpawnProjectile()
    {
        Instantiate(projectile, projectilePosition.position, Quaternion.identity);
    }


    public void ApplyMeleeDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, attackableLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<EnemyBoss>() != null)
            {
                enemy.GetComponent<EnemyBoss>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }         
        }

        
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    

    private void OnDrawGizmosSelected()
    {
        if(attackArea == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackArea.position, attackRange);
    }



}
