using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int Damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<SkeletonPresent>();
            if (enemy == null) return;

            Attack(enemy);
            Destroy();
        }

    }

    private void Attack(SkeletonPresent enemy)
    {
        enemy.TakeDamaged(Damage);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
