using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private int _maxEnemy = 6;
    [SerializeField] private int enemiesPerWave = 2; // Number of enemies per wave
    [SerializeField] private float timeBetweenWaves = 5f; // Time between waves
    [SerializeField] private float timeBetweenSpawns = 1f; // Time between each enemy in a wave
    private int totalSpawnedEnemies = 0; // Total enemies spawned

    private void Start()
    {
        StartSpawningEnemies().Forget();
    }

    private async UniTaskVoid StartSpawningEnemies()
    {
        while (totalSpawnedEnemies <= _maxEnemy)
        {
            await SpawnWave();
            await UniTask.Delay((int)(timeBetweenWaves * 1000)); // Delay for timeBetweenWaves in milliseconds
        }
    }

    private async UniTask SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (totalSpawnedEnemies >= 10)
                break;

            SpawnAtPosition(_spawnPoints.position);
            totalSpawnedEnemies++;

            await UniTask.Delay((int)(timeBetweenSpawns * 1000)); // Delay between each enemy spawn in milliseconds
        }
    }

    private void SpawnAtPosition(Vector3 position)
    {
        GameObject enemy = _enemyPool.GetObjectFromPool(); // Get enemy from pool
        enemy.transform.position = position; // Set enemy position
        enemy.SetActive(true);
    }

    public void DespawnEnemy(GameObject enemy)
    {
        _enemyPool.ReturnObjectToPool(enemy); // return enemy to pool
    }
}
