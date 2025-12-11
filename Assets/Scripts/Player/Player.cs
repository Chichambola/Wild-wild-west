using System;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(ThirdPersonController), typeof(MouseHandler))]
public class Player : MonoBehaviour, IAttacker
{
    [SerializeField] private Gun _gun;
    [SerializeField] private CharacterRotationHandler _rotatorHandler;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Health _health;

    private ThirdPersonController _controller;
    private MouseHandler _mouseHandler;

    private ParticleSystem _particle;
    
    public int GunDamage => _gun.Damage;
    
    private void Awake()
    {
        _controller = GetComponent<ThirdPersonController>();
        _mouseHandler = GetComponent<MouseHandler>();
    }

    private void OnEnable()
    {
        _mouseHandler.ShootButtonClicked += ProcessClick;
        _rotatorHandler.IdleStarted += StartRotating;
        _health.NoHealthLeft += Die;
    }

    private void OnDisable()
    {
        _mouseHandler.ShootButtonClicked -= ProcessClick;
        _rotatorHandler.IdleStarted -= StartRotating;
        _health.NoHealthLeft -= Die;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void DealDamage(IAttacker defender)
    {
        defender.TakeDamage(_gun.Damage);
    }
    
    public void Die()
    {
        _controller.SetIsRotating(false);
        
        _animationController.PlayDead();

        _controller.enabled = false;

        _mouseHandler.enabled = false;
    }
    
    private void ProcessClick(Vector3 aimDirection)
    {
        _controller.SetIsRotating(false);
        
        _rotatorHandler.StartRotating(aimDirection);
        
        _gun.Shoot(aimDirection);
    }

    private void StartRotating()
    {
        _controller.SetIsRotating(true);
    }
}
