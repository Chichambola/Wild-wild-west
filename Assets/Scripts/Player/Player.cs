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
    [SerializeField] private Health _health;

    private ThirdPersonController _controller;
    private MouseHandler _mouseHandler;

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
    }

    private void OnDisable()
    {
        _mouseHandler.ShootButtonClicked -= ProcessClick;
        _rotatorHandler.IdleStarted -= StartRotating;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
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
