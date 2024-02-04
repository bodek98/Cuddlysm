using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : EnemyBaseController
{
    protected override void AttackTarget()
    {
        if (agent.remainingDistance < 1)
        {
            weapon.Attack();

            gameObject.TryGetComponent<Entity>(out Entity entityComponent);
            entityComponent.DamageEntity(entityComponent.maxHealth, Entity.DamageDealerType.Explosive);
        }
    }
}
