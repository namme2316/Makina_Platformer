using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdShoot : MonoBehaviour
{
    [SerializeField] GameObject birdProjectile;
    [SerializeField] Transform projectilePosition;
    [SerializeField] float shootRate = 2f;
    private float timer;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootRate)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(birdProjectile, projectilePosition.position, Quaternion.identity);
    }
}
