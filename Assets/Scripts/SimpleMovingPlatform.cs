using UnityEngine;

public class SimpleMovingPlatform : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 2f;
    public float distance = 4f;
    public bool moveHorizontal = true;

    [Header("Разнообразие")]
    [Range(0, 5)] // Ползунок для удобства
    public float timeOffset = 0f; // Смещение по времени

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Добавляем timeOffset к общему времени
        float movement = Mathf.PingPong((Time.time + timeOffset) * speed, distance);

        if (moveHorizontal)
            transform.position = startPos + new Vector3(movement, 0, 0);
        else
            transform.position = startPos + new Vector3(0, movement, 0);
    }
}
