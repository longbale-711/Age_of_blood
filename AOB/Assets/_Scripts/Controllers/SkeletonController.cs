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
        _isDie = false;
    }

    public bool IsDie() => _isDie;

    public void Attack()
    {
        // Implement attack logic
        Debug.Log("Attack with "+_skeletonModel.Damage+" damage !");
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
        Debug.Log("Die");
        OnDying?.Invoke();

    }
}
