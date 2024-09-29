using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //[SerializeField] private float _fireRate;
    //[SerializeField] private float _radius;
    //[SerializeField] private int _damage;
    //[SerializeField] private int _level;
    //[SerializeField] private Vector2Int _position;
    //// Events
    //public event Action<float> OnFireRateChanged;
    //public event Action<float> OnRadiusChanged;
    //public event Action<int> OnDamageChanged;
    //public event Action<int> OnLevelChanged;
    //public event Action<Vector2Int> OnPositionChanged;

    //#region Getter / Setter
    //public float FireRate
    //{
    //    get => _fireRate;
    //    set
    //    {
    //        _fireRate = value;
    //        OnFireRateChanged?.Invoke(value);
    //    }
    //}

    //public float Radius
    //{
    //    get => _radius;
    //    set
    //    {
    //        _radius = value;
    //        OnRadiusChanged?.Invoke(value);
    //    }
    //}

    //public int Damage
    //{
    //    get => _damage;
    //    set
    //    {
    //        _damage = value;
    //        OnDamageChanged?.Invoke(value);
    //    }
    //}

    //public int Level
    //{
    //    get => _level;
    //    set
    //    {
    //        _level = value;
    //        OnLevelChanged?.Invoke(value);
    //    }
    //}

    //public Vector2Int Position
    //{
    //    get => _position;
    //    set
    //    {
    //        _position = value;
    //        OnPositionChanged?.Invoke(value);
    //    }
    //}
    //#endregion

    public GameObject bulletPrefab;  // The bullet prefab to instantiate
    public Transform bulletSpawnPoint; // Point from where the bullet will be spawned
    public LayerMask enemyLayer;  // Layer containing enemies
    public float radius = 2f; // Radius to detect enemy
    public float shootingDelay = 1f; // Time delay between shots
    private float lastShotTime = 0f;  // Time of the last shot
    public float bulletSpeed = 10f; // Speed at which the bullet moves

    private void Update()
    {
        // Find all enemies within a 2 unit radius
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        // If there are any enemies in range
        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                // Shoot at the first enemy in range
                ShootAtEnemy(enemy.transform);
                break;
            }
        }
    }

    private void ShootAtEnemy(Transform enemy)
    {
        // Check the time between shots
        if (Time.time >= lastShotTime + shootingDelay)
        {
            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Calculate direction to the enemy
            Vector2 direction = (enemy.position - bulletSpawnPoint.position).normalized;

            // Set bullet's velocity towards the enemy
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }

            lastShotTime = Time.time; // Update the last shot time
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wire sphere for detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
