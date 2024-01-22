using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject player;

    [Range(0, 360)]
    public float angle;
    public float radius;
    public bool isPlayerVisible;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
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
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

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
