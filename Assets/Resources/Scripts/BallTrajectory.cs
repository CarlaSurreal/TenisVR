using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallTrajectory : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool correcting = false;

    [Header("Corrección de precisión")]
    public float correctionRadius = 0.3f;
    public float minVelocityToStop = 0.5f;
    public float snapDelay = 0.05f;
    private float lifeTime = 4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Destroy(gameObject, lifeTime);
    }

    public void ApplyLaunchVelocity(Vector3 velocity, Vector3 target)
    {
        targetPosition = target;
        correcting = true;

        rb.isKinematic = false;
        rb.linearVelocity = velocity;
    }

    void FixedUpdate()
    {
        if (!correcting) return;

        float distance = Vector3.Distance(transform.position, targetPosition);
        float speed = rb.linearVelocity.magnitude;

        if (distance < correctionRadius && speed < minVelocityToStop)
        {
            StartCoroutine(SnapToTarget());
            correcting = false;
        }
    }

    System.Collections.IEnumerator SnapToTarget()
    {
        yield return new WaitForSeconds(snapDelay); // pequeña pausa opcional
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        transform.position = targetPosition;
    }
}

