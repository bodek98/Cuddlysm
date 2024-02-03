using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseController : MonoBehaviour
{
    [SerializeField] private float _searchRange = 10;

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
        if (fov.currentTarget && fov.isTargetVisible)
        {
            WalkToTarget();
        }
        else if (_needsToInspectLastPosition)
        {
            InspectLastPosition();
        }
        else if (_agent.remainingDistance < 1)
        {
            SearchTarget();
        }
    }

    void WalkToTarget()
    {
        _agent.destination = CalculateDestination();
        _needsToInspectLastPosition = true;
    }

    void InspectLastPosition()
    {
        _agent.destination = fov.lastSeenPosition;
        _needsToInspectLastPosition = false;
    }

    void SearchTarget()
    {
        Vector3 searchDestination;
        if (RandomPoint(fov.lastSeenPosition, _searchRange, out searchDestination))
        {
            _agent.SetDestination(searchDestination);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    protected abstract Vector3 CalculateDestination();
}