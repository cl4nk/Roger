using UnityEngine;

public class RotationController : MonoBehaviour, ICommand
{
    #region Fields
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField] private GameObject feedback;
    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
    }

    public void OnCommandEnable()
    {
        if (feedback)
        {
            feedback.SetActive(true);
        }
    }

    public void OnCommandDisable()
    {
        if (feedback)
        {
            feedback.SetActive(false);
        }
    }

    public void EnterInputVector(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.0f)
        {
            float angle = Vector3.Angle(Vector3.right, direction);
            Quaternion targetQuaternion = Quaternion.Euler(0, 0, angle * Mathf.Sign(direction.y) * -1);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetQuaternion, rotationSpeed * Time.deltaTime);
        }
    }
}
