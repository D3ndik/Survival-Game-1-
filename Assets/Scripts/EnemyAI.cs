using UnityEngine;
using UnityEngine.UI; // Обязательно для работы со слайдером здоровья

public class EnemyAI : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 2f;
    private Transform player;

    [Header("Здоровье")]
    public float maxHealth = 30f;
    private float currentHealth;
    public Slider healthSlider; // Ссылка на Slider над головой врага

    [Header("Атака")]
    public float damageToPlayer = 10f; // Сколько урона враг наносит игроку

    void Start()
    {
        // Находим игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Инициализируем здоровье
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        // Движение в сторону игрока
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // МЕТОД ПОЛУЧЕНИЯ УРОНА (его вызывает пуля)
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Обновляем ползунок здоровья над головой
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Если здоровье закончилось — враг погибает
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Пытаемся добавить фраг в GameManager (если он создан в проекте)
        if (GameManager.instance != null)
        {
            GameManager.instance.AddKill();
        }

        // Уничтожаем объект врага
        Destroy(gameObject);
    }

    // НАНЕСЕНИЕ УРОНА ИГРОКУ ПРИ СТОЛКНОВЕНИИ
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Если враг врезался в игрока
        if (collision.CompareTag("Player"))
        {
            // Исправлено: вместо PlayerHealth ищем PlayerController
            PlayerController playerScript = collision.GetComponent<PlayerController>();
            
            if (playerScript != null)
            {
                // Наносим урон через новый главный скрипт
                playerScript.TakeDamage(damageToPlayer);
            }
        }
    }
}