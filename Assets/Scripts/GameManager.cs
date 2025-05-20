using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float GRAVITYSCALE = 7f;
    public static GameManager INSTANCE;
    public GameObject PlayerPrefab;
    public GameObject Player;
    public Vector3 CheckpointPos;

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
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Respawn(CheckpointPos);
    }
}
