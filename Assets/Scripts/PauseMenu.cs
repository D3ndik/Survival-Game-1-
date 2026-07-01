using UnityEngine;
using UnityEngine.SceneManagement; // Нужно для перехода в Главное меню

public class PauseMenu : MonoBehaviour
{
    [Header("Настройки UI")]
    // Ссылка на объект панели паузы, который мы создали в Шаге 1
    public GameObject pauseMenuUI; 

    // Ссылка на имя сцены Главного меню. Впиши его в Инспекторе.
    public string mainMenuSceneName = "MainMenu"; 

    // Логическая переменная, чтобы знать, находится ли игра на паузе
    public static bool IsGamePaused = false;

    void Update()
    {
        // Отслеживаем нажатие клавиши для паузы. Обычно это Esc.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume(); // Если на паузе — продолжаем
            }
            else
            {
                Pause(); // Если не на паузе — ставим
            }
        }
    }

    // Метод для продолжения игры (выхода из паузы)
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Выключаем панель паузы
        Time.timeScale = 1f;        // Восстанавливаем нормальное течение времени
        IsGamePaused = false;       // Обновляем состояние
        Debug.Log("Игра продолжена");
    }

    // Метод для постановки игры на паузу
    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Включаем панель паузы
        Time.timeScale = 0f;        // "Замораживаем" время
        IsGamePaused = true;       // Обновляем состояние
        Debug.Log("Игра на паузе");
    }

    // Метод для перехода в Главное меню
    public void LoadMainMenu()
    {
        // Перед загрузкой новой сцены ОБЯЗАТЕЛЬНО возвращаем время в норму
        Time.timeScale = 1f; 
        IsGamePaused = false;
        
        Debug.Log("Загрузка главного меню...");
        // Загружаем сцену с именем mainMenuSceneName
        SceneManager.LoadScene(mainMenuSceneName);
    }
}