using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;

    public bool touchedFloor = false;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
