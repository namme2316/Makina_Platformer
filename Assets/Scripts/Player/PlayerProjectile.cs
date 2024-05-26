using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    private GameObject player;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] int bulletDamage = 20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        rb.velocity = new Vector2(bulletSpeed * player.transform.localScale.x, 0);
        Invoke("Clear", 5f);
    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.GetComponent<EnemyBoss>() != null)
		{
			collision.gameObject.GetComponent<EnemyBoss>().TakeDamage(bulletDamage);
			Clear();
		}

		if (collision.gameObject.GetComponent<Enemy>() != null)
		{
			collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
			Clear();
		}
        
	}

    private void  Clear()
    {
        Destroy(gameObject);
    }


}
