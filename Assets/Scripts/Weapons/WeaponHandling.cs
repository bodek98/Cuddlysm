using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandling : MonoBehaviour
{

    [SerializeField] private int _selectedWeapon = 0;
    [SerializeField] private GameObject _weaponHolder;

    private Weapon _weapon;
    private PlayerInput _playerInput;

    public InputActionReference weaponAttackAction;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<Weapon>();
        SelectWeapon();
    }

    private void OnEnable()
    {
        weaponAttackAction.action.Enable();
        weaponAttackAction.action.performed += OnAttackPressed;
        weaponAttackAction.action.canceled += OnAttackReleased;
    }

    private void OnDisable()
    {
        weaponAttackAction.action.Disable();
        weaponAttackAction.action.performed -= OnAttackPressed;
        weaponAttackAction.action.canceled -= OnAttackReleased;
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
        {
            _weapon.Attack();
        }
    }

    private void OnAttackReleased(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0.5f)
        {
            _weapon.StopAttack();
        }
    }

    void OnWeaponReloading(InputValue value)
    {
        StartCoroutine(_weapon.Reload());
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

        _weapon.StopAttack();
        SelectWeapon();
    }

    void OnWeaponNumericalSelection(InputValue value)
    {
        int numericalSelection = (int)value.Get<float>();

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
