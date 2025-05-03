using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public static SoulManager instance;
    public int soulCount = 0;
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
        soulCount += amount;
        UpdateSoulUI();
    }

    private void UpdateSoulUI()
    {
        soulText.text = "Souls: " + soulCount.ToString();
    }
}
