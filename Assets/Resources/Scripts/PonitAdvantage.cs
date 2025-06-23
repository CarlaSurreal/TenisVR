using UnityEngine;

public class PonitAdvantage : MonoBehaviour
{
    public enum ContactType { Fuera, Wall }

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
                    else if (Score.Instance.CurrentScore == 30)
                    {
                        Score.Instance.AddScore(10);
                    }
                    else if (Score.Instance.CurrentScore == 40)
                    {
                        if (Score.Instance.CurrentScore2 < 40)
                        {
                            // Jugador 1 gana el game directamente
                            GameManager.Instance.PlayerWins(1);
                        }
                        else
                        {
                            if (Score.Instance.IsAdvantagePlayer1)
                            {
                                GameManager.Instance.PlayerWins(1);
                            }
                            else if (Score.Instance.IsAdvantagePlayer2)
                            {
                                // Vuelve a deuce
                                Score.Instance.IsAdvantagePlayer2 = false;
                            }
                            else
                            {
                                // Ventaja para el jugador 1
                                Score.Instance.IsAdvantagePlayer1 = true;
                            }
                        }
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
                    else if (Score.Instance.CurrentScore2 == 30)
                    {
                        Score.Instance.PutScore(10);
                    }
                    else if (Score.Instance.CurrentScore2 == 40)
                    {
                        if (Score.Instance.CurrentScore < 40)
                        {
                            GameManager.Instance.PlayerWins(2);
                        }
                        else
                        {
                            if (Score.Instance.IsAdvantagePlayer2)
                            {
                                GameManager.Instance.PlayerWins(2);
                            }
                            else if (Score.Instance.IsAdvantagePlayer1)
                            {
                                // Vuelve a deuce
                                Score.Instance.IsAdvantagePlayer1 = false;
                            }
                            else
                            {
                                // Ventaja para el jugador 2
                                Score.Instance.IsAdvantagePlayer2 = true;
                                Score.Instance.IsAdvantagePlayer2 = true;
                            }
                        }
                    }
                    Destroy(other.gameObject);
                }
                break;
        }

    }
}
