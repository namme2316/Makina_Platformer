using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamarawCombat : MonoBehaviour
{
    Rigidbody2D rb;
    
	[Header("Attack Parameters")]
	[SerializeField] public float attackCooldown;
	[SerializeField] private float range;
	[SerializeField] private int damage;
	[SerializeField] float chargeForce = 5f;

	[Header("Collider Parameters")]
	[SerializeField] private float colliderDistance;
	[SerializeField] private BoxCollider2D boxCollider;

	[Header("Player Layer")]
	[SerializeField] private LayerMask playerLayer;
	public float cooldownTimer = Mathf.Infinity;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Charge() 
    {

		rb.AddForce(new Vector2(chargeForce * transform.localScale.x, 0), ForceMode2D.Impulse);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
	}

	public bool PlayerInSight()
	{
		RaycastHit2D hit =
			Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
			new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
			0, Vector2.left, 0, playerLayer);

		return hit.collider != null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
			new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
	}
}
