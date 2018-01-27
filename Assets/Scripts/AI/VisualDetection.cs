using UnityEngine;
using UnityEngine.Events;

public class VisualDetection : MonoBehaviour
{
    [Range(0, 360)]
    public float Angle = 10.0f;

    [Range(0, float.MaxValue)]
    public float Distance = 5.0f;

    public Transform player;

    public UnityEvent OnPlayerDetected;

    public void Update()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);
        if (playerDistance > Distance)
            return;

        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.right, directionToPlayer);

        if (angle > Angle)
            return;

        OnPlayerDetected.Invoke();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Angle);
        Gizmos.DrawRay(transform.position, quaternion * transform.right);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -Angle);

        Gizmos.DrawRay(transform.position, quaternion * transform.right);
    }
}
