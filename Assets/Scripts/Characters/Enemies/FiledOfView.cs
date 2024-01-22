using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledOfView : MonoBehaviour
{
    private GameObject _player;

    [Range(0, 360)]
    [SerializeField] private float _angle;
    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    [SerializeField] private bool _isPlayerVisible;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, directionToTarget);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    _isPlayerVisible = true;
                } else
                {
                    _isPlayerVisible = false;
                }
            } else
            {
                _isPlayerVisible = false;
            }
        } else if (_isPlayerVisible)
        {
            _isPlayerVisible = false;
        }
    }
}
