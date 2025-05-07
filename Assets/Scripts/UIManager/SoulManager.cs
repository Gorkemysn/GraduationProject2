using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public static SoulManager instance;
    private int currentSoul = 0; // Altýn miktarý
    public TextMeshProUGUI soulText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateSoulUI();
    }

    public void AddSouls(int amount)
    {
        currentSoul += amount;
        UpdateSoulUI();
    }

    private void UpdateSoulUI()
    {
        soulText.text = "Souls: " + CurrentSoul.ToString();
    }

    public int CurrentSoul
    {
        get { return currentSoul; }
    }
}
