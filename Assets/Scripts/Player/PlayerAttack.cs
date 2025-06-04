using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] public float playerDamage;
    [SerializeField] public float playerDamage2;

    private Animator anim;
    private PlayerMovement playerMovement;
    private StaminaSystem stamina;
    private float timer = Mathf.Infinity;

    public float attackRange;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        stamina = GetComponent<StaminaSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timer > attack && playerMovement.canAttack())
        {
            if (!stamina.UseStamina(15)) return;
            Attack();
        }
        else if (Input.GetMouseButton(1) && timer > attack && playerMovement.canAttack())
        {
            if (!stamina.UseStamina(20)) return;
            Attack2();
        }

        timer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        timer = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(playerDamage);
        }
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");
        timer = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(playerDamage2);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
