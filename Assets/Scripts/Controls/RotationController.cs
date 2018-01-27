using UnityEngine;

public class RotationController : MonoBehaviour, ICommand
{
    #region Fields
    [SerializeField]
    private float rotationSpeed = 5f;

    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
    }

    public void OnCommandEnable()
    {
    }

    public void OnCommandDisable()
    {
    }

    public void EnterInputVector(Vector2 direction)
    {
        Vector3 forward = new Vector3(direction.y, direction.x, 0.0f) * -1;

        if (forward.sqrMagnitude > 0.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(forward, Vector3.forward), rotationSpeed * Time.deltaTime);
        }
    }
}
