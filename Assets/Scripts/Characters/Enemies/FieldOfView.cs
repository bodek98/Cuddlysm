using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)]
    public float angle;
    public float radius;
    public bool isTargetVisible;
    public Vector3 lastSeenPosition;
    public GameObject currentTarget;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    void Start()
    {
        lastSeenPosition = transform.position;
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            if (!isTargetVisible)
            {
                FindNearestTarget(rangeChecks);
            }
            
            Vector3 directionToTarget = (currentTarget.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    isTargetVisible = true;
                    lastSeenPosition = currentTarget.transform.position;
                } else
                {
                    isTargetVisible = false;
                }
            } else
            {
                isTargetVisible = false;
            }
        } else if (isTargetVisible)
        {
            isTargetVisible = false;
        }
    }

    private void FindNearestTarget(Collider[] rangeChecks)
    {
        float minimalDistance = float.PositiveInfinity;

        foreach (Collider rangeCheck in rangeChecks)
        {
            float distance = Vector3.Distance(rangeCheck.transform.position, transform.position);

            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                currentTarget = rangeCheck.gameObject;
            }
        }
    }
}
