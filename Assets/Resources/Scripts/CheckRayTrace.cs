using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;


public class CheckRayTrace : MonoBehaviour
{
    public LayerMask ballLayer; // Asegúrate de asignar esto en el Inspector (o usa Default)

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 direction = transform.position - lastPosition;
        float distance = direction.magnitude;

        if (distance > 0.001f)
        {
            Ray ray = new Ray(lastPosition, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, ballLayer))
            {
                if (hit.collider.CompareTag("Ball"))
                {
                    Debug.Log("¡Golpe detectado por raycast!");

                    // También puedes golpear manualmente:
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 swingVelocity = direction / Time.fixedDeltaTime;
                        Vector3 hitDir = swingVelocity.normalized;
                        hitDir.y = Mathf.Clamp(hitDir.y, -0.1f, 0.3f);

                        float force = Mathf.Min(swingVelocity.magnitude * 3f, 14f);
                        rb.linearVelocity = hitDir * force;

                        Debug.DrawRay(hit.point, hit.normal, Color.yellow, 0.5f);
                    }
                    // ✅ Vibración háptica
                    SendHapticImpulse(0.4f, 0.15f);
                }
            }
        }

        lastPosition = transform.position;
    }
    private void SendHapticImpulse(float amplitude, float duration)
{
    // Detectar en qué mano está la raqueta
    var inputDevices = new List<InputDevice>();
    InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices); // Cambia a LeftHand si tu raqueta está en la izquierda

    foreach (var device in inputDevices)
    {
        if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
        {
            device.SendHapticImpulse(0, amplitude, duration);
        }
    }
}

}
