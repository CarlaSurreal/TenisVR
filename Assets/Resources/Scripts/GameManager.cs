using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuración del juego")]
    [SerializeField] private float gameDuration = 60f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI endGameText;

    [Header("Sonidos")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip timeoutSound;

    [SerializeField] private AudioClip gameOverSound;

    private float gameTimer = 0f;
    private bool gameEnded = false;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameTimer = 0f;
        if (endGameText != null)
            endGameText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameEnded) return;

        gameTimer += Time.deltaTime;

        if (gameTimer >= gameDuration)
        {
            // Fin de juego por tiempo agotado
            PlayerWins(-1); // -1 indica empate o fin por tiempo
        }
    }

    public void PlayerWins(int player)
    {
        gameEnded = true;

        // Detener el spawner de bolas
        BallSpawner spawner = FindFirstObjectByType<BallSpawner>();
        if (spawner != null)
            spawner.enabled = false;

        // Mostrar mensaje adecuado
        string message = "";
        AudioClip sound = null;

        if (player == 1)
        {
            message = "¡GANASTE!";
            sound = winSound;
        }
        else if (player == 2)
        {
            message = "¡PERDISTE!";
            sound = gameOverSound;
        }
        else
        {
            message = "Juego terminado";
            sound = timeoutSound;
        }

        if (endGameText != null)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = message;
        }

        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }

    public bool IsGameOver()
    {
        return gameEnded;
    }
}
