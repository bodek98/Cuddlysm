using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : Weapon
{
    [SerializeField] private float _radius = 5;
    [SerializeField] private int _damage = 5;
    [SerializeField] private Entity.DamageDealerType _damageDealerType;
    
    [SerializeField] private LayerMask _targetMask;


    public override void Attack()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        foreach (Collider rangeCheck in rangeChecks)
        {
            rangeCheck.gameObject.TryGetComponent<Entity>(out Entity entityComponent);
            entityComponent.DamageEntity(_damage, _damageDealerType);
        }
    }

    public override void StopAttack()
    {

    }

    public override IEnumerator Reload()
    {
        yield break;
    }
}
