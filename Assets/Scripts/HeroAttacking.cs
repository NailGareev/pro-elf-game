using UnityEngine;

public class HeroAttacking : MonoBehaviour
{
    [Header("Настройки удара")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // ПРОВЕРКА: Если ты забыла перетащить AttackPoint, код напишет об этом в консоль, а не выдаст красную ошибку
        if (attackPoint == null)
        {
            Debug.LogError("ВНИМАНИЕ: Ты забыла перетащить объект AttackPoint в инспектор героя!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformStrike();
        }
    }

    void PerformStrike()
    {
        // Если точки атаки нет, выходим
        if (attackPoint == null) return;

        if (anim != null) anim.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Ищем скрипт здоровья на враге
            EnemyHealth monsterHealth = enemy.GetComponent<EnemyHealth>();

            if (monsterHealth != null)
            {
                // Наносим 1 единицу урона
                monsterHealth.TakeDamage(1);
                Debug.Log("Урон нанесен по: " + enemy.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}