using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] FloatValueSO currentHealth;
    [SerializeField] InputManager inputManager;
    [SerializeField] SpriteRenderer spriteRenderer;

    float maxHealth = 100;
    bool isAlive = true;
    void Start()
    {
        currentHealth.floatValue = maxHealth;
        inputManager.SetPlayer();
    }

    public void TakeDamage(int damage)
    {
       currentHealth.floatValue -= damage;
    }
}
