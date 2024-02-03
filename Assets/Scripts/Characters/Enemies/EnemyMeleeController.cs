using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : EnemyBaseController
{
    protected override Vector3 CalculateDestination()
    {
        return fov.currentTarget.transform.position;
    }
}
