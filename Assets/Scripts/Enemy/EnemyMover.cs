using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    
    private int _idleSpeed = 0;
    private int _moveSpeed = 1;
    
    private Player _player;
    private Coroutine _coroutine;

    public int idleSpeed => _idleSpeed;
    public int MoveSpeed => _moveSpeed;

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
    
    private IEnumerator Moving()
    {
        while (enabled)
        {
            Vector3 playerPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);

            Vector3 playerDirection= UserUtils.GetDirection(playerPosition, transform.position);
            
            transform.position = Vector3.MoveTowards(transform.position, playerPosition ,_speed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, playerDirection, _speed * Time.deltaTime);
            
            yield return null;
        }
    }
}
