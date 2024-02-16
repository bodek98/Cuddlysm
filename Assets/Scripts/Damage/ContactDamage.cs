using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private Entity.DamageDealerType _damageDealerType;
    [SerializeField] private List<Entity> entitiesToDamage;
    
    private void Update()
    {
        foreach (var entity in entitiesToDamage)
        {
            entity.DamageEntity(_damage, _damageDealerType);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Entity>(out Entity entityComponent))
        {
            entitiesToDamage.Add(entityComponent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Entity>(out Entity entityComponent))
        {
            entitiesToDamage.Remove(entityComponent);
        }
    }
}
