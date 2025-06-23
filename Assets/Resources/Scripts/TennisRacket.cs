using UnityEngine;
using TMPro;
using System.Collections;

public class TennisRacket : MonoBehaviour
{
    [Header("Potencia del golpe")]
    public float hitPowerMultiplier = 2f;
    public AudioSource hitSound;

    private Vector3 lastPosition;
    private Vector3 racketVelocity;

    [SerializeField] private TextMeshProUGUI hitText;

    private void Start()
    {
        lastPosition = transform.position;
        hitText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Calcular la velocidad del movimiento de la raqueta por frame
        racketVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Direccion del golpe
                Vector3 hitDirection = racketVelocity.normalized;

                // Magnitud de la fuerza
                float hitForce = racketVelocity.magnitude * hitPowerMultiplier;

                // Aplicar impulso a la bola
                ballRb.linearVelocity = hitDirection * hitForce;

                if (hitSound != null)
                    hitSound.Play();
                ShowHit("¡Good!");
            }
        }
    }
    private void ShowHit(string message)
    {
        if (hitText != null)
        {
            hitText.text = message;
            hitText.gameObject.SetActive(true);
            StartCoroutine(HideFeedbackAfterDelay(0.5f));
        }
    }
    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hitText != null)
            hitText.gameObject.SetActive(false);
    }
}


