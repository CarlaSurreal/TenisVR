using UnityEngine;

public class BallDispenser : MonoBehaviour
{
   public GameObject ballPrefab;
public Transform spawnPoint;
public Transform[] targetAreas;
public float launchInterval = 3f;
public float timeInAir = 0.8f;
public AudioSource createSound;


private void Start()
{
    InvokeRepeating(nameof(LaunchBall), 1f, launchInterval);
}

void LaunchBall()
{
    Transform target = targetAreas[Random.Range(0, targetAreas.Length)];

    GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

    Vector3 velocity = CalculateLaunchVelocity(spawnPoint.position, target.position, timeInAir);

    BallTrajectory trajectory = ball.GetComponent<BallTrajectory>();
    if (trajectory != null)
    {
        trajectory.ApplyLaunchVelocity(velocity, target.position);
    }

    if (createSound != null) createSound.Play();
}

Vector3 CalculateLaunchVelocity(Vector3 origin, Vector3 target, float time)
{
    Vector3 displacement = target - origin;
    Vector3 displacementXZ = new Vector3(displacement.x, 0f, displacement.z);
    float displacementY = displacement.y;

    float vxz = displacementXZ.magnitude / time;
    float vy = (displacementY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

    Vector3 result = displacementXZ.normalized * vxz;
    result.y = vy;

    return result;
}

}

