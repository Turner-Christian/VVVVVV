using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Camera CameraController;
    public float Speed = 5f;
    public Rigidbody2D Rb;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    private bool _canMove = true;
    // private bool _isGrounded;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rb.gravityScale = GameManager.GRAVITYSCALE;
    }

    private void Update()
    {
        if (!_canMove)
            return;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rb.gravityScale = -Rb.gravityScale;
            SpriteRenderer.flipY = !SpriteRenderer.flipY;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // FIXME:
        // if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        // {
        //     _isGrounded = true;
        // }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            GameManager.INSTANCE.Death();
        }
    }

    // FIXME:
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //     {
    //         _isGrounded = false;
    //     }
    // }

    public void FreezeMovement()
    {
        StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        _canMove = false;
        yield return new WaitForSeconds(.25f);
        _canMove = true;
    }
}
