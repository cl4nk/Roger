using UnityEngine;

public class DistanceFilter : MonoBehaviour
{
    public float MaxDistance = 5.0f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
