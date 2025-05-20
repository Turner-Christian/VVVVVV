using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    public static float GRAVITYSCALE = 7f;
    public static bool UPSIDE_DOWN = false;
    public GameObject PlayerPrefab;
    public Vector3 CheckpointPos;
    public GameObject Player;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        Player = Instantiate(PlayerPrefab, CheckpointPos, Quaternion.identity);
        CheckpointPos = new Vector3(-4, 4, 0); //TODO: needs to change at some point, just for testing
    }

    public void Death()
    {
        Destroy(Player);
        Player = null;
        StartCoroutine(RespawnCoroutine());
    }

    private void Respawn(Vector3 checkpointPos)
    {
        Player = Instantiate(PlayerPrefab, checkpointPos, Quaternion.identity);
        Player.GetComponent<Rigidbody2D>().gravityScale = GRAVITYSCALE;

        if (UPSIDE_DOWN)
        {
            Player.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            Player.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Respawn(CheckpointPos);
    }
}
