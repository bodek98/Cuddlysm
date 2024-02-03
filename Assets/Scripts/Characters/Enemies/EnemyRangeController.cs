using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : EnemyBaseController
{
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