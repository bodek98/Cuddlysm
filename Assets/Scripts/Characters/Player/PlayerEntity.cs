using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private PlayerHealthBar _playerHealthBar;
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
        UpdateHealthbar();
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
    
    private void UpdateHealthbar()
    {
        _playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    
    public void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.HandlePlayerAddition(transform.parent.gameObject);
    }
}
