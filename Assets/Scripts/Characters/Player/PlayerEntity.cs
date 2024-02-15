using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    
    protected override bool CheckIfVulnerable(DamageDealerType damageDealerType)
    {
        return damageDealerType switch
        {
            DamageDealerType.Acid => DamageCooldown(DamageDealerType.Acid),
            _ => true
        };
    }

    protected override void HandleAfterDamage()
    {
        UpdateHealthbar();
    }
    
    protected override void DeathAnimation()
    {
        Debug.Log("Player is dead :(");
    }

    // New functions
    
    private void UpdateHealthbar()
    {
        _playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
