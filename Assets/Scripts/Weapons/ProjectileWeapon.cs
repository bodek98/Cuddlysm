using System;
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

    public override void Attack()
    {
        if (_isReloading) return;

        if (_magazineAmmo > 0)
        {
            FireProjectile();
        }
        if (_magazineAmmo == 0 && _storageAmmo > 0)
        {
            HandleReload();
        }
    }

    public void StartFire()
    {
        switch (_fireMode)
        {
            case FireMode.Single:
                Attack();
                break;

            case FireMode.Burst:
                for (global::System.Int32 i = 0; i < 3; i++)
                {
                    Attack();
                }
                break;

            case FireMode.FullAuto:
                InvokeRepeating("Attack", 0f, _attackDelay);
                break;
        }
    }

    public void StopFire()
    {
        if (_fireMode == FireMode.FullAuto)
        {
            CancelInvoke("Attack");
        }
    }

    public void ReloadMagazine()
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
        
        _isReloading = false;
    }

    private void HandleReload()
    {
        Debug.Log("Reloading");
        _isReloading = true;
        Invoke("ReloadMagazine", _reloadDuration);
    }
    
    private void FireProjectile()
    {
        if (Time.time < _nextTimeToAttack) return;
        _magazineAmmo--;
        _nextTimeToAttack += _attackDelay;

        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);
    }
}