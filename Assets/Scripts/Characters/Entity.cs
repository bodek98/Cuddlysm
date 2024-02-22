using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Dictionary<DamageDealerType, float> _damageCooldownDict;
    private const float _damageCooldownDuration = .01f; // in seconds

    [SerializeField] public float maxHealth = 100;
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected GameObject deathPrefab;
    [SerializeField] protected bool attacksOnDeath;
    
    public enum DamageDealerType
    {
        Acid, Bullet, Knife, Explosive, Flame
    }

    public float Health => currentHealth;

    public float MaxHealth => maxHealth;
    
    private void Start()
    {
        _damageCooldownDict = new Dictionary<DamageDealerType, float>();
    }

    public void DamageEntity(float damage, DamageDealerType damageDealerType)
    {
        if (currentHealth <= 0 || !CheckIfVulnerable(damageDealerType)) return;
        currentHealth -= damage;
            
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HandleDeath();
        }

        HandleAfterDamage();
    }

    public void HealEntity(int heal)
    {
        if (currentHealth >= maxHealth) return;
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void HandleDeath()
    {
        if (attacksOnDeath)
        {
            if (gameObject.TryGetComponent<EnemyBaseController>(out EnemyBaseController controller))
            {
                controller.weapon?.Attack();
            }
        }
        DeathAnimation();
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
