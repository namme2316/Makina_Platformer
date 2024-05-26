using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public int currentHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.PlayerWin();
            Destroyed();
            Debug.Log("Enemy Killed");
        }
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }



}
