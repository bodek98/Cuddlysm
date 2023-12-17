using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandling : MonoBehaviour
{

    [SerializeField] private float _selectedWeapon = 0;
    [SerializeField] private GameObject _weaponHolder;

    private ProjectileWeapon _projectileWeapon;
    private PlayerInput _playerInput;

    // Start is called before the first frame update
    void Start()
    {
        _projectileWeapon = GetComponent<ProjectileWeapon>();
        SelectWeapon();
    }

    void OnWeaponShoot(InputValue value)
    {
        string fireMode = _projectileWeapon.FireMode;

        if (fireMode == "single")
        {
            _projectileWeapon.Attack();

        } else if (fireMode == "auto")
        {
            Debug.Log("auto");
        }
    }

    void OnWeaponReloading(InputValue value)
    {
        Debug.Log("reload");
    }

    void OnWeaponScrollSelection(InputValue value)
    {
        float scrollDeltaY = value.Get<float>();

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

        SelectWeapon();
    }

    void OnWeaponNumericalSelection(InputValue value)
    {
        float numericalSelection = value.Get<float>();
        _selectedWeapon = numericalSelection - 1;

        SelectWeapon();
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
