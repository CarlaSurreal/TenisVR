using UnityEngine;

public class Audios : MonoBehaviour
{
    public AudioSource hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hitSound.Play();
        }
    }
}
