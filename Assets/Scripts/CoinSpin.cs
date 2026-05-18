using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    [Header("Анимация вращения")]
    public float spinSpeed = 180f; // градусов в секунду

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}
