using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    private Dictionary<DamageDealerType, float> _damageCooldownDict;
    private float _damageCooldownDuration = 1.0f; // in seconds
    
    public PlayerEntity() : base(100, 100)
    {
    }
    
    private void Start()
    {
        _damageCooldownDict = new Dictionary<DamageDealerType, float>();
    }
    
    private void Update()
    {

    }
    
    protected override bool checkIfVulnerable(DamageDealerType damageDealerType)
    {
        switch (damageDealerType)
        {
            case DamageDealerType.Bullet:
                return true;
            case DamageDealerType.Acid:
                return acidDamageCooldown();
            case DamageDealerType.Knife:
                return true;
            default:
                return true;
        }
    }

    protected override void DeathAnimation()
    {
        Debug.Log("Player is dead :(");
    }

    private bool acidDamageCooldown()
    {
        // If cooldown data exist, check if it passed
        if (_damageCooldownDict.TryGetValue(DamageDealerType.Acid, out float cooldownEnd))
        {
            if (cooldownEnd > Time.time) return false;
        }
        // If it doesn't exist, or it passed
        _damageCooldownDict[DamageDealerType.Acid] = Time.time + _damageCooldownDuration;
        return true;
    }
}
