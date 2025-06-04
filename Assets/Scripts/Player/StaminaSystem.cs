using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float regenRate = 5f;

    [HideInInspector] public float currentStamina;

    private StaminaBar staminaBar;

    private void Awake()
    {
        staminaBar = FindObjectOfType<StaminaBar>();
        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.SetMaxStamina(maxStamina);
        }
    }

    private void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);

            if (staminaBar != null)
            {
                staminaBar.SetStamina(currentStamina);
            }
        }
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            if (staminaBar != null)
            {
                staminaBar.SetStamina(currentStamina);
            }
            return true;
        }

        return false;
    }
}
