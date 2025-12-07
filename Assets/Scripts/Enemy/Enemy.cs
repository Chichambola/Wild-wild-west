using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IBulletDetectable, IAttacker, IPoolable
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Health _health;
    
    public event Action<Enemy> BulletDetected;
    public event Action<Enemy> CanBeReleased;

    private void OnEnable()
    {
        _health.NoHealthLeft += Die;
        _animationController.FinishedDying += Release;
    }

    private void OnDisable()
    {
        _health.NoHealthLeft -= Die;
        _animationController.FinishedDying -= Release;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet _))
        {
            ProcessTrigger();    
        }
    }

    public void StartMoving()
    {
        _animationController.PlayMove(_mover.MoveSpeed);
        
        _mover.StartMoving();
    }
    
    public void SetPlayer(Player player)
    {
        _mover.SetPlayer(player);
    }
    
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void Die()
    {
        _animationController.PlayDead();
    }

    private void Release()
    {
        CanBeReleased?.Invoke(this);
    }
    
    private void ProcessTrigger()
    {
        BulletDetected?.Invoke(this);
    }
}
