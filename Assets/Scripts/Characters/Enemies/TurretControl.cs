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
            float singleStep = _rotationSpeed * Time.deltaTime;

            Vector3 targetHeadPosition = _currentTarget.transform.position ;
            Vector3 aimTurretDirection = targetHeadPosition - _head.transform.position;
            Vector3 aimWeaponDirection = targetHeadPosition - _weapon.transform.position;

            Vector3 newTurretDirection = Vector3.RotateTowards(_head.transform.forward, aimTurretDirection, singleStep, 0.0f);
            Vector3 newWeaponDirection = Vector3.RotateTowards(_weapon.transform.forward, aimWeaponDirection, singleStep, 0.0f);

            _head.transform.rotation = Quaternion.LookRotation(new Vector3(newTurretDirection.x, 0, newTurretDirection.z));
            _weapon.transform.rotation = Quaternion.LookRotation(newWeaponDirection);

            float targetTurretAngle = Vector3.Angle(aimWeaponDirection, newWeaponDirection);
            if (targetTurretAngle < 5.0f)
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
