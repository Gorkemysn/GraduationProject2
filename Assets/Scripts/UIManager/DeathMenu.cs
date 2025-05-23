using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathScreenUI;

    private void Start()
    {
        HideDeathScreen();
    }

    public void ShowDeathScreen()
    {
        // Oyun zaman�n� durdur
        Time.timeScale = 0f;
        deathScreenUI.SetActive(true); // �l�m ekran�n� g�ster
        Cursor.lockState = CursorLockMode.None; // Mouse'u serbest b�rak
        Cursor.visible = true; // Mouse'u g�r�n�r yap
    }

    public void HideDeathScreen()
    {
        // Oyun zaman�n� devam ettir
        Time.timeScale = 1f;
        deathScreenUI.SetActive(false); // �l�m ekran�n� kapat
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u kilitle
        Cursor.visible = false; // Mouse'u gizle
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden y�kle
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesine geri d�n
    }
}
