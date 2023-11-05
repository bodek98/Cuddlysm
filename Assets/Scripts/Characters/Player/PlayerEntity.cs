using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public PlayerEntity() : base(100, 100)
    {
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {

    }
    
    protected override bool checkIfVulnerable(DamageDealerType damageDealerType)
    {
        switch (damageDealerType)
        {
            case DamageDealerType.Bullet:
                return true;
            case DamageDealerType.Acid:
                return true;
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
}
