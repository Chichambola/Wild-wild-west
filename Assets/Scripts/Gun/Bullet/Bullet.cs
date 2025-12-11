using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private int _lifeSpan = 3;
    [SerializeField] private int _ageDelay = 1;
    
    public event Action<Bullet> CollisionDetected;
    public event Action<Bullet> OldEnough;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Aging());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBulletDetectable _))
        {
            CollisionDetected?.Invoke(this);
        }
    }

    private IEnumerator Aging()
    {
        var wait = new WaitForSecondsRealtime(_ageDelay);
        
        int currentLife = 0;
        
        while (currentLife != _lifeSpan)
        {
            currentLife += _ageDelay;
            
            yield return wait;
        }
        
        OldEnough?.Invoke(this);
    }
}
