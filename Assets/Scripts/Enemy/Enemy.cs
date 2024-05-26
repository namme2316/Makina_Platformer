using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth = 100;
    private bool IsDead = false;



	private void Update()
	{
		if (currentHealth <= 0 && !IsDead) 
        {
            IsDead = true;
            GameManager.Instance.EnemyDeathSound();
        }
	}

	public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }

    

}
