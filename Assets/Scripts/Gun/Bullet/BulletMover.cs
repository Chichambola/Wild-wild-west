using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Coroutine _coroutine;

    private void OnEnable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Moving());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }
    
    private IEnumerator Moving()
    {
        while (enabled)
        {
            transform.localPosition += transform.forward * _speed * Time.deltaTime;
            
            yield return null;
        }
    }
}
