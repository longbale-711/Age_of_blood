using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase
{
    public int Health { get; protected set; }
    public int Speed { get; protected set; }

    protected EnemyBase(int health, int speed)
    {
        Health = health;
        Speed = speed;
    }

    public abstract void Attack();
    public abstract void TakeDamage(int dmg);
}
