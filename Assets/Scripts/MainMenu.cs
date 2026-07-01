using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно для работы со сценами

public class MainMenu : MonoBehaviour
{
    // Метод для запуска игры
    public void PlayGame()
    {
        // Загружает следующую сцену по индексу в очереди (или можно написать имя сцены в кавычках, например "GameScene")
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Метод для выхода из игры
    public void ExitGame()
    {
        Debug.Log("Выход из игры выполнен!"); // Будет видно в консоли Unity
        Application.Quit(); // Закрывает скомпилированную игру
    }
}