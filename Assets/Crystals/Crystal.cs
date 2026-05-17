using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что в кристалл врезался именно Игрок
        // Убедись, что у твоего героя стоит тег "Player"
        if (collision.CompareTag("Player"))
        {
            // Говорим менеджеру, что мы подобрали кристалл
            CrystalManager.instance.AddCrystal();

            // Удаляем кристаллик со сцены
            Destroy(gameObject);
        }
    }
}