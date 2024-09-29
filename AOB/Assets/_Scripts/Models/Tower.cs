using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private int _level;
    [SerializeField] private Vector2Int _position;
    // Events
    public event Action<float> OnFireRateChanged;
    public event Action<float> OnRadiusChanged;
    public event Action<int> OnDamageChanged;
    public event Action<int> OnLevelChanged;
    public event Action<Vector2Int> OnPositionChanged;
    
    #region Getter / Setter
    public float FireRate
    {
        get => _fireRate;
        set
        {
            _fireRate = value;
            OnFireRateChanged?.Invoke(value);
        }
    }

    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            OnRadiusChanged?.Invoke(value);
        }
    }

    public int Damage
    {
        get => _damage;
        set
        {
            _damage = value;
            OnDamageChanged?.Invoke(value);
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(value);
        }
    }

    public Vector2Int Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPositionChanged?.Invoke(value);
        }
    }
    #endregion
}
