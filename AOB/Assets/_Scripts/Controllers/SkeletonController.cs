using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController
{
    private readonly SkeletonPresent _skeletonPresent;
    private readonly Skeleton _skeletonModel;

    public SkeletonController(SkeletonPresent skeletonPresent, Skeleton skeletonModel)
    {
        _skeletonPresent = skeletonPresent;
        _skeletonModel = skeletonModel;
    }

    public void Attack()
    {
        // Implement attack logic
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
        Debug.Log("Die");
    }
}
