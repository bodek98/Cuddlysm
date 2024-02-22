using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Dictionary<DamageDealerType, float> _damageCooldownDict;

    [SerializeField] public float maxHealth = 100;
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] private float damageCooldownDuration = .01f;
    [SerializeField] private float burnDuration = 4f;
    [SerializeField] private float burnDamage = .1f;
    [SerializeField] protected GameObject fireArson;
    [SerializeField] protected GameObject deathPrefab;
    [SerializeField] protected bool attacksOnDeath;
    protected bool isOnFire = false;

    private IEnumerator _burnCoroutine;
    private ParticleSystem _burningParticles;
    
    public enum DamageDealerType
    {
        Acid, Bullet, Explosive, Flame
    }

    public float Health => currentHealth;

    public float MaxHealth => maxHealth;
    
    private void Start()
    {
        _damageCooldownDict = new Dictionary<DamageDealerType, float>();
        _burningParticles = fireArson.GetComponent<ParticleSystem>();
        _burningParticles.Stop();
    }

    public void DamageEntity(float damage, DamageDealerType damageDealerType)
    {
        if (currentHealth <= 0 || !CheckIfVulnerable(damageDealerType)) return;
        if (damageDealerType == DamageDealerType.Flame)
        {
            SetOnFire();
        }

        DamageHandling(damage);
    }

    private void DamageHandling(float damage)
    {
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
        _damageCooldownDict[damageDealerType] = Time.time + damageCooldownDuration;
        return true;
    }

    private void SetOnFire()
    {
        if (_burnCoroutine != null)
        {
            StopCoroutine(_burnCoroutine);
            _burningParticles.Stop();
        }

        _burnCoroutine = Burn();
        StartCoroutine(_burnCoroutine);
    }

    private IEnumerator Burn()
    {
        _burningParticles.Play();
        
        float elapsed = 0f;
        while (elapsed < burnDuration) 
        {
            // ToDo: Handle game pause
            elapsed += Time.deltaTime + damageCooldownDuration;
            DamageHandling(burnDamage);
            yield return new WaitForSeconds(damageCooldownDuration);
        }
        
        _burningParticles.Stop();
    }

    /* Abstract functions */
    
    protected abstract void HandleAfterDamage();
    
    protected abstract void DeathAnimation();

    protected abstract bool CheckIfVulnerable(DamageDealerType damageDealerType);
}
