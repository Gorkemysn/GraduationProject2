using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public GameObject upgradePanel;
    public PlayerMovement playerMovementScript;

    private bool isNearShop = false;

    void Update()
    {
        if (isNearShop && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = upgradePanel.activeSelf;
            upgradePanel.SetActive(!isActive);

            playerMovementScript.enabled = !upgradePanel.activeSelf;

            Cursor.visible = upgradePanel.activeSelf;
            Cursor.lockState = upgradePanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }

        if (upgradePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            upgradePanel.SetActive(false);
            playerMovementScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ShopTrigger"))
        {
            isNearShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ShopTrigger"))
        {
            isNearShop = false;

            if (upgradePanel != null)
            {
                upgradePanel.SetActive(false);
            }

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
