using UnityEngine;

public class CanonRoidCombat : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] public float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectilePosition;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    public float cooldownTimer = Mathf.Infinity;

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

    public void Shoot()
    {
        GameManager.Instance.PlayShootSound2();
        Instantiate(projectile, projectilePosition.position, Quaternion.identity);
    }



}