using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] InputManager inputManager;
    private Transform trans;
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    private BoxCollider2D boxCollider2d;
    public LayerMask groundLayer;
    private SpriteRenderer sr;
    private Animator anim;


    [Header("Moving")]
    [SerializeField] float speed = 4f;
    private Vector2 inputVector;
    private bool isFacingRight = true;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float maxFallSpeed = 12f;
    [SerializeField] float gravityMultiplier = 1.5f;
	private float originalGravity;
    [SerializeField] float coyoteTime = 0.3f;
    private float coyoteTimeCounter;

    [Header("Dashing")]
    [SerializeField] private float dashingPower = 5f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    
    //For handling animation states
    private enum MovementState { idle, running, jumping, falling }


    private void Awake()
    {
        trans = GetComponent<Transform>(); 
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        inputManager.MoveEvent += HandleMove;
        inputManager.JumpEvent += Jump;
        inputManager.JumpCanceledEvent += JumpCanceled;
        inputManager.DashEvent += Dash;
        inputManager.SetPlayer();
    }

    private void OnDisable()
    {
        inputManager.DashEvent -= Dash;
        inputManager.MoveEvent -= HandleMove;
        inputManager.JumpEvent -= Jump;
        inputManager.JumpCanceledEvent -= JumpCanceled;
    }

    private void Start()
    {
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        ValueReset();
        CoyoteTimecounter();
        UpdateAnimation();

    }

    private void FixedUpdate()
    {
        if(isDashing) 
        {
            return;
        }
        Move();

        ClampFall();
    }

    private void HandleMove(Vector2 ctx)
    {
        inputVector = ctx;
    }

    private void Move()
    {
        float xDirection = Mathf.Round(inputVector.x);
        rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);
    }

    private void Dash()
    {
        if(canDash && rb.velocity != Vector2.zero) 
        {
            canDash = false;
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        Vector2 dashDirection = new Vector2(inputVector.x, inputVector.y).normalized;
        canDash = false;
        isDashing = true;
        rb.velocity = rb.velocity.normalized * dashingPower;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void Jump()
    {
        if (coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.02f, jumpForce);
        }
    }

    public void JumpCanceled()
    {
        rb.gravityScale *= gravityMultiplier;
        coyoteTimeCounter = 0;
    }

    private void ClampFall()
    {
		if (rb.velocity.y < -maxFallSpeed)
		{
			rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
		}
	}

    private void ValueReset()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            rb.gravityScale = originalGravity;
        }
    }

    private void CoyoteTimecounter()
    {
        if (!IsGrounded() && rb.velocity.y < 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void UpdateAnimation()
    {
        MovementState currentState = MovementState.idle;

        if(IsGrounded()) 
        {
            anim.SetBool("IsGrounded", true);
        }
        else 
        {
            anim.SetBool("IsGrounded", false);
        }

        if (inputVector.x > 0)
        {
            currentState = MovementState.running;
            if (!isDashing && !isFacingRight)
                Flip();
        }
        else if (inputVector.x < 0)
        {
            currentState = MovementState.running;
            if (!isDashing && isFacingRight)
                Flip();
        }
        else
        {
            if(rb.velocity.y == 0 && rb.velocity.x == 0 && IsGrounded())
                currentState = MovementState.idle;
        }
        



        if(rb.velocity.y > 0.01f && !IsGrounded())
        {
            currentState = MovementState.jumping;
        }
        else if(rb.velocity.y < -0.01f && !IsGrounded())
        {
            currentState = MovementState.falling;
        }


        anim.SetInteger("state", (int)currentState);
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }


    private bool IsGrounded()
    {
        float extend = 0.2f;
        RaycastHit2D boxCastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extend, groundLayer);
        Color rayColor;
        if (boxCastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extend), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extend), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extend), Vector2.right * (boxCollider2d.bounds.extents.y + extend), rayColor);

        return boxCastHit.collider != null;
    }
}
