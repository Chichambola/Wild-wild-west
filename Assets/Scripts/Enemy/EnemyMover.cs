using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 15f;
    
    private Player _player;
    private Coroutine _coroutine;

    public void SetPlayer(Player player)
    {
        _player = player;
    }
    
    public void StartMoving()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Moving());
    }

    public void StopMoving()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }
    
    private IEnumerator Moving()
    {
        while (enabled)
        {
            Vector3 playerPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);

            Vector3 playerDirection= UserUtils.GetDirection(playerPosition, transform.position);
            
            transform.position = Vector3.MoveTowards(transform.position, playerPosition ,_speed * Time.deltaTime);
            
            transform.forward = Vector3.Lerp(transform.forward, playerDirection, _rotationSpeed * Time.deltaTime);
            
            yield return null;
        }
    }
}
