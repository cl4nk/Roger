using System;
using UnityEditor;
using UnityEngine;

public class VisualDetection : ADirectionnable
{
    public static event Action<VisualDetection, Transform> StaticOnPlayerDetected;

    public event Action<Transform> OnPlayerDetected;

    [Range(0, 360)]
    public float Angle = 15.0f;

    [Range(0, float.MaxValue)]
    public float Distance = 5.0f;

    public LayerMask raycastMask;

    [SerializeField]
    private Transform target;
    public Transform Target
    {
        get
        {
            if (target == null)
            {
                target = Player.Instance.transform;
            }
            return target;
        }
    }

    public void Update()
    {
        float playerDistance = Vector3.Distance(Target.position, transform.position);

        if (playerDistance > Distance)
            return;

        Vector3 directionToPlayer = Target.position - transform.position;

        if (Target.GetComponent<SpriteRenderer>().enabled == false)
        {
            return;
        }

        float angle = Vector3.Angle(Direction, directionToPlayer.normalized);

        if (angle > Angle)
            return;

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Target.position - transform.position, Vector2.Distance(Target.position, transform.position), raycastMask);

        if (raycastHit.collider == null)
        {

            if (StaticOnPlayerDetected != null)
                StaticOnPlayerDetected.Invoke(this, Target);

            if (OnPlayerDetected != null)
                OnPlayerDetected.Invoke(Target);
        }
       
    }

    public void OnDrawGizmos()
    {
        Vector3 endpoint = transform.position + (Quaternion.Euler(0, -Angle * 0.5f, 0) * transform.forward);

        Handles.color = new Color(0, 0, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, transform.up, (endpoint - transform.position).normalized, Angle, Distance);
    }
}
