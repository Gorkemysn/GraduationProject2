using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public GameObject upgradePanel;
    public PlayerMovement playerMovementScript; // Oyuncunun hareket scripti

    private bool isNearShop = false;

    void Update()
    {
        // Shop alanýndaysa ve E tuþuna bastýysa
        if (isNearShop && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = upgradePanel.activeSelf;
            upgradePanel.SetActive(!isActive);

            // Panel açýldýysa hareketi engelle, kapandýysa tekrar aç
            playerMovementScript.enabled = !upgradePanel.activeSelf;
        }

        // ESC ile paneli kapatma
        if (upgradePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            upgradePanel.SetActive(false);
            playerMovementScript.enabled = true; // Hareketi geri aç
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
            upgradePanel.SetActive(false);
            playerMovementScript.enabled = true; // Panel kapanýnca hareketi aç
        }
    }
}
