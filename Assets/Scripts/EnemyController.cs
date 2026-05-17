using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 2f;

    private float leftBoundary;
    private float rightBoundary;
    private bool movingRight = true;

    void Start()
    {
        // Устанавливаем границы один раз при старте относительно центра платформы
        leftBoundary = transform.localPosition.x - patrolDistance;
        rightBoundary = transform.localPosition.x + patrolDistance;
    }

    void Update()
    {
        if (movingRight)
        {
            // Движение вправо
            transform.localPosition += Vector3.right * speed * Time.deltaTime;

            // Если перешли правую границу — разворот
            if (transform.localPosition.x >= rightBoundary)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            // Движение влево
            transform.localPosition += Vector3.left * speed * Time.deltaTime;

            // Если перешли левую границу — разворот
            if (transform.localPosition.x <= leftBoundary)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}