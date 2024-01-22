using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent _agent;
    private FieldOfView _fov;
    private bool _needsToInspectLastPosition;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _fov = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (!_fov.currentTarget) return;

        if (_fov.isTargetVisible)
        {
            _agent.destination = _fov.currentTarget.transform.position;
            _needsToInspectLastPosition = true;
        }
        else if (_needsToInspectLastPosition)
        {
            _agent.destination = _fov.lastSeenPosition;
            _needsToInspectLastPosition = false;
        }
    }
}
