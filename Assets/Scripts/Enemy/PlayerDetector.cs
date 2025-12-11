using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    
    public event Action<bool> IsPlayerInCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            IsPlayerInCollider?.Invoke(true);
        }
    }

    public bool TryGetPlayer(out Player player)
    {
        float scaleOffset = 0.5f;
        
        Vector3 detectAreaCenter = _collider.transform.TransformPoint(_collider.center);
        Vector3 detectAreaHalfExtents = Vector3.Scale(_collider.size, _collider.transform.lossyScale) * scaleOffset;
        Collider[] hitColliders = Physics.OverlapBox(detectAreaCenter, detectAreaHalfExtents, _collider.transform.rotation);

        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent(out Player tempPlayer))
            {
                player = tempPlayer;
                
                return true;
            }
        }
        
        player = null;
            
        return false;   
    }
}
