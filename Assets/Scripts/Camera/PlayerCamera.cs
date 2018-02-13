using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public bool InitOnStartup = true;

    [SerializeField]
    private Transform playerTransf;

    public Vector3 Direction;
    public Vector3 Forward;

    public float RotationSpeed = 10.0f;
    public float SmoothTime = 0.3f;

    private Vector3 velocity;

    private void OnEnable()
    {
        if (InitOnStartup)
        {
            Direction = playerTransf.position - transform.position;
            Forward = transform.forward;
        }
    }

    private void Update ()
    {
        Vector3 newForward = playerTransf.rotation * Forward;
        Vector3 newDirection = playerTransf.rotation * Direction;

        Quaternion targetRotation = Quaternion.LookRotation(newForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);

        transform.position = Vector3.SmoothDamp(transform.position, playerTransf.position - newDirection, ref velocity, SmoothTime);
    }
}
