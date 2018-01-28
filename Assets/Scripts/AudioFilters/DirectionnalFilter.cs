using UnityEngine;

public class DirectionnalFilter : MonoBehaviour, ICommand
{
    public float Angle = 10.0f;

    private Vector3 LastInput;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Angle);
        Gizmos.DrawRay(transform.position, quaternion * transform.right);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -Angle);

        Gizmos.DrawRay(transform.position, quaternion * transform.right);
    }

    public void OnCommandEnable()
    {
    }

    public void OnCommandDisable()
    {
    }

    public void EnterInputVector(Vector2 direction)
    {
        if (direction.sqrMagnitude == 0.0f)
        {
            LastInput = Vector2.zero;
        }
        else
        {
            Vector3 newDirection = direction.normalized;
            if (LastInput.sqrMagnitude > 0.0f)
            {
                Angle += Vector2.SignedAngle(LastInput, newDirection) * Time.deltaTime * -Angle;
                Angle = Mathf.Clamp(Angle, 0, 180);
            }
            LastInput = newDirection;
        }
    }
}
