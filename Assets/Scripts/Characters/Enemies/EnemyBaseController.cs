using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseController : MonoBehaviour
{
    private NavMeshAgent _agent;
    protected FieldOfView fov;
    private bool _needsToInspectLastPosition;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (!fov.currentTarget) return;

        if (fov.isTargetVisible)
        {
            Debug.Log(CalculateDestination());
            _agent.destination = CalculateDestination();
            _needsToInspectLastPosition = true;
        }
        else if (_needsToInspectLastPosition)
        {
            _agent.destination = fov.lastSeenPosition;
            _needsToInspectLastPosition = false;
        }
    }

    protected abstract Vector3 CalculateDestination();
}