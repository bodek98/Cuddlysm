using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : BuildingTool
{

    [SerializeField] private Movement _movement;
    [SerializeField] private GameObject _previewBuilding;
    [SerializeField] private GameObject _buildingObject;
    [SerializeField] private float _buildDelay = 5f;
    [SerializeField] private float _buildCost = 50f;

    private float _nextTimeToBuild = 0;


    private void OnEnable()
    {
        _weaponGUIUpdater.SetWeaponSprite(sprite);
        _weaponGUIUpdater.UpdateAmmoGUI("-", "-");
    }

    private void Update()
    {
        Vector3 buildPosition = new(_movement.targetPosition.x, 0, _movement.targetPosition.z);

        if (!_previewBuilding.activeSelf) _previewBuilding.SetActive(true);
        _previewBuilding.transform.position = buildPosition;
    }

    public override void UseTool()
    {
        bool isReadyToBuild = readyToBuild && Time.time >= _nextTimeToBuild;
        if (!isReadyToBuild || playerEntity.currentStamina < _buildCost) return;

        Build();
        StartCoroutine(WaitForNextBuildAndUpdateGUI(_buildDelay));
        UpdateStaminaBar();
    }

    private void Build()
    {
        Vector3 buildPosition = new(_movement.targetPosition.x, 0, _movement.targetPosition.z);

        _nextTimeToBuild = Time.time + _buildDelay;

        playerEntity.currentStamina -= _buildCost;

        GameObject newBuilding = Instantiate(_buildingObject, buildPosition, transform.rotation);
        newBuilding.layer = gameObject.layer;
    }

    protected IEnumerator WaitForNextBuildAndUpdateGUI(float duration)
    {
        float timePassed = 0f;
        float animationFrameDelay = 0.01f;

        while (timePassed < duration)
        {
            _weaponGUIUpdater.FillAmmoBar(timePassed / duration);
            yield return new WaitForSeconds(animationFrameDelay);
            timePassed += animationFrameDelay;
        }
    }
}
