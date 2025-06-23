using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Prefab y área de spawn")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnQuad;

    [Header("Frecuencia de aparición")]
    [SerializeField] private float spawnInterval = 2f;

    [Header("Fuerzas de lanzamiento")]
    [SerializeField] private float launchForce = 10f;      // fuerza horizontal
    [SerializeField] private float heightBoost = 5f;       // fuerza vertical

    /*[Header("Dificultad")]
    [SerializeField] private float difficultyInterval = 15f;
    //[SerializeField] private float forceIncrease = 2f;
    private float minSpawnInterval = 0.8f;*/

    private float spawnTimer = 0f;
    private float difficultyTimer = 0f;
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnBall();
            spawnTimer = 0f;
        }

       /* if (difficultyTimer >= difficultyInterval)
        {
            spawnInterval = Mathf.Max(spawnInterval - 0.2f, minSpawnInterval);
            //launchForce += forceIncrease;
            //heightBoost += forceIncrease * 0.5f;
            difficultyTimer = 0f;
            Debug.Log($"Dificultad aumentada – spawnInt: {spawnInterval:F2}s, fuerza: {launchForce:F1}, altura: {heightBoost:F1}");
        }*/
    }

    void SpawnBall()
    {
        if (spawnQuad == null || ballPrefab == null || player == null)
            return;

        // Calcula posición aleatoria dentro del Quad
        Vector3 center = spawnQuad.position;
        Vector3 right = spawnQuad.right * spawnQuad.localScale.x * 0.5f;
        Vector3 up = spawnQuad.up * spawnQuad.localScale.y * 0.5f;
        Vector3 randomPos = center +
            Random.Range(-1f, 1f) * right +
            Random.Range(-1f, 1f) * up;

        GameObject ballObj = Instantiate(ballPrefab, randomPos, Quaternion.identity);
        Rigidbody rb = ballObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Dirección al jugador
            Vector3 dir = (player.position - randomPos); //.normalized;
            Vector3 flatDir = new Vector3(dir.x, 0f, dir.z).normalized;
            
            // Añadir variación ligera
            Vector3 dev = new Vector3(
                Random.Range(-0.2f, 0.2f),
                Random.Range(-0.1f, 0.1f),
                0f
            );
            dir = (dir + dev).normalized;

            // Aplica impulso físico
            Vector3 finalForce = dir * launchForce + Vector3.up * heightBoost;
            rb.AddForce(finalForce, ForceMode.Impulse);
        }
    }
}
