using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    public DeathMenu deathMenu;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadCurrentLevelWithDelay(0.5f));
        }
    }

    private IEnumerator LoadCurrentLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (deathMenu != null)
        {
            deathMenu.ShowDeathScreen();
        }
        else
        {
            Debug.LogError("DeathMenu is not assigned in the Inspector.");
        }
    }
}
