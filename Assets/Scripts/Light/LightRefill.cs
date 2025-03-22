using UnityEngine;

public class LightRecharge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece oyuncu dokundu�unda �al��s�n
        {
            LightController lightController = other.GetComponent<LightController>();
            if (lightController != null)
            {
                lightController.RefillLight();
            }
        }
    }
}
