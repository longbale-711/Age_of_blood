using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController
{
    private readonly SkeletonPresent _skeletonPresent;
    private readonly Skeleton _skeletonModel;
    private bool _isDie = false;

    public event Action OnDying;

    public SkeletonController(SkeletonPresent skeletonPresent, Skeleton skeletonModel)
    {
        _skeletonPresent = skeletonPresent;
        _skeletonModel = skeletonModel;
       
    }

    public void Init()
    {
        _isDie = false;
        _skeletonModel.ResetValue();
    }

    public bool IsDie() => _isDie;

    public void Attack()
    {
        // Implement attack logic
        GameManager.Instance.PlayerController.TakeDamaged(_skeletonModel.Damage);
    }

    public void TakeDamaged(int dmgTaken)
    {
        // Implement take damage logic
        _skeletonModel.Health -= dmgTaken;
        if (_skeletonModel.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement dying logic
        _isDie = true;
        _skeletonModel.ResetValue();
        OnDying?.Invoke();

    }
}
