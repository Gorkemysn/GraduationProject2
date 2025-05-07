using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillButton : MonoBehaviour
{
    private bool isSkillSelected = false;  // Yetenek seçildi mi?
    private bool isSkillSelectable = false;  // Yetenek seçilebilir mi?

    public int soulCost = 20;  // Yetenek maliyeti
    public int healthIncrease = 0;  // Saðlýk artýþý
    public int damageIncrease = 0;  // Hasar artýþý
    public int damage2Increase = 0;  // Hasar artýþý

    public List<SkillButton> nextSkills; // Baðlý sonraki skilller

    public Color lockedColor = Color.gray; // Kilitli skill rengi
    public Color selectableColor = Color.white; // Seçilebilir skill rengi
    public Color selectedColor = Color.green; // Seçilen skill rengi

    private Image skillImage; // Skill düðmesinin görseli
    private Health playerHealth;
    private PlayerAttack playerAttack;

    void Start()
    {
        skillImage = GetComponent<Image>();
        playerHealth = FindAnyObjectByType<Health>();
        playerAttack = FindAnyObjectByType<PlayerAttack>();

        // Skill baþlangýç durumunu güncelle
        UpdateSkillVisual();

        // Baþlangýçta sadece 1. seviyedeki skill'leri seçilebilir yap
        if (gameObject.name == "HealthSkill_1" || gameObject.name == "LightDamageSkill_1" || gameObject.name == "HeavyDamageSkill_1")
        {
            SetSelectable(); // Bu skill'ler seçilebilir
        }
    }

    void Update()
    {
        // Seçim yapýlamazsa, baþlangýçta kilitli renk gösterilsin.
        if (!isSkillSelectable && !isSkillSelected)
        {
            skillImage.color = lockedColor;
        }
    }

    public void OnClick()
    {
        // Eðer skill seçilebilir deðilse veya zaten seçildiyse iþlem yapýlmaz.
        if (!isSkillSelectable || isSkillSelected)
        {
            Debug.Log("Bu yetenek seçilemez!");
            return;
        }

        TrySelectSkill();
    }

    private void TrySelectSkill()
    {
        if (SoulManager.instance == null)
        {
            Debug.LogError("SoulManager bulunamadý!");
            return;
        }

        // Altýn kontrolü
        if (SoulManager.instance.CurrentSoul >= soulCost)
        {
            SoulManager.instance.AddSouls(-soulCost);  // Altýný düþür

            // Yetenek etkilerini uygula
            ApplySkillEffects();

            isSkillSelected = true; // Yetenek seçildi
            Debug.Log("Yetenek baþarýyla seçildi!");

            // Baðlý skilleri aç
            SelectSkill();
        }
        else
        {
            Debug.Log("Yeterli altýn yok!");
        }
    }

    private void ApplySkillEffects()
    {

        // Saðlýk ve stamina artýþý
        if (healthIncrease > 0)
        {
            playerHealth.maxHealth += healthIncrease;
            playerHealth.currentHealth += healthIncrease;  // Mevcut saðlýðý artýr
        }

        // Hasar artýþý
        if (damageIncrease > 0)
        {
            playerAttack.playerDamage += damageIncrease;
        }

        if (damage2Increase > 0)
        {
            playerAttack.playerDamage2 += damage2Increase;
        }
    }

    private void SelectSkill()
    {
        UpdateSkillVisual(); // Mevcut skilli güncelle

        // Baðlý skill'leri aç
        foreach (SkillButton nextSkill in nextSkills)
        {
            nextSkill.SetSelectable();
        }
    }

    public void SetSelectable()
    {
        isSkillSelectable = true; // Skill seçilebilir oldu
        UpdateSkillVisual();
    }

    private void UpdateSkillVisual()
    {
        if (isSkillSelected)
        {
            skillImage.color = selectedColor; // Yetenek seçilmiþ rengi
        }
        else if (isSkillSelectable)
        {
            skillImage.color = selectableColor; // Seçilebilir rengi
        }
        else
        {
            skillImage.color = lockedColor; // Kilitli rengi
        }
    }
}
