using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;  // The bullet prefab to instantiate
    public Transform bulletSpawnPoint; // Point from where the bullet will be spawned
    public LayerMask enemyLayer;  // Layer containing enemies
    public int price;
    public int damage;
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
            var bulletController = bullet.GetComponent<BulletController>();
            bulletController.Damage = damage;
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
