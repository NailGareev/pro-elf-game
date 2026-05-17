using UnityEngine;
using TMPro; // Используем TextMeshPro для красивого текста

public class CrystalManager : MonoBehaviour
{
    public static CrystalManager instance; // Ссылка на менеджер, чтобы к нему легко было обращаться

    public TextMeshProUGUI crystalText; // Ссылка на компонент текста на экране
    private int crystalCount = 0;       // Текущее количество кристаллов

    private void Awake()
    {
        // Делаем синглтон, чтобы скрипт был один на всю сцену
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    // Метод, который будут вызывать кристаллы при сборке
    public void AddCrystal()
    {
        crystalCount++;
        UpdateUI();
    }

    // Обновление текста на экране
    private void UpdateUI()
    {
        crystalText.text = "Кристаллы: " + crystalCount.ToString();
    }
}