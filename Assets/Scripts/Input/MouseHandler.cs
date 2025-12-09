using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]
public class MouseHandler : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private Transform _debugTransform;
    [SerializeField] private LayerMask _layerMask;
    
    private Vector2 _screenCenterPoint;
    private Vector3 _mouseWorldPosition;
    private float _maxDistance = 1000f;
    private float _distance = 10f;
    private float _screenOffset = 2f;

    public event Action<Vector3> ShootButtonClicked;
    public event Action SpawnEnemy;

    private void Awake()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    private void OnEnable()
    {
        _screenCenterPoint = new Vector2(Screen.width / _screenOffset, Screen.height / _screenOffset);
        _mouseWorldPosition = Vector2.zero;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, _maxDistance, _layerMask))
        {
            _debugTransform.position = raycastHit.point;
            _mouseWorldPosition = raycastHit.point;
        }
        else
        {
            _mouseWorldPosition = ray.GetPoint(_distance);
            _debugTransform.position = _mouseWorldPosition;
        }

        if(_inputs.shoot)
        {
            ShootButtonClicked?.Invoke(_mouseWorldPosition);
            
            _inputs.shoot = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemy?.Invoke();
        }
    }
}
