using UnityEngine;

public class DirectionnalFilter : ADirectionnable, ICommand
{
    public float MinAngle = 5f;
    public float MaxAngle = 30f;

    [SerializeField]
    private float angle = 10.0f;
    public float Angle
    {
        get { return angle; }
        set
        {
            angle = Mathf.Clamp(value, MinAngle, MaxAngle);
        }
    }

    public float VariationSpeed = 5f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Quaternion quaternion = Quaternion.Euler(0.0f, Angle, 0.0f);
        Gizmos.DrawRay(transform.position, quaternion * transform.forward);

        quaternion = Quaternion.Euler(0.0f, -Angle, 0.0f);

        Gizmos.DrawRay(transform.position, quaternion * transform.forward);
    }

    public void OnUpdate(float value)
    {
        Angle = VariationSpeed * value * Time.deltaTime;
    }
}
