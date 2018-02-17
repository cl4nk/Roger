using UnityEditor;
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
        Vector3 endpoint = transform.position + (Quaternion.Euler(0, -Angle * 0.5f, 0) * transform.forward);

        Handles.color = new Color(1, 0.92f, 0.016f, 0.2f);
        Handles.DrawSolidArc(transform.position, transform.up, (endpoint - transform.position).normalized, Angle, 10.0f);
    }

    public void OnUpdate(float value)
    {
        Angle = VariationSpeed * value * Time.deltaTime;
    }
}
