using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMover), typeof(AnimationController), typeof(Health))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour, IBulletDetectable, IAttacker, IPoolable
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerDetector _playerDetector;
    
    private Collider _collider;
    private int _damage = 1;
    
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
        _animationController.FinishedAttack += LookForPlayer;
        _animationController.HitPointReached += TryHitPlayer;
        _playerDetector.IsPlayerInCollider += PlayAttack;
    }

    private void OnDisable()
    {
        _health.NoHealthLeft -= Die;
        _animationController.FinishedDying -= Release;
        _animationController.FinishedAttack -= LookForPlayer;
        _animationController.HitPointReached -= TryHitPlayer;
        _playerDetector.IsPlayerInCollider -= PlayAttack;
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
    
    public void DealDamage(IAttacker defender)
    {
        defender.TakeDamage(_damage);
    }

    public void ResetCharacteristics()
    {
        _collider.enabled = true;
        
        _health.ResetHealth();
    }

    public void Die()
    {
        _collider.enabled = false;
        
        _mover.StopMoving();
        
        _animationController.PlayDead();
    }
    
    private void LookForPlayer()
    {
        if (_playerDetector.TryGetPlayer(out Player _))
            PlayAttack(true);
        else 
            PlayAttack(false);
    }

    private void TryHitPlayer()
    {
        if (_playerDetector.TryGetPlayer(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }
    
    private void PlayAttack(bool value)
    {
        _animationController.PlayAttack(value);
        
        if (value == false)
        {
            _mover.StartMoving();
        }
        else
        {
            _mover.StopMoving();
        }
    } 
    
    private void Release() => CanBeReleased?.Invoke(this);
    
    private void ProcessTrigger() => BulletDetected?.Invoke(this);
}
