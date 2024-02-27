using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingTool : MonoBehaviour
{
    public bool readyToBuild = true; 
    public Sprite sprite = null;
    [SerializeField] protected PlayerEntity playerEntity; 
    [SerializeField] private PlayerStaminaBar _playerStaminaBar;

    protected WeaponGUIUpdater _weaponGUIUpdater;

    public void Awake()
    {
        _weaponGUIUpdater = GetComponentInParent<WeaponGUIUpdater>();
    }

    protected void UpdateStaminaBar()
    {
        _playerStaminaBar.UpdateStaminaBar(playerEntity.currentStamina, playerEntity.maxStamina);
    }

    public abstract void UseTool();

    public void OnBuildingTriggerEnter()
    {
        Debug.Log("false");
        readyToBuild = false;
    }

    public void OnBuildingTriggerExit()
    {
        Debug.Log("true");
        readyToBuild = true;
    }
}
