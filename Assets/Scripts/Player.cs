using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Movement")]
    private float movement;
    public float jumpHeight = 15f;
    public float speed = 5f;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask whatIsEnemy;

    private Animator animator;

    private bool isGround;
    private bool facingRight = true;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.5f;
    public LayerMask whatIsGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGround = true;
        facingRight = true;
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        Facing();
        PlayAnimationRun();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Collider2D collInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (collInfo)
        {
            isGround = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayAnimationAttack();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Death", true);
            Debug.Log("change later with death function");
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.deltaTime * speed;
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsEnemy);
        if (collInfo)
        {
            if (collInfo.gameObject.GetComponent<Enemy3>() != null)
            {
                collInfo.gameObject.GetComponent<Enemy3>().TakeDamage(1);
            }

            if (collInfo.gameObject.GetComponent<Enemy2>() != null)
            {
                collInfo.gameObject.GetComponent<Enemy2>().TakeDamage(1);
            }
        }
    }

    private void Facing()
    {
        if (movement < 0f && facingRight == true)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
    }

    void Jump()
    {
        if (isGround == true)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpHeight;
            rb.linearVelocity = velocity;
            isGround = false;
            animator.SetBool("Jump", true);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (maxHealth != 0)
        {
            maxHealth -= damageAmount;
            animator.SetTrigger("Damaged");
            FindAnyObjectByType<Camera>().Shake(0.2f, 4f);
        } else
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(this.gameObject.name + "died");
    }

    void PlayAnimationRun()
    {
        if (Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("Run", 1f);
        } else if (movement < 0.1f)
        {
            animator.SetFloat("Run", 0f);
        }
    }

    void PlayAnimationAttack()
    {
        animator.SetTrigger("Attack1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }

        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
