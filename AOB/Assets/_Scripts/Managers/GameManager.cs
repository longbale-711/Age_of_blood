using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern.Singleton;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private UIController _uiController;
    [SerializeField] private PlayerController _playerController;

    public GridManager GridManager => _gridManager;
    public TowerManager TowerManager => _towerManager;
    public EnemyManager EnemyManager => _enemyManager;
    public UIController UIController => _uiController;
    public PlayerController PlayerController => _playerController;

    public event Action OnStartGame;

    [ContextMenu("Start game")]
    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
    }
}
