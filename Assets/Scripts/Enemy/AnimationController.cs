using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{ 
    public const string IsAttacking = nameof(IsAttacking);
    public const string IsDead = nameof(IsDead);
    public const string Speed = nameof(Speed);

    public event Action FinishedDying;
    public event Action FinishedAttack;
    public event Action HitPointReached;
    
    private int _idleSpeed = 0;
    private int _moveSpeed = 1;
    private Animator _animator;
    
    public int IdleSpeed => _idleSpeed;
    public int MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttack(bool value)
    {
        _animator.SetBool(IsAttacking, value);
    }
    
    public void PlayDead()
    {
        _animator.SetBool(IsDead, true);
    }
    
    public void PlayMove(int speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    private void FinishDying()
    {
        FinishedDying?.Invoke();
    }

    private void FinishAttack()
    {
        FinishedAttack?.Invoke();
    }

    private void HitPlayer()
    {
        HitPointReached?.Invoke();
    }
}
