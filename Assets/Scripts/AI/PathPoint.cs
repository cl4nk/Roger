using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public float GizmoSize = 0.05f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, GizmoSize);
    }
}
