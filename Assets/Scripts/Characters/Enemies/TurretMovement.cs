using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private Transform _playerTransform;
    private float _distance;

    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private float _gunHeight = 2.25f;
    [SerializeField] private float _aggroDistance;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        _distance = Vector3.Distance(_playerTransform.position, transform.position);

        if (_distance <= _aggroDistance) {
            float singleStep = _rotationSpeed * Time.deltaTime;

            Vector3 targetHeadDirection = _playerTransform.position - _head.transform.position;
            Vector3 targetWeaponDirection = _playerTransform.position - _weapon.transform.position;

            Vector3 newHeadDirection = Vector3.RotateTowards(_head.transform.forward, targetHeadDirection, singleStep, 0.0f);
            Vector3 newWeaponDirection = Vector3.RotateTowards(_weapon.transform.forward, targetWeaponDirection, singleStep, 0.0f);

            _head.transform.rotation = Quaternion.LookRotation(new Vector3(newHeadDirection.x, 0, newHeadDirection.z));
            _weapon.transform.rotation = Quaternion.LookRotation(newWeaponDirection);
        }
    }
}
