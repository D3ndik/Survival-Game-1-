using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ссылки")]
    public GameObject enemyPrefab; // Префаб врага
    public Transform player;       // Ссылка на трансформ игрока

    [Header("Настройки спавна")]
    public float spawnRadius = 12f;     // Расстояние от игрока (за пределами экрана)
    public float waveInterval = 10f;    // Интервал между волнами (в секундах)
    
    [Header("Настройки сложности")]
    public int baseEnemyCount = 5;      // Сколько врагов в первой волне
    public int enemiesPerWaveIncrease = 3; // На сколько больше врагов будет в каждой следующей волне

    private float timer;
    private int currentWave = 1;

    void Start()
    {
        // Сразу спавним первую волну при старте игры
        SpawnWave();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Если время пришло — запускаем новую волну
        if (timer >= waveInterval)
        {
            SpawnWave();
            timer = 0f;
        }
    }

    void SpawnWave()
    {
        if (player == null) return;

        // Рассчитываем, сколько врагов нужно заспавнить в этой волне
        int enemiesToSpawn = baseEnemyCount + (currentWave - 1) * enemiesPerWaveIncrease;

        Debug.Log($"<color=cyan>Запуск волны #{currentWave}! Спавним врагов: {enemiesToSpawn}</color>");

        // Цикл, который спавнит всю группу врагов одновременно
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnSingleEnemy();
        }

        // Переходим к следующей волне (увеличиваем сложность для будущего спавна)
        currentWave++;
    }

    void SpawnSingleEnemy()
    {
        // 1. Получаем случайное направление (вектор) на окружности
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // 2. Считаем позицию: позиция игрока + смещение на радиус спавна
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * spawnRadius;

        // 3. Создаем врага в этой точке
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}