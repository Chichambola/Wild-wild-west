using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    private Vector3 _shootDirection;
    private Vector3 _shootPosition;

    public void SetDirection(Vector3 direction)
    {
        _shootDirection = direction;
    }

    public void SetShootPosition(Vector3 position)
    {
        _shootPosition = position;
    }
    
    protected override void ActionOnGet(Bullet bullet)
    {
        bullet.transform.position = _shootPosition;
        
        bullet.transform.rotation = Quaternion.LookRotation(_shootDirection);

        bullet.CollisionDetected += Release;
        bullet.OldEnough += Release;
        
        base.ActionOnGet(bullet);
    }

    protected override void ActionOnRelease(Bullet bullet)
    {
        bullet.CollisionDetected -= Release;
        bullet.OldEnough -= Release;
        
        base.ActionOnRelease(bullet);
    }
}
