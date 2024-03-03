using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    [SerializeField] private PlayerStaminaBar _playerStaminaBar;
    private GameManager _gameManager;

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
        _gameManager.HandlePlayerDestroy(transform.parent.gameObject);
        Destroy(transform.parent.gameObject, _gameManager.playerDeathDuration);
        Destroy(gameObject);
    }

    // New functions
    
    private void UpdateHealthBar()
    {
        _playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void RegenerateStamina(float staminaPoints)
    {
        currentStamina = maxStamina;
        _playerStaminaBar.UpdateStaminaBar(currentStamina, staminaPoints);
    }
    
    public void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.HandlePlayerAddition(transform.parent.gameObject);
    }
}
