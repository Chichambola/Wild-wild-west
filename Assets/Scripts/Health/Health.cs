using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _currentHealth;

    public event Action<float, float> ValueChanged;
    public event Action NoHealthLeft;
    private bool _isAlive => _currentHealth != _minHealth;
    
    public float Value => _currentHealth;
    public float MaxValue => _maxHealth;
    public float MinValue => _minHealth;

    private void OnValidate()
    {
        IsHealthMoreMaxHealth();
    }

    public void Heal(float healAmount)
    {
        if(healAmount > 0)
        {
            _currentHealth += healAmount;

            IsHealthMoreMaxHealth();

            ValueChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_isAlive && damage > _minHealth)
        {
            _currentHealth -= damage;

            IsHealthMoreMaxHealth();
        }
        
        IsDead();
        
        ValueChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void IsDead()
    {
        if (_isAlive == false)
        {
            NoHealthLeft?.Invoke();
        }
    }
    
    private void IsHealthMoreMaxHealth()
    {
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
