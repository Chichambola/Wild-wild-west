using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Player _targetPlayer;

    public event Action<Enemy> ObjectTookDamage;
    
    private void OnEnable()
    {
        GetObject();
    }

    protected override void ActionOnGet(Enemy enemy)
    {
        enemy.SetPlayer(_targetPlayer);

        enemy.CanBeReleased += Release;
        enemy.BulletDetected += TakeDamage;
        
        base.ActionOnGet(enemy);
        
        enemy.StartMoving();
    }

    protected override void ActionOnRelease(Enemy enemy)
    {
        enemy.CanBeReleased -= Release;
        
        base.ActionOnRelease(enemy);
    }

    private void TakeDamage(Enemy enemy)
    {
        ObjectTookDamage?.Invoke(enemy);
    }
}
