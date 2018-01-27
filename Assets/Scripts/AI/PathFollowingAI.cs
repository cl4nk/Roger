﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollowingAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] pathPoints;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float reachMargin = 0.5f;

    private int currentDestination = 0;
    private int increment = 1;

    private Vector3? TempDestination;

    private NoiseDetection noiseDetection;
    private float rotationSpeed = 10.0f;

    public void OnEnable()
    {
        noiseDetection = GetComponent<NoiseDetection>();
        if (noiseDetection)
        {
            noiseDetection.OnNoiseDetected += SetTempDestination;
        }
    }

    public void OnDisable()
    {
        if (noiseDetection)
        {
            noiseDetection.OnNoiseDetected += SetTempDestination;
        }
    }

	// Update is called once per frame
	private void Update ()
    {
        Vector3? target = GetTarget();
        if (target == null)
            return;

        Vector3 direction = target.Value - transform.position;
        direction.Normalize();

        transform.position += (direction * Time.deltaTime * speed);

        direction = new Vector3(direction.y, -direction.x);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(direction, Vector3.forward), rotationSpeed * Time.deltaTime);

        if (HasReachDestination())
        {
            Increment();
        }
	}

    private Vector3? GetTarget()
    {
        if (!TempDestination.HasValue && pathPoints.Length == 0)
            return null;

        return TempDestination.HasValue ? TempDestination.Value : pathPoints[currentDestination].position;
    }

    private void Increment()
    {
        if (TempDestination.HasValue)
        {
            TempDestination = null;
        }
        else
        {
            currentDestination += increment;

            if (currentDestination == 0 || currentDestination == pathPoints.Length - 1)
            {
                increment = -increment;
            }
        }
    }

    private bool HasReachDestination()
    {
        Vector3? target = GetTarget();
        if (target == null)
            return false;
        return Vector3.Distance(transform.position, target.Value) <= reachMargin;
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

    public void SetTempDestination(Vector3 position)
    {
        TempDestination = position;
    }

    public void OnDrawGizmos()
    {
        Vector3? target = GetTarget();
        if (target == null)
            return;
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(target.Value, reachMargin);
    }
}
