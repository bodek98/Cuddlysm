using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private Entity.DamageDealerType _damageDealerType;
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Entity>(out Entity entityComponent))
        {
            entityComponent.DamageEntity(_damage, _damageDealerType);
        }
    }
}
