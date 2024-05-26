using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinCondition : MonoBehaviour
{
    private bool bossDead = false;
    private Enemy enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<Enemy>();
        
    }

    private void Update()
    {
        if (enemyHealth.currentHealth <= 0 && !bossDead)
        {
            GameManager.Instance.PlayerWin();
            bossDead = true;
            Debug.Log("WIN");
        }
    }
}
