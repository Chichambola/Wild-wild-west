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
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayDead()
    {
        _animator.SetBool(IsDead, true);
    }
    
    public void PlayMove(int speed)
    {
        _animator.SetFloat(Speed, speed);
    }
}
