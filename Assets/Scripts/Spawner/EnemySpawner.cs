using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Player _targetPlayer;
    [SerializeField] private MouseHandler _mouseHandler;

    public event Action<Enemy> ObjectTookDamage;
    
    private void OnEnable()
    {
        _mouseHandler.SpawnEnemy += StartSpawning;
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
        enemy.ResetCharacteristics();
        
        enemy.CanBeReleased -= Release;
        enemy.BulletDetected -= TakeDamage;
        
        base.ActionOnRelease(enemy);
    }

    private void TakeDamage(Enemy enemy)
    {
        ObjectTookDamage?.Invoke(enemy);
    }
}
