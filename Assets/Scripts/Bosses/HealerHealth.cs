using UnityEngine;

public class HealerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        anim.SetTrigger("die");
        GetComponent<Healer>().enabled = false; // Healer davranýþýný durdur
        this.enabled = false;
    }

    public bool IsDead()
    {
        return dead;
    }
}
