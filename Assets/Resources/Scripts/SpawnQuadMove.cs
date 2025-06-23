using UnityEngine;

public class SpawnQuadMove : MonoBehaviour
{
    [Header("Movimiento en área")]
    public float moveRangeX = 4f;
    //public float moveRangeY = 0.2f;
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer = 0f;

    void Start()
    {
        startPos = transform.position;
        PickNewTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Mover suavemente hacia el target
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // Cada cierto tiempo, elegir nuevo destino
        if (timer >= waitTime)
        {
            PickNewTarget();
            timer = 0f;
        }
    }

    void PickNewTarget()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        //float randomY = Random.Range(-moveRangeY, moveRangeY);

        targetPos = startPos + transform.right * randomX + transform.up * 0; //randomY;
    }
}
