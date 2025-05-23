using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause men�s� UI'�n� temsil eder
    private bool isPaused = false;


    private void Update()
    {
        // ESC tu�una bas�ld���nda pause men�s�n� a�/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Oyun zaman�n� duraklat
        pauseMenuUI.SetActive(true); // Pause men�s�n� g�ster
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        pauseMenuUI.SetActive(false); // Pause men�s�n� kapat
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesine geri d�n
    }

    public bool IsGamePaused()
    {
        return isPaused; // Oyun duraklat�ld� m�?
    }
}
