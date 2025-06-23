using UnityEngine;
using TMPro;
using NUnit.Framework;

public class Score : MonoBehaviour
{

    public static Score Instance;

    [SerializeField] private TextMeshProUGUI scoreText, scoreText2;
    [SerializeField] private TextMeshProUGUI statusText;
    private int currentScore = 0;
    private int currentScore2 = 0;

    public bool IsAdvantagePlayer1 = false;
    public bool IsAdvantagePlayer2 = false;
    public int CurrentScore => currentScore;
    public int CurrentScore2 => currentScore2;
    private void Awake()
    {
        // Singleton para acceso global
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        IsAdvantagePlayer2 = false; // reinicia ventaja del otro jugador
        UpdateScoreUI();
        UpdateStatusText();
    }
    public void PutScore(int amount)
    {
        currentScore2 += amount;
        IsAdvantagePlayer1 = false; // reinicia ventaja del otro jugador
        UpdateScoreUI2();
        UpdateStatusText();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "PlayerA: " + currentScore;
    }
    private void UpdateScoreUI2()
    {
        if (scoreText2 != null)
            scoreText2.text = "PlayerB: " + currentScore2;
    }
    private void UpdateStatusText()
    {
        if (currentScore >= 40 && currentScore2 >= 40)
        {
            if (IsAdvantagePlayer1)
            {
                scoreText.text = "VentajaA ";
            }
            else if (IsAdvantagePlayer2)
            {
                scoreText2.text = "VentajaB";
            }
            else
            {
                statusText.text = "Deuce";
            }
        }
        else
        {
            statusText.text = ""; // ocultar texto si no aplica
        }
    }
    
}