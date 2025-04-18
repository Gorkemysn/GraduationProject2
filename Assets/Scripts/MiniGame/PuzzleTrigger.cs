using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzlePanel;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            puzzlePanel.SetActive(true);
            Time.timeScale = 0f; // Oyunu durdur (isteðe baðlý)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerNear = false;
    }
}
