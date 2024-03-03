using System;
using System.Collections;
using UnityEngine;

public class ProjectileWeapon : ReloadableWeapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _projectileForce = 1;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private float _burstDelay = 0.05f;
    [SerializeField] private FireMode _fireMode = FireMode.Single;

    private bool _isBursting = false;
    private float _nextTimeToAttack = 0;
    private IEnumerator _fullAutoCoroutine;

    private enum FireMode
    {
        Single,
        Burst,
        FullAuto
    }

    private void OnEnable()
    {
        _fullAutoCoroutine = FireFullAuto();
        RefreshWeapon();
    }

    private void OnDisable()
    {
        _isBursting = false;
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
        if (_fireMode == FireMode.FullAuto && _fullAutoCoroutine != null)
        {
            StopCoroutine(_fullAutoCoroutine);
        }
    }

    // Internal functions

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
            // Todo: Ensure delay in case of stopping coroutine
            FireProjectile();
            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void FireProjectile(bool ignoreAttackDelay = false)
    {
        bool isReadyToShoot = ignoreAttackDelay || Time.time >= _nextTimeToAttack;
        if (_magazineAmmo <= 0 || !isReadyToShoot || _isReloading) return;
        if (!ignoreAttackDelay) _nextTimeToAttack = Time.time + _attackDelay;

        _magazineAmmo--;

        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.layer = gameObject.layer;
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);

        HandleGUIUpdate();
        CheckAutoReload();
    }
}
