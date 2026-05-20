using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject winPanel;

    bool gameOver = false;
    bool paused = false;
    bool gameWon = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetCursor(true);
    }

    void Update()
    {
        // LOSE -> click to main menu
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        // WIN -> click to main menu
        if (gameWon && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        // PAUSE toggle
        if (!gameOver && !gameWon && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ContinueGame();
            else
                PauseGame();
        }
    }

    // ---------------- LEVEL FLOW ----------------

    public void LevelCompleted()
    {
        NextLevel();
    }

    void NextLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }

    public void WinGame()
    {
        if (gameWon) return;

        gameWon = true;

        winPanel.SetActive(true);
        Time.timeScale = 0f;

        SetCursor(true);
    }

    public void LoseGame()
    {
        if (gameOver) return;

        gameOver = true;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        SetCursor(true);
    }

    // ---------------- PAUSE ----------------

    void PauseGame()
    {
        paused = true;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        SetCursor(true);
    }

    public void ContinueGame()
    {
        paused = false;

        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        SetCursor(false);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

        SetCursor(true);
    }

    // ---------------- CURSOR ----------------

    void SetCursor(bool visible)
    {
        Cursor.visible = visible;

        if (visible)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}