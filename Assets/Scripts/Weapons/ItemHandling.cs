using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHandling : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode = GameMode.Combat;
    [SerializeField] private int _selectedItem = 0;
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private GameObject _buildingHolder;

    private Movement _movement;
    private GameObject _itemHolder;
    private Weapon _weapon;
    private BuildingTool _buildingTool;
    private int _lastWeaponItem;
    private int _lastBuildingToolItem;

    // Start is called before the first frame update
    void Start()
    {
        _itemHolder = _weaponHolder;
        _weapon = GetComponent<Weapon>();
        _buildingTool = GetComponent<BuildingTool>();
        _movement = GetComponent<Movement>();

        SelectItem();
    }

    public enum GameMode
    {
        Combat, Build
    }

    public void OnSwitchMode(InputAction.CallbackContext context)
    {
        switch (_gameMode)
        {
            case GameMode.Combat:
                _gameMode = GameMode.Build;
                _weaponHolder.SetActive(false);
                _buildingHolder.SetActive(true);
                _itemHolder = _buildingHolder;
                _weapon.StopAttack();
                _selectedItem = _lastBuildingToolItem;

                break;

            case GameMode.Build:
                _gameMode = GameMode.Combat;
                _buildingHolder.SetActive(false);
                _weaponHolder.SetActive(true);
                _itemHolder = _weaponHolder;
                _selectedItem = _lastWeaponItem;

                break;
        }

        _movement.itemHolder = _itemHolder;
        SelectItem();
    }

    public void OnItemAction(InputAction.CallbackContext context)
    {
        if (_gameMode == GameMode.Combat)
        {
            if (context.started)
            {
                _weapon.Attack();
            } 
            else if (context.canceled)
            {
                _weapon.StopAttack();
            }
        }
        else
        {
            if (context.started)
            {
                _buildingTool.UseTool();
            }
        }
    }

    public void OnWeaponReloading(InputAction.CallbackContext context)
    {
        if (_gameMode == GameMode.Combat)
        {
            if (context.started)
            {
                _weapon.StartReloading();
            }
        }
        else
        {
            return;
        }   
    }

    public void OnItemScrollSelection(InputAction.CallbackContext context)
    {
        int scrollDeltaY = (int)context.ReadValue<float>();

        switch (scrollDeltaY)
        {
            case > 0:
                if (_selectedItem >= _itemHolder.transform.childCount - 1)
                {
                    _selectedItem = 0;
                }
                else
                {
                    _selectedItem++;
                }
                break;
            case < 0:
                if (_selectedItem <= 0)
                {
                    _selectedItem = _itemHolder.transform.childCount - 1;
                }
                else
                {
                    _selectedItem--;
                }
                break;
        }

        if (_gameMode == GameMode.Combat) _weapon.StopAttack();
        SetLastItem();
        SelectItem();
    }

    public void OnItemNumericalSelection(InputAction.CallbackContext context)
    {
        int numericalSelection = (int)context.ReadValue<float>();

        if (numericalSelection >= 1 && numericalSelection <= _itemHolder.transform.childCount)
        {
            _selectedItem = numericalSelection - 1;

            if (_gameMode == GameMode.Combat) _weapon.StopAttack();
            SetLastItem();
            SelectItem();
        } 
    }

    private void SelectItem()
    {
        int i = 0;
        foreach (Transform item in _itemHolder.transform)
        {
            if (i == _selectedItem)
            {
                item.gameObject.SetActive(true);
                if (_gameMode == GameMode.Combat)
                {
                    _weapon = item.GetComponent<Weapon>();
                }
                else
                {
                    _buildingTool = item.GetComponent<BuildingTool>();
                }
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SetLastItem()
    {
        if (_gameMode == GameMode.Combat)
        {
            _lastWeaponItem = _selectedItem;
        }
        else
        {
            _lastBuildingToolItem = _selectedItem;
        }
    }
}
