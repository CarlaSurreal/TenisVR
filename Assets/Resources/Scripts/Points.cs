using UnityEngine;

public class Points : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            AutoDestroy ballState = other.GetComponent<AutoDestroy>();

            if (CompareTag("wall"))
            {
                if (ballState != null && !ballState.touchedFloor)
                {
                    // Solo suma si no tocó el piso
                    Score.Instance.AddScore(1);
                }
            }
            else if (CompareTag("floor"))
            {
                if (ballState != null && !ballState.touchedFloor)
                {
                    ballState.touchedFloor = true;

                    // Sumar punto por tocar el piso (otro contador si quieres)
                    Score.Instance.AddScore(1);
                }
            }
        }   
    }
}
