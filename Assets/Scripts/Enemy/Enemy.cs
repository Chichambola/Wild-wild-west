using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMover), typeof(AnimationController), typeof(Health))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour, IBulletDetectable, IAttacker, IPoolable
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Health _health;
    
    private Collider _collider;
    
    public event Action<Enemy> BulletDetected;
    public event Action<Enemy> CanBeReleased;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

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
        _animationController.PlayMove(_animationController.MoveSpeed);
        
        _mover.StartMoving();
    }
    
    public void SetPlayer(Player player) => _mover.SetPlayer(player);
    
    public void TakeDamage(int damage) => _health.TakeDamage(damage);
    
    
    private void Die()
    {
       _collider.enabled = false;
        
        _mover.StopMoving();
        
        _animationController.PlayDead();
    }

    public void ResetCharacteristics()
    {
       _collider.enabled = true;
        
        _health.ResetHealth();
    }
    
    private void Release() => CanBeReleased?.Invoke(this);
    
    private void ProcessTrigger() => BulletDetected?.Invoke(this);
}
