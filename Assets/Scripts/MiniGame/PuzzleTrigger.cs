using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    private bool isPlayerNear = false;
    private bool puzzleCompleted = false; 

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!PuzzleManager.IsPuzzleActive && !PauseMenu.IsGamePausedGlobally && !puzzleCompleted)
            {
                puzzleManager.OpenPuzzle();
            }
            else if (PuzzleManager.IsPuzzleActive)
            {
                puzzleManager.ClosePuzzleExternally(); 
                puzzleCompleted = true; 
            }
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
