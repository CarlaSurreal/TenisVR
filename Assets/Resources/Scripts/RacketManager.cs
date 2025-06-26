using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class RacketManager : MonoBehaviour
{
    [Header("Parámetros de Golpe")]
    public float powerMultiplier = 2.8f;
    public float maxSpeed = 11f;
    public float minSwingSpeed = 0.25f;
    public float upwardLimit = 0.35f;
    public LayerMask ballLayer;

    [Header("Hápticos")]
    public XRNode controllerNode = XRNode.RightHand;
    public float hapticForce = 0.4f;
    public float hapticDuration = 0.15f;

    private Vector3 previousPosition;
    private Vector3 swingVelocity;
    private HashSet<GameObject> alreadyHitBalls = new HashSet<GameObject>();

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        swingVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 direction = transform.position - previousPosition;
        float distance = direction.magnitude;

        if (distance > 0.001f)
        {
            Ray ray = new Ray(previousPosition, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, ballLayer))
            {
                if (hit.collider.CompareTag("Ball") && !alreadyHitBalls.Contains(hit.collider.gameObject))
                {
                    ApplyHit(hit.collider.gameObject);
                    Debug.DrawRay(hit.point, hit.normal, Color.yellow, 0.5f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !alreadyHitBalls.Contains(collision.gameObject))
        {
            ApplyHit(collision.gameObject);
        }
    }

    private void ApplyHit(GameObject ball)
    {
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb == null) return;

        float swingSpeed = swingVelocity.magnitude;
        if (swingSpeed < minSwingSpeed) return;

        Vector3 direction = swingVelocity.normalized;
        direction.y = Mathf.Clamp(direction.y, -0.1f, upwardLimit);
        direction.Normalize();

        float finalSpeed = Mathf.Min(swingSpeed * powerMultiplier, maxSpeed);
        ballRb.linearVelocity = direction * finalSpeed;

        SendHapticImpulse(hapticForce, hapticDuration);

        alreadyHitBalls.Add(ball); // Evitar múltiples impactos
        Invoke(nameof(ClearHitCache), 0.25f); // Limpiar después de un tiempo
    }

    private void ClearHitCache()
    {
        alreadyHitBalls.Clear();
    }

    private void SendHapticImpulse(float amplitude, float duration)
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);

        foreach (var device in devices)
        {
            if (device.TryGetHapticCapabilities(out var capabilities) && capabilities.supportsImpulse)
            {
                device.SendHapticImpulse(0, amplitude, duration);
            }
        }
    }
}

