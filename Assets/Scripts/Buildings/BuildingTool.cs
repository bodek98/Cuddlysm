using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingTool : MonoBehaviour
{
    public Sprite sprite = null;

    [SerializeField] protected PlayerEntity playerEntity; 
    [SerializeField] private PlayerStaminaBar _playerStaminaBar;

    protected void UpdateStaminaBar()
    {
        _playerStaminaBar.UpdateStaminaBar(playerEntity.currentStamina, playerEntity.maxStamina);
    }

    public abstract void UseTool();
}
