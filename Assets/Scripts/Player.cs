using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public Rigidbody2D Rb;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    private bool _isGrounded;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rb.gravityScale = GameManager.GRAVITYSCALE;
    }

    private void Update()
    {
        // Handle sprite flipping
        if (Input.GetKey(KeyCode.A))
        {
            Rb.linearVelocity = new Vector2(-Speed, Rb.linearVelocity.y);
            Animator.SetBool("isMovingLeft", true);
            Animator.SetBool("isMovingRight", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rb.linearVelocity = new Vector2(Speed, Rb.linearVelocity.y);
            Animator.SetBool("isMovingLeft", false);
            Animator.SetBool("isMovingRight", true);
        }
        else
        {
            Rb.linearVelocity = new Vector2(0, Rb.linearVelocity.y);
            Animator.SetBool("isMovingLeft", false);
            Animator.SetBool("isMovingRight", false);
        }
        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Rb.gravityScale = -Rb.gravityScale;
            SpriteRenderer.flipY = !SpriteRenderer.flipY;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            GameManager.INSTANCE.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = false;
        }
    }
}
