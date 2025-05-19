using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private Vector3 spawnPosition;
    private float gravityScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    private void Update()
    {
        // Handle sprite flipping
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isMovingRight", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
        }
        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.gravityScale = -rb.gravityScale;
            gravityScale = rb.gravityScale;
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            Death();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
