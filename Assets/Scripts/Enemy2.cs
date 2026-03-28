using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Check Player")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float size_X = 4f;
    [SerializeField] private float size_Y = 2f;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Spawn Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float x_Velocity = 10f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 2;
    private bool isEnemyDead;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    [Header("Floating Text")]
    [SerializeField] private GameObject floatingTextPrefab;

    private bool facingLeft;
    private Animator animator;

    void Start()
    {
        facingLeft = true; 
        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
        isEnemyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collInfo = Physics2D.OverlapBox(checkPoint.position, new Vector2 (size_X, size_Y), 0f, whatIsPlayer);

        if (isEnemyDead)
        {
            return;
        }

        if (collInfo)
        {
            if (facingLeft && player.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3 (0f, -180f, 0f);
                facingLeft = false;
            } else if (!facingLeft && player.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingLeft = true;
            }
            Attack();
            animator.SetBool("Attack", true);
        } else
        {
            animator.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        Debug.Log("Attack arrow");
    }

    public void SpawnArrow()
    {
        GameObject tempArrowPrefab = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        tempArrowPrefab.gameObject.GetComponent<Rigidbody2D>().linearVelocity = x_Velocity * (-spawnPoint.right);
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

    private void OnDrawGizmosSelected()
    {
        if (checkPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(checkPoint.position, new Vector2(size_X, size_Y));
        }
    }

    void Die()
    {
        isEnemyDead = true;
        rb.gravityScale = 0f;
        boxCollider2D.enabled = false;
        Destroy(this.gameObject, 5f);
        Debug.Log(this.gameObject + " Dead");
        animator.SetBool("Death", true);
    }
}
