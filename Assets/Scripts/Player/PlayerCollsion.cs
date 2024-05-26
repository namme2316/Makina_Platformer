using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsion : MonoBehaviour
{
    [SerializeField] FloatValueSO playerHealth;

    Vector2 respawnPoint;
    [SerializeField] float RespawnDelay = 0.5f;
    SpriteRenderer sr;
    Rigidbody2D rb;
    private bool IsDead = false;

	private void Start()
	{
		respawnPoint = transform.position;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
	}


	private void Update()
	{
		if (playerHealth.floatValue <= 0 && !IsDead)
        {
            IsDead = true;
            StartCoroutine("RespawnCoroutine");
            
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Hazard"))
        {
            StartCoroutine("RespawnCoroutine");
        }

        if(collision.gameObject.CompareTag("HealthRestore"))
        {
            GameManager.Instance.HealthPickUpSound();
            playerHealth.floatValue += collision.gameObject.GetComponent<HealthRestore>().healthRestoreValue;
            if(playerHealth.floatValue >= 100) 
            { 
                playerHealth.floatValue = 100;
            }
            Destroy(collision.gameObject);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Respawn"))
		{
            respawnPoint = collision.gameObject.transform.position;
		}
	}

	private IEnumerator RespawnCoroutine()
    {
        GameManager.Instance.PlayerDeathSound();
        sr.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(RespawnDelay);
		transform.position = respawnPoint;
        sr.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
		playerHealth.floatValue = 100;
        IsDead = false;
	}

}
