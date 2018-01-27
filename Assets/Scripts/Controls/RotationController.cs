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

    public void EnterInputVector(Vector2 direction)
    {
        Vector3 forward = new Vector3(direction.x, direction.y, 0.0f);

        if (forward.sqrMagnitude > 0.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(forward, Vector3.forward), rotationSpeed * Time.deltaTime);
        }
    }
}
