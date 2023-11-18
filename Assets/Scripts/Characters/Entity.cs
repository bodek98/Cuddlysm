using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Dictionary<DamageDealerType, float> _damageCooldownDict;
    private const float _damageCooldownDuration = 1.0f; // in seconds

    protected float _maxHealth;
    protected float _currentHealth;
    
    public enum DamageDealerType
    {
        Acid, Bullet, Knife
    }

    public float Health => _currentHealth;

    public float MaxHealth => _maxHealth;

    protected Entity(int currentHealth, int maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }
    
    private void Start()
    {
        _damageCooldownDict = new Dictionary<DamageDealerType, float>();
    }

    public void DamageEntity(int damage, DamageDealerType damageDealerType)
    {
        if (_currentHealth <= 0 || !CheckIfVulnerable(damageDealerType)) return;
        _currentHealth -= damage;
            
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            DeathAnimation();
        }

        HandleAfterDamage();
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
    
    protected bool DamageCooldown(DamageDealerType damageDealerType)
    {
        // If cooldown data exist, check if it passed
        if (_damageCooldownDict.TryGetValue(damageDealerType, out float cooldownEnd))
        {
            if (cooldownEnd > Time.time) return false;
        }
        // If it doesn't exist, or it passed
        _damageCooldownDict[damageDealerType] = Time.time + _damageCooldownDuration;
        return true;
    }

    /* Abstract functions */
    
    protected abstract void HandleAfterDamage();
    
    protected abstract void DeathAnimation();

    protected abstract bool CheckIfVulnerable(DamageDealerType damageDealerType);
}
