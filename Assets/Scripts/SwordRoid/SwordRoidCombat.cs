using UnityEngine;

public class SwordRoidCombat : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] public float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    public float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    [SerializeField] PlayerHealth playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public void ApplyDamage()
    {
        playerHealth.TakeDamage(damage);
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