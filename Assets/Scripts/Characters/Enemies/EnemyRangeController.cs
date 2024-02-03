using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : EnemyBaseController
{
    [SerializeField] private float _safeDistanceToPlayer = 10;
    protected override Vector3 CalculateDestination()
    {
        float currentDistance = Vector3.Distance(transform.position, fov.currentTarget.transform.position);
        float interpolationRatio = Mathf.Clamp(1 - _safeDistanceToPlayer / currentDistance, 0, 1);

        return Vector3.Lerp(transform.position, fov.currentTarget.transform.position, interpolationRatio);
    }
}