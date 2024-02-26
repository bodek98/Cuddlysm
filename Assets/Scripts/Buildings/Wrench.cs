using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : BuildingTool
{
    [SerializeField] private GameObject _building;
    [SerializeField] private float _buildDelay = 0.1f;
    [SerializeField] private float _buildCost = 50f;

    private float _nextTimeToBuild = 0;
    private WeaponGUIUpdater _weaponGUIUpdater;
    private Movement _movement;

    public void Awake()
    {
        _weaponGUIUpdater = GetComponentInParent<WeaponGUIUpdater>();
        _movement = _weaponGUIUpdater.GetComponentInParent<Movement>();
    }

    public override void UseTool()
    {
        Build();
        UpdateStaminaBar();
    }

    private void Build(bool ignoreBuildDelay = false)
    {
        bool isReadyToBuild = ignoreBuildDelay || Time.time >= _nextTimeToBuild;
        if (!isReadyToBuild || playerEntity.currentStamina < _buildCost) return;
        if (!ignoreBuildDelay) _nextTimeToBuild = Time.time + _buildDelay;

        playerEntity.currentStamina -= _buildCost;

        GameObject newBuilding = Instantiate(_building, _movement.aimDirection, transform.rotation);
        newBuilding.layer = gameObject.layer;

        /*HandleGUIUpdate();*/
    }
}
