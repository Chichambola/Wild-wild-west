using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBulletDetectable, IAttacker
{
    [SerializeField] private EnemyMover _mover;

    private Player _player;
    public event Action TookDamage;

    public void SetPlayer(Player player)
    {
        _mover.SetPlayer(player);
        _player = player;
    }

    public void StartMoving()
    {
        _mover.StartMoving();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet _))
        {
            ProcessTrigger();    
        }
    }

    public void TakeDamage(float damage)
    {
        
    }
    
    private void ProcessTrigger()
    {
        TookDamage?.Invoke();
    }
}
