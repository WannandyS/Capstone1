using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float speed = 5f;
    private float movement;
    private bool facingRight;

    void Start()
    {
        facingRight = true;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);

        if (facingRight && movement < 0f)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        } else if (!facingRight && movement > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
    }

    public void MoveRight()
    {
        movement = 1f;
        animator.SetFloat("Run", 1f);
    }

    public void MoveLeft()
    {
        movement = -1f;
        animator.SetFloat("Run", 1f);
    }

    public void StopMove()
    {
        movement = 0f;
        animator.SetFloat("Run", 0f);
    }
}
