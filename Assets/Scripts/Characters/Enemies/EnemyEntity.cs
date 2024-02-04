using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEntity : Entity
{
    [SerializeField] private Image _healthBar;

    protected override bool CheckIfVulnerable(DamageDealerType damageDealerType)
    {
        return damageDealerType switch
        {
            DamageDealerType.Bullet => true,
            DamageDealerType.Acid => DamageCooldown(DamageDealerType.Acid),
            DamageDealerType.Knife => true,
            _ => true
        };
    }

    protected override void HandleAfterDamage()
    {
        UpdateHealthBar();
    }

    protected override void DeathAnimation()
    {
        if (deathPrefab)
        {
            Vector3 groundLevel = transform.position;
            groundLevel.y -= 1;
            Instantiate(deathPrefab, groundLevel, transform.rotation);
        }

        Destroy(gameObject);
    }
    
    // New functions
    
    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = currentHealth / maxHealth ;
    }
}
