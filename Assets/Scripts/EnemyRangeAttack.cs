using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectilePosition;


    [SerializeField] float shootRate = 2f;
    private float timer;

    private EnemyChase chase;


    private void Awake()
    {
        chase = GetComponent<EnemyChase>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > shootRate && chase.EnemyAggroRange()) 
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot() 
    {
        Instantiate(projectile,projectilePosition.position,Quaternion.identity);   
    }
}
