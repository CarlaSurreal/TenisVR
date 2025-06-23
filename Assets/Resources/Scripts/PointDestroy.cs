using UnityEngine;

public class PointDestroy : MonoBehaviour
{
     public enum ContactType { Floor, Fuera, Wall }

    [Header("Define el tipo de este objeto")]
    public ContactType contactType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        switch (contactType)
        {
            case ContactType.Wall:
                {
                    if (Score.Instance.CurrentScore < 30)
                    {
                        Score.Instance.AddScore(15);
                    }
                    else if (Score.Instance.CurrentScore >= 30 && Score.Instance.CurrentScore < 40)
                    {
                        Score.Instance.AddScore(10);
                    }
                    else if (Score.Instance.CurrentScore == 40)
                    {
                        Score.Instance.AddScore(1);
                    }
                    Destroy(other.gameObject);
                }
                break;
            case ContactType.Floor:
                {
                    if (Score.Instance.CurrentScore < 30)
                    {
                        Score.Instance.AddScore(15);
                    }
                    else if (Score.Instance.CurrentScore >= 30 && Score.Instance.CurrentScore < 40)
                    {
                        Score.Instance.AddScore(10);
                    }
                    else if (Score.Instance.CurrentScore == 40)
                    {
                        Score.Instance.AddScore(1);
                    }
                    Destroy(other.gameObject);
                }
                break;
            case ContactType.Fuera:
                {
                    if (Score.Instance.CurrentScore2 < 30)
                    {
                        Score.Instance.PutScore(15);
                    }
                    else if (Score.Instance.CurrentScore2 >= 30 && Score.Instance.CurrentScore2 < 40)
                    {
                        Score.Instance.PutScore(10);
                    }
                    else if (Score.Instance.CurrentScore2 == 40)
                    {
                        Score.Instance.PutScore(1);
                    }
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}
