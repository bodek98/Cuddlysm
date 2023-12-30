using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandling : MonoBehaviour
{

    [SerializeField] private int _selectedWeapon = 0;
    [SerializeField] private GameObject _weaponHolder;

    private ProjectileWeapon _projectileWeapon;
    private PlayerInput _playerInput;

    public InputActionReference weaponShootAction;

    // Start is called before the first frame update
    void Start()
    {
        _projectileWeapon = GetComponent<ProjectileWeapon>();
        SelectWeapon();
    }

    private void OnEnable()
    {
        weaponShootAction.action.Enable();
        weaponShootAction.action.performed += OnButtonPressed;
        weaponShootAction.action.canceled += OnButtonReleased;
    }

    private void OnDisable()
    {
        weaponShootAction.action.Disable();
        weaponShootAction.action.performed -= OnButtonPressed;
        weaponShootAction.action.canceled -= OnButtonReleased;
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
        {
            _projectileWeapon.StartFire();
        }
    }

    private void OnButtonReleased(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0.5f)
        {
            _projectileWeapon.StopFire();
        }
    }

    void OnWeaponReloading(InputValue value)
    {
            _projectileWeapon.ReloadMagazine();
    }

    void OnWeaponScrollSelection(InputValue value)
    {
        int scrollDeltaY = (int)value.Get<float>();

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

        _projectileWeapon.StopFire();
        SelectWeapon();
    }

    void OnWeaponNumericalSelection(InputValue value)
    {
        int numericalSelection = (int)value.Get<float>();

        if (numericalSelection >= 1 && numericalSelection <= _weaponHolder.transform.childCount)
        {
            _selectedWeapon = numericalSelection - 1;
            _projectileWeapon.StopFire();
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
                _projectileWeapon = projectileWeapon.GetComponent<ProjectileWeapon>();
            }
            else
            {
                projectileWeapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
