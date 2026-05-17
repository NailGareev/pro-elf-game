using UnityEngine;
using System.Collections;

public class EnemyAttacking : MonoBehaviour
{
    [Header("Настройки здоровья монстра")]
    public int health = 3;

    [Header("Настройки дистанции")]
    public Transform player;
    public float chaseDistance = 5f;
    public float attackDistance = 1.2f;

    [Header("Настройки урона")]
    public int damageAmount = 1;
    public float attackCooldown = 1.5f;
    private float nextAttackTime;

    [Header("Состояние")]
    public bool isAngry = false;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < chaseDistance)
        {
            if (!isAngry)
            {
                isAngry = true;
                if (anim != null) anim.SetBool("isAttacking", true);
            }

            // Логика атаки героя
            if (distToPlayer <= attackDistance)
            {
                if (Time.time >= nextAttackTime)
                {
                    AttackHero();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        else if (isAngry)
        {
            isAngry = false;
            if (anim != null) anim.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкнулись именно с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ищем скрипт HeroHealth на игроке и вызываем метод получения урона
            HeroHealth playerHealth = collision.gameObject.GetComponent<HeroHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Отнимаем 1 сердечко
            }
        }
    }

    // Враг бьет героя
    void AttackHero()
    {
        HeroHealth heroHealth = player.GetComponent<HeroHealth>();
        if (heroHealth != null)
        {
            heroHealth.TakeDamage(damageAmount);
            Debug.Log("Враг укусил героя!");
        }
    }

    // Враг получает урон от меча героя
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Попадание по монстру! Осталось: " + health);
        StartCoroutine(DamageEffect());

        if (health <= 0) Die();
    }

    IEnumerator DamageEffect()
    {
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    void Die()
    {
        Debug.Log("Монстр погиб");
        Destroy(gameObject);
    }
}