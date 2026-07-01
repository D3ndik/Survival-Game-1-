using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Mobile Controls")]
    // Добавляем переменную для связи с экранным джойстиком
    public Joystick joystick; 

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;

    [Header("Weapon Settings")]
    public GameObject weaponPrefab; // Префаб автоматического оружия
    private List<GameObject> activeWeapons = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Спавним первое стартовое оружие при начале игры
        AddAdditionalWeapon();
    }

    void Update()
    {
        // Проверяем, назначен ли джойстик, чтобы избежать других ошибок
        if (joystick != null)
        {
            float moveX = joystick.Horizontal;
            float moveY = joystick.Vertical;

            // Записываем значения в moveInput для FixedUpdate
            moveInput = new Vector2(moveX, moveY);
        }
        else
        {
            // Если джойстик на сцене еще не привязан, оставляем управление с клавиатуры для тестов на ПК
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(moveX, moveY);
        }
    }

    void FixedUpdate()
    {
        // Движение физического тела игрока
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Метод получения урона от врагов
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Логика добавления нового оружия при Level Up
    public void AddAdditionalWeapon()
    {
        if (weaponPrefab != null)
        {
            // Создаем новое оружие как дочерний объект игрока
            GameObject newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
            activeWeapons.Add(newWeapon);

            // Если это не первое оружие, немного смещаем его, чтобы выстрелы не накладывались
            if (activeWeapons.Count > 1)
            {
                float offset = (activeWeapons.Count - 1) * 0.3f;
                newWeapon.transform.localPosition = new Vector3(offset, 0f, 0f);
            }
        }
    }

    // Лечение игрока
    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // Логика смерти игрока
    private void Die()
    {
        // Вызов Game Over экрана через GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}