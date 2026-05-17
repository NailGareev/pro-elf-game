using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Настройки земли")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Настройки атаки")]
    [SerializeField] private Transform attackPoint;   // Сюда перетащи пустой объект AttackPoint
    [SerializeField] private float attackRange = 0.5f; // Радиус удара
    [SerializeField] private LayerMask enemyLayers;   // Выбери слой Enemy

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isGrounded;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Прыжок (срабатывает на Пробел или на кнопку W)
        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Атака
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Поворот спрайта
        if (moveInput > 0) sprite.flipX = false;
        else if (moveInput < 0) sprite.flipX = true;

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        // Проверка земли
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }

        // Движение
        if (isGrounded && Mathf.Abs(moveInput) < 0.05f)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        }
    }

    private void Attack()
    {
        // Если забыли привязать точку атаки — просто ничего не делаем (ошибки не будет)
        if (attackPoint == null) return;

        // Запуск анимации
        if (anim != null) anim.SetTrigger("attack");

        // Поиск врагов в зоне круга и их мгновенное удаление
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            {
                // 1. Ищем скрипт здоровья на монстре (убедись, что твой скрипт называется EnemyHealth)
                EnemyHealth monsterHealth = enemy.GetComponent<EnemyHealth>();

                if (monsterHealth != null)
                {
                    // 2. Вызываем метод получения урона
                    monsterHealth.TakeDamage(1);
                    Debug.Log("Попал по врагу! HP уменьшено.");
                }
            }
        }
    }

    private void UpdateAnimations()
    {
        if (anim == null) return;
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        anim.SetBool("isjumping", !isGrounded);
    }

    // Рисуем зоны в редакторе (Красная - земля, Желтая - атака)
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }

        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}