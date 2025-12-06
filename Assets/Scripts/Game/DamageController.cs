using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _enemy.TookDamage += ProcessDamage;
    }

    private void OnDisable()
    {
        _enemy.TookDamage -= ProcessDamage;
    }

    private void ProcessDamage()
    {
        _enemy.TakeDamage(_player.GunDamage);
        
        Debug.Log("Enemy took damage");
    }
}
