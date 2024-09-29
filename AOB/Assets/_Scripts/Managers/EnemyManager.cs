using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public EnemyPool EnemyPool;
    public SpawnEnemy EnemySpawner;
    public List<Transform> EnemyPathNode;

    private void Start()
    {
        Instance = this;
    }
}
