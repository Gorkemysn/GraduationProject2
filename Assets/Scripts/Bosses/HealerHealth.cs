using UnityEngine;

public class HealerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

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
        GetComponent<Healer>().enabled = false; // Healer davranışını durdur
        this.enabled = false;
    }

    public bool IsDead()
    {
        return dead;
    }
}
