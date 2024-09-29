using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Default value
    private const int DEFAULT_MAX_HEALTH = 5;
    private const int DEFAULT_MAX_DAMAGE = 1;
    private const float DEFAULT_MAX_SPEED = 1;
    // Attributes
    [SerializeField] private int _health = DEFAULT_MAX_HEALTH;
    [SerializeField] private int _damage = DEFAULT_MAX_DAMAGE;
    [SerializeField] private float _speed = DEFAULT_MAX_SPEED;
    // Events
    public event Action<int> OnHealthChange;
    public event Action<int> OnDamageChange;
    public event Action<float> OnSpeedChange;


    #region Getter/Setter
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChange?.Invoke(value);
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            _speed = value;
            OnSpeedChange?.Invoke(value);
        }
    }

    public int Damage
    {
        get => _damage;
        set
        {
            _damage = value;
            OnDamageChange?.Invoke(value);
        }
    } 
    #endregion


}
