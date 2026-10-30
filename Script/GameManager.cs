using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // =========================
    // GAME SETTINGS
    // =========================

    [Header("Game Settings")]

    public int targetCoins = 15;

    public float timeLimit = 60f;

    // If Player goes below this Y position,
    // the player loses the game.
    public float fallYLimit = -5f;


    // =========================
    // UI
    // =========================

    [Header("UI")]

    public TMP_Text scoreText;

    public TMP_Text timerText;

    public GameObject endPanel;

    public TMP_Text resultText;


    // =========================
    // PLAYER
    // =========================

    [Header("Player")]

    public PlayerMovement playerMovement;


    // =========================
    // AUDIO
    // =========================

    [Header("Audio")]

    public AudioSource audioSource;

    public AudioClip coinSound;

    public AudioClip winSound;

    public AudioClip loseSound;


    // =========================
    // PRIVATE VARIABLES
    // =========================

    private int score = 0;

    private float timeRemaining;

    private bool gameEnded = false;


    // =========================
    // AWAKE
    // =========================

    void Awake()
    {
        Instance = this;
    }


    // =========================
    // START
    // =========================

    void Start()
    {
        // Start timer
        timeRemaining = timeLimit;

        // Hide End Panel when game starts
        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        // Update UI
        UpdateScoreUI();

        UpdateTimerUI();
    }


    // =========================
    // UPDATE
    // =========================

    void Update()
    {
        // If game already ended,
        // don't continue the game.
        if (gameEnded)
        {
            return;
        }


        // =========================
        // CHECK PLAYER FALL
        // =========================

        if (playerMovement != null)
        {
            if (playerMovement.transform.position.y < fallYLimit)
            {
                EndGame(false, "YOU FELL!\nTry Again!");

                return;
            }
        }


        // =========================
        // TIMER
        // =========================

        timeRemaining -= Time.deltaTime;


        // Time finished
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            UpdateTimerUI();

            EndGame(false);

            return;
        }


        // Update timer on screen
        UpdateTimerUI();
    }


    // =========================
    // ADD COIN
    // =========================

    public void AddCoin()
    {
        // Don't collect coins after game ends
        if (gameEnded)
        {
            return;
        }


        // Increase score
        score++;


        // Update score text
        UpdateScoreUI();


        // Play coin sound
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }


        // Check if player collected
        // all required coins
        if (score >= targetCoins)
        {
            EndGame(true);
        }
    }


    // =========================
    // SCORE UI
    // =========================

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + score + " / " + targetCoins;
        }
    }


    // =========================
    // TIMER UI
    // =========================

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " +
                Mathf.CeilToInt(timeRemaining);
        }
    }


    // =========================
    // END GAME
    // =========================

    void EndGame(bool won, string customMessage = null)
    {
        // Prevent EndGame from running twice
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;


        // Show End Panel
        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }


        // =========================
        // RESULT MESSAGE
        // =========================

        if (resultText != null)
        {
            if (customMessage != null)
            {
                resultText.text = customMessage;
            }
            else
            {
                if (won)
                {
                    resultText.text =
                        "YOU WIN!\nCampus Quest Complete!";
                }
                else
                {
                    resultText.text =
                        "TIME'S UP!\nTry Again!";
                }
            }
        }


        // =========================
        // STOP PLAYER
        // =========================

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }


        // =========================
        // PLAY SOUND
        // =========================

        if (audioSource != null)
        {
            AudioClip soundToPlay;

            if (won)
            {
                soundToPlay = winSound;
            }
            else
            {
                soundToPlay = loseSound;
            }


            if (soundToPlay != null)
            {
                audioSource.PlayOneShot(soundToPlay);
            }
        }
    }


    // =========================
    // RESTART GAME
    // =========================

    public void RestartGame()
    {
        // Make sure time is normal
        Time.timeScale = 1f;


        // Reload current scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}