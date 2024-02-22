using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flamethrower : ReloadableWeapon
{
    [SerializeField] private GameObject flame;
    [SerializeField] private float _flameAmmoDecreaseDelay = 0.1f;

    private IEnumerator _flameFire;
    
    private void OnEnable()
    {
        RefreshWeapon();
    }

    public override void Attack()
    {
        _flameFire = FireFlame();
        StartCoroutine(_flameFire);
    }

    public override void StopAttack()
    {
        if (_flameFire != null)
        {
            StopCoroutine(_flameFire);
        }
        flame.SetActive(false);
    }

    private IEnumerator FireFlame()
    {
        while (true)
        {
            if (!_isReloading)
            {
                if (_magazineAmmo > 0)
                {
                    flame.SetActive(true);
                    _magazineAmmo -= 1;
                    HandleGUIUpdate();
                }
                else
                {
                    flame.SetActive(false);
                    StartReloading();
                }
            }

            yield return new WaitForSeconds(_flameAmmoDecreaseDelay);
        }
    }

}
