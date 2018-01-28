using System.Collections;
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

    private Vector3? TempDirection;

    public float FixedDirectionDuration = 2.0f;

    private NoiseDetection noiseDetection;
    [SerializeField]
    private float rotationSpeed = 10.0f;

    private float fixedDirectionTime = 0.0f;

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
        if (TempDirection.HasValue)
        {
            if (fixedDirectionTime > Time.time)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(TempDirection.Value, Vector3.forward), rotationSpeed * Time.deltaTime);
                return;
            }
            else
            {
                TempDirection = null;
            }
        }

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
        if (pathPoints == null)
            return null;
        if (pathPoints.Length == 0)
            return null;

        return pathPoints[currentDestination].position;
    }

    private void Increment()
    {
        currentDestination += increment;

        if (currentDestination == 0 || currentDestination == pathPoints.Length - 1)
        {
            increment = -increment;
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
        TempDirection = position - transform.position;

        fixedDirectionTime = FixedDirectionDuration + Time.time;
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
