using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    private Weapon _projectileWeapon;
    private Transform _playerTransform;
    private float _distance;

    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _aggroDistance;

    void Start()
    {
        _projectileWeapon = _weapon.GetComponent<Weapon>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        _distance = Vector3.Distance(_playerTransform.position, transform.position);

        if (_distance <= _aggroDistance) {
            float singleStep = _rotationSpeed * Time.deltaTime;

            Vector3 playerHeadPostion = _playerTransform.position + new Vector3(0, 1.5f, 0);
            Vector3 targetHeadDirection = playerHeadPostion - _head.transform.position;
            Vector3 targetWeaponDirection = playerHeadPostion - _weapon.transform.position;

            Vector3 newHeadDirection = Vector3.RotateTowards(_head.transform.forward, targetHeadDirection, singleStep, 0.0f);
            Vector3 newWeaponDirection = Vector3.RotateTowards(_weapon.transform.forward, targetWeaponDirection, singleStep, 0.0f);

            _head.transform.rotation = Quaternion.LookRotation(new Vector3(newHeadDirection.x, 0, newHeadDirection.z));
            _weapon.transform.rotation = Quaternion.LookRotation(newWeaponDirection);

            float playerTurretAngle = Vector3.Angle(targetWeaponDirection, newWeaponDirection);
            if (playerTurretAngle < 5.0f)
            {
                _projectileWeapon.Attack();
            }
        }
    }
}
