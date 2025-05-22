using UnityEngine;
using System.Collections;

public class StarSpikeReverse : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 4f;
    public float waitTime = .5f;
    private Vector3 _startPos;
    private Vector3 _endPos;

    private void Start()
    {
        _startPos = transform.position;
        _endPos = _startPos + Vector3.up * distance;
        StartCoroutine(MoveSpike());
    }

    private IEnumerator MoveSpike()
    {
        while (true)
        {
            // Move down
            yield return MoveToPosistion(_endPos);
            yield return new WaitForSeconds(waitTime);

            // Move up
            yield return MoveToPosistion(_startPos);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToPosistion(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos; // Ensure we set the position to the target
    }
}
