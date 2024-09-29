using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _money;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnMoneyChanged;


    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            OnMoneyChanged?.Invoke(value);
        }
    }
}
