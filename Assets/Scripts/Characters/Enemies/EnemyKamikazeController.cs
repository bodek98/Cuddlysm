using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeController : EnemyBaseController
{

    protected override void AttackTarget()
    {
        if (agent.remainingDistance > safeDistanceToPlayer) return;
        
        weapon.Attack();

        gameObject.TryGetComponent<Entity>(out Entity entityComponent);
        entityComponent.DamageEntity(entityComponent.maxHealth, Entity.DamageDealerType.Explosive);
    }
}
