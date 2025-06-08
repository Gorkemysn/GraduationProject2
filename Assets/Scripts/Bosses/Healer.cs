using System.Collections;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [Header("Healing")]
    public BossHealth bossHealth;
    public float healAmount = 10f;
    public float healCooldown = 3f;
    private float healTimer;
    private Animator anim;
    private HealerHealth healerHealth;

    [Header("Teleport")]
    public Transform[] teleportPoints;
    public float teleportInterval = 5f;
    private float teleportTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        healerHealth = GetComponent<HealerHealth>();
    }

    private void Update()
    {
        if (healerHealth != null && healerHealth.IsDead())
            return;

        // Healing
        healTimer += Time.deltaTime;
        if (healTimer >= healCooldown && bossHealth != null && bossHealth.currentHealth > 0)
        {
            HealBoss();
            healTimer = 0;
        }

        // Teleport
        teleportTimer += Time.deltaTime;
        if (teleportTimer >= teleportInterval)
        {
            TeleportToRandomPoint();
            teleportTimer = 0;
        }
    }

    private void HealBoss()
    {
        if (bossHealth.currentHealth < bossHealth.maxHealth)
        {
            anim.SetTrigger("heal"); // Saldýrý animasyonunu burada "heal" gibi tetikle
            bossHealth.currentHealth = Mathf.Clamp(bossHealth.currentHealth + healAmount, 0, bossHealth.maxHealth);
            bossHealth.healthBar.SetHealth(bossHealth.currentHealth);
        }
    }

    private void TeleportToRandomPoint()
    {
        if (teleportPoints.Length == 0) return;

        int index = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[index].position;
    }
}
