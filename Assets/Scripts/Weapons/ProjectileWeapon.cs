using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _projectileForce = 1;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private float _burstDelay = 0.05f;
    [SerializeField] private FireMode _fireMode = FireMode.Single;

    [SerializeField] private int _magazineAmmo;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _storageAmmo;
    // to uncomment during ammo picking up functionality development 
    // [SerializeField] private int _storageAmmoCapacity;

    [SerializeField] private float _reloadDuration = 2.5f;
    private float _timeOfFinishedReload = 0.0f;
    private bool _isReloading = false;
    private bool _isBursting = false;

    private float _nextTimeToAttack = 0;

    private IEnumerator _fullAutoCoroutine;

    private enum FireMode
    {
        Single,
        Burst,
        FullAuto
    }

    public void Start()
    {
        _fullAutoCoroutine = FireFullAuto();
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
                StartCoroutine(_fullAutoCoroutine);
                break;
        }
    }

    public override void StopAttack()
    {
        if (_fireMode == FireMode.FullAuto)
        {
            StopCoroutine(_fullAutoCoroutine);
        }
    }

    public override IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_reloadDuration);

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

    private IEnumerator FireBurst()
    {
        if (Time.time < _nextTimeToAttack || _isBursting) yield break;
        _isBursting = true;

        for (int i = 0; i < 3; i++)
        {
            FireProjectile(true);
            yield return new WaitForSeconds(_burstDelay);
        }

        _isBursting = false;
        _nextTimeToAttack += _attackDelay;
    }

    private IEnumerator FireFullAuto()
    {
        while (true)
        {
            FireProjectile();
            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void FireProjectile(bool ignoreAttackDelay = false)
    {
        bool isReadyToShoot = ignoreAttackDelay || Time.time >= _nextTimeToAttack;
        if (_magazineAmmo <= 0 || !isReadyToShoot || _isReloading) return;
        if (!ignoreAttackDelay) _nextTimeToAttack += _attackDelay;
        
        _magazineAmmo--;

        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);

        CheckAutoReload();
    }

    private void CheckAutoReload()
    {
        if (_magazineAmmo == 0 && _storageAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }
}
