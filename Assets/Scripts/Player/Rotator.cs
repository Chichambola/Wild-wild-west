using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    
    private Coroutine _coroutine;

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    public void StartRotating(Vector3 direction)
    {
        if (_coroutine != null) 
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Rotating(direction));
    }

    private IEnumerator Rotating(Vector3 direction)
    {
        direction.y = transform.position.y;

        Vector3 aimDirection = UserUtils.GetDirection(direction, transform.position);
        
        while (transform.forward != aimDirection)
        {
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, _speed * Time.deltaTime);
            
            yield return null;
        }
    }
}
