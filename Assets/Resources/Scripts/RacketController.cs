using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RacketController : MonoBehaviour
{
    [Header("Parámetros de Golpe")]
    public float powerMultiplier = 2.8f;      // Escala la velocidad de la raqueta
    public float maxSpeed = 11f;              // Velocidad máxima que puede alcanzar la bola
    public float minSwingSpeed = 0.25f;       // Umbral mínimo para que el golpe cuente
    public float upwardLimit = 0.35f;         // Máximo valor vertical permitido

    private Vector3 previousPosition;
    private Vector3 swingVelocity;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        swingVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        if (ballRb == null) return;

        float swingSpeed = swingVelocity.magnitude;

        if (swingSpeed < minSwingSpeed) return; // ignorar si mueves poco la raqueta

        // Ajustamos dirección para controlar tiros exageradamente verticales
        Vector3 direction = swingVelocity.normalized;
        direction.y = Mathf.Clamp(direction.y, -0.1f, upwardLimit);
        direction.Normalize();

        // Calculamos la velocidad final de la bola
        float finalSpeed = Mathf.Min(swingSpeed * powerMultiplier, maxSpeed);
        ballRb.linearVelocity = direction * finalSpeed;

        Debug.DrawRay(transform.position, direction * finalSpeed, Color.green, 1f);
    }
}
