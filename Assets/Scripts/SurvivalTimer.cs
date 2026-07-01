using UnityEngine;
using TMPro; // Обязательно для работы с TextMeshPro

public class SurvivalTimer : MonoBehaviour
{
    [Header("UI Элемент")]
    public TextMeshProUGUI timerText; // Ссылка на текст таймера

    private float elapsedTime = 0f;   // Сколько секунд прошло
    private bool isTimerRunning = true; // Активен ли таймер

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Пожалуйста, прикрепите объект TimerText в Инспекторе!");
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Прибавляем время, прошедшее за последний кадр
            elapsedTime += Time.deltaTime;

            // Обновляем визуальное отображение текста
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        // Вычисляем минуты и секунды
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Форматируем строку, чтобы всегда было две цифры (например, 02:05 вместо 2:5)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Метод для остановки таймера (вызовем его, когда игрок умрет)
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Метод для получения текущего времени (если понадобится для аналитики или рекордов)
    public float GetTime()
    {
        return elapsedTime;
    }
}