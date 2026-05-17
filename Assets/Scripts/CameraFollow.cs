using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Сюда перетащим Героя
    public float smoothSpeed = 0.125f; // Плавность движения

    // В этой переменной мы зафиксируем высоту, чтобы камера не падала
    private float fixedY;

    void Start()
    {
        // Запоминаем начальную высоту камеры
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Создаем новую позицию: X берем у героя, а Y оставляем фиксированным
            Vector3 desiredPosition = new Vector3(target.position.x, fixedY, transform.position.z);

            // Плавно перемещаем камеру к этой точке
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}