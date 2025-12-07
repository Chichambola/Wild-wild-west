using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _enemySpawner.ObjectTookDamage += ProcessDamage;
    }

    private void OnDisable()
    {
        _enemySpawner.ObjectTookDamage -= ProcessDamage;
    }

    private void ProcessDamage(Enemy enemy)
    {
        enemy.TakeDamage(_player.GunDamage);
        
        Debug.Log("Enemy took damage");
    }
}
