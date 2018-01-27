using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

    [SerializeField]
    private float angle;
    [SerializeField]
    private Transform playerTransf;
    [SerializeField]
    private float cameraDistance = 5;
    [SerializeField]
    private float smoothTime = 0.3f;

    private Vector3 velocity;

    void Start ()
    {
	    transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
	    transform.position = playerTransf.position - transform.forward * cameraDistance;
    }

    void Update ()
    {
        transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        Vector3 targetPosition = playerTransf.position - transform.forward * cameraDistance;
        transform.position = targetPosition;
    }
}
