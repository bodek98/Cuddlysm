using System;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _projectileForce = 1;
    [SerializeField] private float _attackDelay = 1;
    [SerializeField] private string _fireMode = "single";

    [SerializeField] private float _currentAmmo;
    [SerializeField] private float _magazineCapacity;
    [SerializeField] private float _maxAmmoStorage;

    private float _nextTimeToAttack = 0;

    public string FireMode
    {
        get { return _fireMode; }
        set { _fireMode = value; }
    }

    private void Start()
    {
        _currentAmmo = _magazineCapacity;
    }

    public void StartFire()
    {
        if (_fireMode == "single")
        {
            Attack();
        } else if (_fireMode == "burst")
        {
            for (global::System.Int32 i = 0; i < 3; i++)
            {
                Attack();
            }
        } else if (_fireMode == "auto")
        {
            InvokeRepeating("Attack", 0f, _attackDelay);
        }
    }

    public void StopFire()
    {
        if (_fireMode == "auto")
        {
            CancelInvoke("Attack");
        }
    }

    public void ReloadMagazine()
    {
        if (_maxAmmoStorage > 0)
        {
            if (_currentAmmo <= _maxAmmoStorage)
            {
                _maxAmmoStorage = _maxAmmoStorage - (_magazineCapacity - _currentAmmo);
                _currentAmmo = _magazineCapacity;
            } else
            {
                _currentAmmo = _maxAmmoStorage + _currentAmmo;
                _maxAmmoStorage = 0;
            }
        }
    }

    public override void Attack()
    {
        if (_currentAmmo > 0)
        {   
            FireProjectile();
            _currentAmmo--;
        } else
        {
            ReloadMagazine();
        }
    }
    private void FireProjectile()
    {
        if (Time.time < _nextTimeToAttack) return;
        _nextTimeToAttack += _attackDelay;

        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);
    }


}