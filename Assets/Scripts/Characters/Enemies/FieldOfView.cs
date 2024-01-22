using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [Range(0, 360)]
    public float angle;
    public float radius;
    public bool isPlayerVisible;
    public GameObject currentTarget;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;


    void Start()
    {
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
        float minimalDistance = float.PositiveInfinity;

        if (rangeChecks.Length != 0)
        {
            foreach (Collider rangeCheck in rangeChecks)
            {
                float distance = Vector3.Distance(rangeCheck.transform.position, transform.position);

                if (distance < minimalDistance)
                {
                    minimalDistance = distance;
                    currentTarget = rangeCheck.gameObject;
                }
            }
            
            Vector3 directionToTarget = (currentTarget.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    isPlayerVisible = true;
                } else
                {
                    isPlayerVisible = false;
                }
            } else
            {
                isPlayerVisible = false;
            }
        } else if (isPlayerVisible)
        {
            isPlayerVisible = false;
        }
    }
}
