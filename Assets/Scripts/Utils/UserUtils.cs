using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserUtils
{
    public static Vector3 GetDirection(Vector3 startPosition, Vector3 endPosition)
    { 
        Vector3 aimDirection = (startPosition - endPosition).normalized;

        return aimDirection;
    }
}
