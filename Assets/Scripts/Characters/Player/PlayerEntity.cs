using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public PlayerEntity() : base(100, 100)
    {
    }

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
