using UnityEngine;

public class Pointers : MonoBehaviour
{
    public enum ContactType { Floor, Fuera, Red, Wall }

    [Header("Define el tipo de este objeto")]
    public ContactType contactType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        AutoDestroy ballState = other.GetComponent<AutoDestroy>();
        if (ballState == null) return;

        switch (contactType)
        {
            case ContactType.Wall:
                if (!ballState.touchedFloor)
                {
                    if (Score.Instance.CurrentScore < 30)
                    {
                        Score.Instance.AddScore(15);
                    }
                    else if(Score.Instance.CurrentScore >= 30 && Score.Instance.CurrentScore < 40)
                    {
                        Score.Instance.AddScore(10);
                    }
                    else if(Score.Instance.CurrentScore == 40)
                    {
                        Score.Instance.AddScore(1);
                    }
                }
                break;
            case ContactType.Floor:
                if (!ballState.touchedFloor)
                {
                    ballState.touchedFloor = true;
                    if (Score.Instance.CurrentScore < 30)
                    {
                        Score.Instance.AddScore(15);
                    }
                    else if(Score.Instance.CurrentScore >= 30 && Score.Instance.CurrentScore < 40)
                    {
                        Score.Instance.AddScore(10);
                    }
                    else if(Score.Instance.CurrentScore == 40)
                    {
                        Score.Instance.AddScore(1);
                    }
                }
                break;
            case ContactType.Fuera:
                if (!ballState.touchedFloor )
                {
                    if (Score.Instance.CurrentScore2 < 30)
                    {
                        Score.Instance.PutScore(15);
                    }
                    else if(Score.Instance.CurrentScore2 >= 30 && Score.Instance.CurrentScore2 < 40)
                    {
                        Score.Instance.PutScore(10);
                    }
                    else if(Score.Instance.CurrentScore2 == 40)
                    {
                        Score.Instance.PutScore(1);
                    }
                }
                break;
                case ContactType.Red:
                if (!ballState.touchedFloor)
                {
                    if (Score.Instance.CurrentScore2 < 30)
                    {
                        Score.Instance.PutScore(15);
                    }
                    else if(Score.Instance.CurrentScore2 >= 30 && Score.Instance.CurrentScore2 < 40)
                    {
                        Score.Instance.PutScore(10);
                    }
                    else if(Score.Instance.CurrentScore2 == 40)
                    {
                        Score.Instance.PutScore(1);
                    }
                }
                break;
        }
    }
}
