using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float destroyDelay;
    public float currentHealth;
    private bool dead;

    private void Awake()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth > 0)
        {
         
        }
        else if (currentHealth <= 0)
        {
            if (!dead)
            {
                
                GetComponent<EnemyDamage>().enabled = false;
                dead = true;
            }
            StartCoroutine(DestroyAfterDelay());
        }
    }
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}

