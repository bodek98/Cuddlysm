using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TurretControl : MonoBehaviour
{
    private Weapon _projectileWeapon;
    private List<GameObject> _targetList = new List<GameObject>();
    private GameObject _currentTarget;

    [TagField]
    [SerializeField] private string _targetTag;
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private float _rotationSpeed = 1.0f;

    void Start()
    {
        _projectileWeapon = _weapon.GetComponent<Weapon>();
    }

    void Update()
    {
        if (_currentTarget)
        {
            TurretStep();
        } else
        {
            FindNearestTarget();
        }
    }

    private void TurretStep()
    {
            float singleStepDeg = _rotationSpeed * Time.deltaTime;
            float singleStepRad = singleStepDeg * Mathf.Deg2Rad;

            Vector3 targetHeadPosition = _currentTarget.transform.position;

            // Head rotation (only yaw)
            Vector3 aimTurretDirection = targetHeadPosition - _head.transform.position;
            Vector3 newTurretDirection = Vector3.RotateTowards(_head.transform.forward, aimTurretDirection, singleStepRad, 0.0f);
            _head.transform.rotation = Quaternion.LookRotation(new Vector3(newTurretDirection.x, 0, newTurretDirection.z));

            // Weapon rotation (only pitch)
            Vector3 aimWeaponDirection = targetHeadPosition - _weapon.transform.position;
            float angleWeaponTarget = Vector3.SignedAngle(_weapon.transform.forward, aimWeaponDirection, _weapon.transform.right);
            float weaponPitchAdjustment = Mathf.Clamp(angleWeaponTarget, -singleStepDeg, singleStepDeg);
            _weapon.transform.Rotate(Vector3.right, weaponPitchAdjustment, Space.Self);

            if (Mathf.Abs(angleWeaponTarget) < 5.0f)
            {
                _projectileWeapon.Attack();
            }
    }

    private void FindNearestTarget()
    {
        float minimalDistance = float.PositiveInfinity;

        _targetList.ForEach(target =>
        {
            if (!target) return;
            
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                _currentTarget = target;
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        _targetList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetInstanceID() == _currentTarget.GetInstanceID())
        {
            _currentTarget = null;
        }
        _targetList.Remove(other.gameObject);
    }
}
