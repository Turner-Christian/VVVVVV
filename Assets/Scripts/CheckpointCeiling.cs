using UnityEngine;

public class CheckpointCeiling : MonoBehaviour
{
    public Sprite[] CheckpointSprites;
    public SpriteRenderer SpriteRenderer;
    private int _inactiveSprite = 0;
    private int _activeSprite = 1;
    private bool _isActive = false;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = CheckpointSprites[_inactiveSprite];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActive)
        {
            GameManager.GRAVITYSCALE = -GameManager.GRAVITYSCALE;
            GameManager.UPSIDE_DOWN = true;
            GameManager.INSTANCE.CheckpointPos = transform.position;
            GameManager.INSTANCE.CameraPos = Camera.main.transform.position;
            SpriteRenderer.sprite = CheckpointSprites[_activeSprite];
            _isActive = true;
        }
    }
}
