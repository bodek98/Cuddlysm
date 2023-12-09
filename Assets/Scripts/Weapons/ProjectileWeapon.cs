using System;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _projectileForce = 1;
    [SerializeField] private float _attackDelay = 1;
    
    private float _nextTimeToAttack = 0;
    
    public override void Attack()
    {
        if (Time.time < _nextTimeToAttack) return;
        _nextTimeToAttack += _attackDelay;
    
        GameObject newProjectile = Instantiate(_projectile, _muzzle.transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * _projectileForce, ForceMode.Impulse);
    }
}
