using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterRotationHandler : MonoBehaviour
{
    [SerializeField] private float _idleDelay = 0.1f;
    [SerializeField] private float _maxWaitTime = 0.6f;
    [SerializeField] private Rotator _rotator;

    public event Action IdleStarted;
    
    private Coroutine _coroutine;

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    public void StartRotating(Vector3 direction)
    {
        _rotator.StartRotating(direction);
        
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Idling());
    }

    private IEnumerator Idling()
    {
        var wait = new WaitForSecondsRealtime(_idleDelay);

        float currentTime = 0;
        
        while (currentTime != _maxWaitTime)
        {
            currentTime += _idleDelay;
            
            yield return wait;
        }
        
        IdleStarted?.Invoke();
    }
}
