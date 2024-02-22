using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretEntity : EnemyEntity
{

    protected override bool CheckIfVulnerable(DamageDealerType damageDealerType)
    {
        return damageDealerType switch
        {
            DamageDealerType.Bullet => true,
            DamageDealerType.Explosive => true,
            _ => false
        };
    }
}
