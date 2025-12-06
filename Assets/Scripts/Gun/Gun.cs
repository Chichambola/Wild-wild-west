using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private int _damage = 1;

    public int Damage => _damage;
    
    public void Shoot(Vector3 shootDirection)
    {
        Vector3 aimDirection = UserUtils.GetDirection(shootDirection, _shootPosition.position);
        
        _bulletSpawner.SetShootPosition(_shootPosition.transform.position);
        _bulletSpawner.SetDirection(aimDirection);

        _bulletSpawner.StartSpawning();
    }
}
