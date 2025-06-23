using UnityEngine;
using TMPro;
using System.Collections;

public class RacketController : MonoBehaviour
{
    [Header("Potencia del golpe")]
    public float hitPowerMultiplier = 6f;
    public float heightBoost = 1.5f;
    private float randomness = 0.015f;
    private float spinMultiplier = 0f;

    public AudioSource hitSound;
    [SerializeField] private TextMeshProUGUI hitFeedbackText;

    private Vector3 lastPosition;
    private Vector3 racketVelocity;

    private void Start()
    {
        lastPosition = transform.position;
        if (hitFeedbackText != null)
            hitFeedbackText.gameObject.SetActive(false);
    }

    private void Update()
    {
        racketVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        Rigidbody ballRb = other.GetComponent<Rigidbody>();
        if (ballRb == null) return;

        // Dirección base
        Vector3 hitDir = racketVelocity.normalized;

        // Variación leve
        hitDir += new Vector3(
            Random.Range(-randomness, randomness),
            Random.Range(-randomness, randomness),
            Random.Range(-randomness, randomness)
        );
        hitDir = hitDir.normalized;

        // Aumento vertical
        hitDir.y += heightBoost;

        // Aplica fuerza con AddForce (respetando física)
        float force = racketVelocity.magnitude * hitPowerMultiplier;
        ballRb.AddForce(hitDir.normalized * force, ForceMode.VelocityChange);

        // Spin (opcional)
        float verticalSpeed = racketVelocity.y;
        Vector3 spinAxis = transform.right;
        float spinAmount = -verticalSpeed * spinMultiplier;
        ballRb.AddTorque(spinAxis * spinAmount, ForceMode.VelocityChange);

        // Feedback
        if (hitSound != null) hitSound.Play();
        ShowHitFeedback("Good");
    }

    private void ShowHitFeedback(string msg)
    {
        if (hitFeedbackText != null)
        {
            hitFeedbackText.text = msg;
            hitFeedbackText.gameObject.SetActive(true);
            StartCoroutine(HideFeedbackAfterDelay(0.5f));
        }
    }

    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hitFeedbackText != null)
            hitFeedbackText.gameObject.SetActive(false);
    }
}
