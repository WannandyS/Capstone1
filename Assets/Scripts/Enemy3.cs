using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;

    [Header ("Ground Point")]
    [SerializeField] private Transform detectPoint;
    [SerializeField] private float distance = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    private bool facingLeft;
    private bool playerAttackRange;
    private Animator animator;
    private bool isEnemyDead;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    [Header ("Detect Player")]
    [SerializeField] private Transform centerPosition;
    [SerializeField] private float size_X = 2f;
    [SerializeField] private float size_Y = 2f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float chaseSpeed = 2.5f;
    [SerializeField] private float retrieveDistance = 1.5f;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Floating Text")]
    [SerializeField] private GameObject floatingTextPrefab;
    void Start()
    {
        facingLeft = true;
        isEnemyDead = false;
        playerAttackRange = false;

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectingGround();
        BoxColliderDetection();

        if (isEnemyDead)
        {
            return;
        }

        if (playerAttackRange)
        {
            FlipEnemyInPlayerDirection();
            Vector2 targetPos = new Vector2(playerPosition.position.x, transform.position.y);

            if (Vector2.Distance(transform.position, playerPosition.position) > retrieveDistance)
            {
                animator.SetBool("Attack", false);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
            } else
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    animator.SetBool("Attack", true);
                }
                else
                {
                    animator.SetBool("Attack", false);
                }
            }
        }   
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetLayer);

        if (collInfo.GetComponent<Player>() != null)
        {
            collInfo.GetComponent<Player>().TakeDamage(1);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (maxHealth != 0)
        {
            maxHealth -= damageAmount;
            animator.SetTrigger("Hurt");
            
        } else
        {
            Die();
            GameObject tempFloatingTextPrefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            Destroy(tempFloatingTextPrefab, 2);
        }
    }

    void Die()
    {
        isEnemyDead = true;
        rb.gravityScale = 0f;
        boxCollider2D.enabled = false;
        Destroy(this.gameObject, 5f);
        Debug.Log(this.gameObject.name + "Dead");
        animator.SetBool("Death", true);
    }

    void FlipEnemyInPlayerDirection()
    {
        if (facingLeft && playerPosition.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingLeft = false;
        } else if (!facingLeft && playerPosition.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingLeft = true;
        }
    }

    void DetectingGround()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(detectPoint.position, Vector2.down, distance, whatIsGround);

        if (hitInfo == false)
        {
            if (facingLeft)
            {
                transform.eulerAngles = new Vector3 (0f, -180f, 0f);
                facingLeft = false;
            } else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingLeft = true;
            }
        }
    }

    void BoxColliderDetection()
    {
        Collider2D collInfo = Physics2D.OverlapBox(centerPosition.position, new Vector2(size_X, size_Y), 0f, whatIsPlayer);

        if (collInfo != null)
        {
            playerAttackRange = true;
        }
        else
        {
            playerAttackRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(detectPoint.position, Vector2.down * distance);
        }

        if (centerPosition != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(centerPosition.position, new Vector2(size_X, size_Y));
        }

        if (attackPoint  != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
