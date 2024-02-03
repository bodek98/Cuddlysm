using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseController : MonoBehaviour
{
    protected FieldOfView fov;
    protected Weapon weapon;
    private NavMeshAgent _agent;
    private bool _needsToInspectLastPosition;

    [SerializeField] private float _followTimeTreshHold = 1;
    [SerializeField] private float _safeDistanceToPlayer = 5;
    [SerializeField] private float _rotationStep = 5;
    [SerializeField] private float _searchRange = 10;
    [SerializeField] private GameObject _weaponObject;
    [SerializeField] protected GameObject weaponHolder;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
        weapon = _weaponObject.GetComponent<Weapon>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Debug.Log(fov.targetLastSeenTimestamp);
        
        if ((fov.currentTarget && fov.isTargetVisible) || WasTargetRecentlySeen())
        {
            HeadToTarget();
            AttackTarget();
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

    void HeadToTarget()
    {
        _agent.stoppingDistance = _safeDistanceToPlayer;
        _agent.SetDestination(fov.currentTarget.transform.position);

        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _agent.updateRotation = false;
            FaceTarget(fov.currentTarget.transform.position);
        }
        else
        {
            _agent.updateRotation = true;
        }

        _needsToInspectLastPosition = true;
    }

    void InspectLastPosition()
    {
        _agent.updateRotation = true;
        _agent.stoppingDistance = 0;
        _agent.SetDestination(fov.lastSeenPosition);
        _needsToInspectLastPosition = false;
    }

    void SearchTarget()
    {
        _agent.updateRotation = true;
        _agent.stoppingDistance = 0;
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

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(lookPos);
        float interpolationRatio = Mathf.Clamp(_rotationStep * Time.deltaTime, 0, 1);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, interpolationRatio);
    }

    private bool WasTargetRecentlySeen()
    {
        return Time.time < fov.targetLastSeenTimestamp + _followTimeTreshHold;
    }

    protected abstract void AttackTarget();
}