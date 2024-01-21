using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    protected override bool CheckIfVulnerable(DamageDealerType damageDealerType)
    {
        return damageDealerType switch
        {
            DamageDealerType.Bullet => true,
            DamageDealerType.Acid => DamageCooldown(DamageDealerType.Acid),
            DamageDealerType.Knife => true,
            _ => true
        };
    }

    protected override void HandleAfterDamage()
    {
    }
    
    protected override void DeathAnimation()
    {
        Debug.Log("Player is dead :(");
    }

    // New functions
}
