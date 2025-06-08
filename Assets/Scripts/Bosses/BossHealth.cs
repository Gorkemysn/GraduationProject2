using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    public float destroyDelay;
    public float currentHealth;
    public HealthBar healthBar;
    private Animator anim;
    private bool dead;

    public int soulReward = 5;

    [Header("Optional References")]
    public GameObject healer; // Healer nesnesi atanacak
    public float healerDestroyDelay = 3f; // Healer yok olma gecikmesi (Inspector'dan ayarlanabilir)

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else if (currentHealth <= 0)
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<EnemyDamage>().enabled = false;
                dead = true;

                // Healer'ý yok etme iþlemini gecikmeli baþlat
                if (healer != null)
                {
                    StartCoroutine(DestroyHealerAfterDelay());
                }
            }
            StartCoroutine(DestroyAfterDelay());
        }
    }

    IEnumerator DestroyHealerAfterDelay()
    {
        yield return new WaitForSeconds(healerDestroyDelay);
        Destroy(healer);
    }

    IEnumerator DestroyAfterDelay()
    {
        SoulManager.instance.AddSouls(soulReward);
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
