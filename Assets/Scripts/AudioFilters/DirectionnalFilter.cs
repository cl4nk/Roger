using UnityEngine;

public class DirectionnalFilter : MonoBehaviour
{
    public float Angle = 10.0f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Angle);
        Gizmos.DrawRay(transform.position, quaternion * transform.right);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -Angle);

        Gizmos.DrawRay(transform.position, quaternion * transform.right);
    }
}
