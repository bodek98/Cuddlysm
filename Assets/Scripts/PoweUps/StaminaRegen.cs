using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRegen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerEntity>(out PlayerEntity entityComponent))
        {
            entityComponent.RegenerateStamina(entityComponent.maxStamina);
            Destroy(gameObject);
        }
    }
}
