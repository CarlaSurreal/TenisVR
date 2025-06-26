using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
   Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform != null)
        {
            transform.LookAt(cameraTransform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Solo gira en Y
        }
    }
}
