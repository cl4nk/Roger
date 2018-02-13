using UnityEngine;

public abstract class ADirectionnable : MonoBehaviour
{
    public static readonly Vector3 localDirection = Vector3.forward;
    public Vector3 Direction
    {
        get
        {
            return transform.TransformDirection(localDirection);
        }
    }
}
