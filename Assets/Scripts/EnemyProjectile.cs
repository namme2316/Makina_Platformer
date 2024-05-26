using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] int bulletDamage = 5;
    [SerializeField] float bulletSpeed = 5f;

    private float bulletTimeSpan = 5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
		GameManager.Instance.PlayShootSound1();
		StartCoroutine(BulletCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BulletCoroutine()
    {
        yield return new WaitForSeconds(bulletTimeSpan);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

}
