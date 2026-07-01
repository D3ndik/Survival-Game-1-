using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Сюда перетащим префаб снаряда
    public float fireRate = 1f; // Раз в сколько секунд стрелять
    public float attackRange = 10f; // Радиус поиска врагов
    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Fire(target);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= attackRange)
            {
                minDistance = distance;
                closest = enemy.transform.transform;
            }
        }
        return closest;
    }

    void Fire(Transform target)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = target.position - transform.position;
        proj.GetComponent<Projectile>().Setup(direction);
    }
}