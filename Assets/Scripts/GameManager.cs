using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Нужно для перезапуска сцены
using TMPro;

public class GameManager : MonoBehaviour
{
    // Статическая ссылка на GameManager, чтобы EnemyAI мог легко к нему обращаться
    public static GameManager instance;

    [Header("Score & Progress")]
    public int killCount = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 100;
    private int currentXP = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI killsText;
    public Slider xpSlider;

    [Header("Game Over UI")]
    public GameObject gameOverScreen; // Сюда перетаскиваем панель GameOverScreen
    public TextMeshProUGUI statsText;   // Сюда перетаскиваем текст статистики StatsText

    private PlayerController player;

    void Awake()
    {
        // Настраиваем синглтон
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        UpdateUI();
    }

    // Метод, вызываемый при убийстве врага
    public void AddKill()
    {
        killCount++;
        AddXP(1); // Добавляем 1 единицу опыта за убийство
    }

    // Метод добавления опыта
    public void AddXP(int amount)
    {
        currentXP += amount;

        // Используем while вместо if на случай переполнения опыта
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI(); // Обновляем интерфейс после изменения опыта
    }

    // Повышение уровня
    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        
        // Мягкое увеличение сложности (например, на 50% больше с каждым уровнем)
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f); 

        if (player != null)
        {
            player.AddAdditionalWeapon(); // Добавляем новое оружие игроку
            player.RestoreHealth();       // Лечим игрока при Level Up
        }
    }

    // Обновление интерфейса во время игры
    void UpdateUI()
    {
        if (killsText != null)
        {
            killsText.text = "Kills: " + killCount.ToString();
        }

        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }
    }

    // Метод вызывается из PlayerController, когда здоровье игрока падает до 0
    public void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Показываем экран смерти
        }

        Time.timeScale = 0f; // Полностью останавливаем время в игре (пауза)

        if (statsText != null)
        {
            // Выводим финальную статистику на экран
            statsText.text = "Kills: " + killCount.ToString() + "\nReached Level: " + currentLevel.ToString();
        }
    }

    // Метод для кнопки "Заново"
    public void RestartGame()
    {
        Time.timeScale = 1f; // Обязательно возвращаем время в норму!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезапускаем текущую сцену
    }
}