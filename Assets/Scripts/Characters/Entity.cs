using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Dictionary<DamageDealerType, float> _damageCooldownDict;
    private const float _damageCooldownDuration = 1.0f; // in seconds

    [SerializeField] public float maxHealth = 100;
    [SerializeField] protected float _currentHealth = 100;
    
    public enum DamageDealerType
    {
        Acid, Bullet, Knife, Explosive
    }

    public float Health => _currentHealth;

    public float MaxHealth => maxHealth;
    
    private void Start()
    {
        _damageCooldownDict = new Dictionary<DamageDealerType, float>();
    }

    public void DamageEntity(float damage, DamageDealerType damageDealerType)
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
        if (_currentHealth >= maxHealth) return;
        _currentHealth += heal;

        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
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
