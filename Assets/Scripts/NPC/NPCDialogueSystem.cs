using UnityEngine;
using TMPro;

public class NPCDialogueSystem : MonoBehaviour
{
    public string[] dialogueLines;
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    private int currentLine = 0;
    private bool playerInRange = false;

    void Start()
    {
        dialogueUI.SetActive(false); // Baþlangýçta gizli
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogueLine();
        }
    }

    private void ShowNextDialogueLine()
    {
        if (currentLine >= dialogueLines.Length)
        {
            dialogueUI.SetActive(false);
            currentLine = 0;
            return;
        }

        dialogueUI.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
        currentLine++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueUI.SetActive(false);
            currentLine = 0;
        }
    }
}
