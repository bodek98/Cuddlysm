using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _projectileForce = 1;
    [SerializeField] private float _attackDelay = 1;
    [SerializeField] private FireMode _fireMode = FireMode.Single;

    [SerializeField] private int _magazineAmmo;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _storageAmmo;
    // to uncomment during ammo picking up functionality development 
    // [SerializeField] private int _storageAmmoCapacity;

    [SerializeField] private float _reloadDuration = 2.5f;
    private float _timeOfFinishedReload = 0.0f;
    private bool _isReloading = false;

    private float _nextTimeToAttack = 0;

    private enum FireMode
    {
        Single,
        Burst,
        FullAuto
    }

    private void OnEnable()
    {
        CheckAutoReload();
    }

    public override void Attack()
    {
        switch (_fireMode)
        {
            case FireMode.Single:
                FireProjectile();
                break;

            case FireMode.Burst:
                StartCoroutine(FireBurst());
                break;

            case FireMode.FullAuto:
                InvokeRepeating(nameof(FireProjectile), 0f, _attackDelay);
                break;
        }
    }

    public override void StopAttack()
    {
        if (_fireMode == FireMode.FullAuto)
        {
            CancelInvoke(nameof(FireProjectile));
        }
    }

    public override void Reload()
    {
        _isReloading = true;
        Invoke(nameof(HandleReload), _reloadDuration);
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < 3; i++)
        {
            FireProjectile();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void FireProjectile()
    {
        if (_magazineAmmo <= 0 || Time.time < _nextTimeToAttack || _isReloading) return;
        _magazineAmmo--;
        _nextTimeToAttack += _attackDelay;

        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);

        CheckAutoReload();
    }

    private void CheckAutoReload()
    {
        if (_magazineAmmo == 0 && _storageAmmo > 0)
        {
            Reload();
        }
    }

    private void HandleReload()
    {
        if (transform.GameObject().activeSelf)
        {
            int missingAmmo = _magazineCapacity - _magazineAmmo;

            if (missingAmmo < _storageAmmo)
            {
                _magazineAmmo += missingAmmo;
                _storageAmmo -= missingAmmo;
            }
            else
            {
                _magazineAmmo += _storageAmmo;
                _storageAmmo = 0;
            }
        }

        _isReloading = false;
    }
}