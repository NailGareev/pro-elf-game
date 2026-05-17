using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Сколько ударов выдержит монстр
    public int health = 3;

    // ПЕРЕМЕННАЯ ДЛЯ ПАМЯТИ: здесь монстр запомнит, где стоял
    private Vector3 startPosition;

    void Start()
    {
        // Запоминаем начальную позицию при старте игры
        startPosition = transform.position;
    }

    // ТОТ САМЫЙ МЕТОД, который "горел красным" в скрипте героя
    public void ResetPosition()
    {
        // 1. Возвращаем монстра в начало
        transform.position = startPosition;

        // 2. ВОССТАНАВЛИВАЕМ ЗДОРОВЬЕ (добавь эту строку)
        health = 3;

        // 3. Обнуляем физику
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Враг окончательно повержен!");
        }
    }
}