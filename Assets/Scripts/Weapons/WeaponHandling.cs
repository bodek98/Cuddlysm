using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandling : MonoBehaviour
{

    [SerializeField] private int _selectedWeapon = 0;
    [SerializeField] private GameObject _weaponHolder;

    private Weapon _weapon;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<Weapon>();
        SelectWeapon();
    }

    public void OnWeaponAttack(InputAction.CallbackContext context)
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

    public void OnWeaponReloading(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _weapon.StartReloading();
        }
    }

    public void OnWeaponScrollSelection(InputAction.CallbackContext context)
    {
        int scrollDeltaY = (int)context.ReadValue<float>();

        switch (scrollDeltaY)
        {
            case > 0:
                if (_selectedWeapon >= _weaponHolder.transform.childCount - 1)
                {
                    _selectedWeapon = 0;
                }
                else
                {
                    _selectedWeapon++;
                }
                break;
            case < 0:
                if (_selectedWeapon <= 0)
                {
                    _selectedWeapon = _weaponHolder.transform.childCount - 1;
                }
                else
                {
                    _selectedWeapon--;
                }
                break;
        }

        _weapon.StopAttack();
        SelectWeapon();
    }

    public void OnWeaponNumericalSelection(InputAction.CallbackContext context)
    {
        int numericalSelection = (int)context.ReadValue<float>();

        if (numericalSelection >= 1 && numericalSelection <= _weaponHolder.transform.childCount)
        {
            _selectedWeapon = numericalSelection - 1;
            _weapon.StopAttack();
            SelectWeapon();
        } 
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform projectileWeapon in _weaponHolder.transform)
        {
            if (i == _selectedWeapon)
            {
                projectileWeapon.gameObject.SetActive(true);
                _weapon = projectileWeapon.GetComponent<Weapon>();
            }
            else
            {
                projectileWeapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
