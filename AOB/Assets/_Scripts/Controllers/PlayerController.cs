using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int DEFAULT_START_HEALTH = 5;
    private const int DEFAULT_START_MONEY = 300;
    [SerializeField] int _startHealth = DEFAULT_START_HEALTH;
    [SerializeField] int _startMoney = DEFAULT_START_MONEY;

    private GameManager _manager;
    private UIController _uiController;
    private Player _currentPlayer;

    public event Action<Tower> OnPurchaseTowerCompleted;
    public Player GetCurrentPlayer() => _currentPlayer;

    private void Start()
    {
        _manager = GameManager.Instance;
        _uiController = _manager.UIController;
        _currentPlayer = GetComponent<Player>();
        _manager.OnStartGame += ConfigStartValue;    
    }

    private void OnDestroy()
    {
        _manager.OnStartGame -= ConfigStartValue;
    }


    private void ConfigStartValue()
    {
        _currentPlayer.Health = _startHealth;
        _currentPlayer.Money = _startMoney;
    }

    public void TakeDamaged(int dmgTaken)
    {
        _currentPlayer.Health -= dmgTaken;

        if (_currentPlayer.Health <= 0)
        {
            // GameOver
            Die();
        }
    }

    private void Die()
    {
        _manager.Pause();
        _uiController.OpenGameOverPanel();
    }

    public void Purchase(Tower tower)
    {
        if (!CanPurchase(tower.price)) return;

        _currentPlayer.Money -= tower.price;
        OnPurchaseTowerCompleted?.Invoke(tower);
    }

    public bool CanPurchase(int price)
    {
        if (_currentPlayer.Money < price)
        {
            Debug.Log("You cant effort it!");
            return false;
        }
        return true;
    }

    public void EarnMoney(int moneyEarn)
    {
        _currentPlayer.Money += moneyEarn;
    }

}
