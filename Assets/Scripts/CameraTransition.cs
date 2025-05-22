using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private bool _hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasTriggered || !other.CompareTag("Player"))
            return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        if (playerRb == null) return;

        float yVelocity = playerRb.linearVelocity.y;
        float xVelocity = playerRb.linearVelocity.x;

        if (yVelocity > 0)
        {
            // Moving up
            other.GetComponent<Player>().FreezeMovement();
            Camera.main.transform.position += new Vector3(0, 16f, 0);
        }
        else if (yVelocity < 0)
        {
            // Moving down
            other.GetComponent<Player>().FreezeMovement();
            Camera.main.transform.position += new Vector3(0, -16f, 0);
        }
        else if (xVelocity < 0)
        {
            // left
            other.GetComponent<Player>().FreezeMovement();
            Camera.main.transform.position += new Vector3(-29, 0, 0);
        }
        else if (xVelocity > 0)
        {
            // right
            other.GetComponent<Player>().FreezeMovement();
            Camera.main.transform.position += new Vector3(29, 0, 0);
        }

        _hasTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _hasTriggered = false;
        }
    }
}
