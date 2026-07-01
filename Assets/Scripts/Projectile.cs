using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Vector2 direction;

    public void Setup(Vector2 dir)
    {
        direction = dir.normalized;
        // Уничтожить снаряд через 3 секунды, если он никого не встретил
        Destroy(gameObject, 3f); 
    }

    void Update()
    {
        // Добавили Space.World, чтобы пуля летела ровно по мировой карте
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Если пуля попала во врага
        if (collision.CompareTag("Enemy")) 
        {
            // ИСПРАВЛЕНО: Теперь ищем обновленный скрипт EnemyAI вместо удаленного EnemyHealth
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            
            if (enemy != null)
            {
                // Наносим урон через новый метод во враге
                enemy.TakeDamage(10f); 
            }
            
            // Уничтожаем пулю после попадания
            Destroy(gameObject); 
        }
    }
}