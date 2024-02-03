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

    protected override void AttackTarget()
    {
        Vector3 directionToTarget = (fov.currentTarget.transform.position - transform.position).normalized;
        float enemyTargetAngle = Vector3.Angle(directionToTarget, transform.forward);

        if (enemyTargetAngle < 5.0f)
        {
            projectileWeapon.Attack();
        }
        // Todo: Stop attack when weapon is full-auto.
    }
}