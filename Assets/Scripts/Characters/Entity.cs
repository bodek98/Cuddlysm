using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;
    
    public enum DamageDealerType
    {
        Acid, Bullet, Knife
    }

    public int Health
    {
        get { return _currentHealth; }
        set { _currentHealth = value;  }
    }
    
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value;  }
    }

    public Entity(int currentHealth, int maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }

    public void DamageEntity(int damage, DamageDealerType damageDealerType)
    {
        if (_currentHealth <= 0 || !checkIfVulnerable(damageDealerType)) return;
        _currentHealth -= damage;
            
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            DeathAnimation();
        }
    }

    public void HealEntity(int heal)
    {
        if (_currentHealth >= _maxHealth) return;
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    /* Abstract functions */

    protected abstract void DeathAnimation();

    protected abstract bool checkIfVulnerable(DamageDealerType damageDealerType);
}
