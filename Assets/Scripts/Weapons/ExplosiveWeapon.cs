using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : Weapon
{
    [SerializeField] private float _radius = 2;
    [SerializeField] private int _damage = 20;
    [SerializeField] private Entity.DamageDealerType _damageDealerType;
    [SerializeField] private LayerMask _targetMask;

    private bool _exploded;

    public override void Attack()
    {
        if (_exploded) return;
        _exploded = true;
        
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        foreach (Collider rangeCheck in rangeChecks)
        {
            rangeCheck.gameObject.TryGetComponent<Entity>(out Entity entityComponent);
            entityComponent.DamageEntity(_damage, _damageDealerType);
        }
    }

    public override void StopAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void StartReloading(bool forceReload = false)
    {
        throw new System.NotImplementedException();
    }
}
