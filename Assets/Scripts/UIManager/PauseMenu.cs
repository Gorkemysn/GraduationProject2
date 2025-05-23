using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public static bool IsGamePausedGlobally = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
                ResumeGame();
            else if (!DeathMenuActive() && !PuzzleManager.IsPuzzleActive)
                PauseGame();
        }
    }

    private bool DeathMenuActive()
    {
        return Time.timeScale == 0f && Cursor.visible == true && !pauseMenuUI.activeSelf;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
        IsGamePausedGlobally = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        IsGamePausedGlobally = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}
