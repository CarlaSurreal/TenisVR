using UnityEngine;

public class BallScoring : MonoBehaviour
{
    public GameObject goodPrefab;
    public GameObject badPrefab;
    public AudioSource hitSound;
    public AudioClip[] anuncio;

    private bool hasScored = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasScored) return;

        Vector3 markPosition = transform.position;
        markPosition.y = 0.5f; // un poco sobre el suelo

        if (other.CompareTag("In"))
        {
            Instantiate(goodPrefab, markPosition, Quaternion.LookRotation(Vector3.up));
            hitSound.clip = anuncio[0];
            hitSound.Play();
            hasScored = true;
        }
        else if (other.CompareTag("Out"))
        {
            Instantiate(badPrefab, markPosition, Quaternion.LookRotation(Vector3.up));
            hitSound.clip = anuncio[1];
            hitSound.Play();
            hasScored = true;
        }
    }
}

