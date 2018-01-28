using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

    [SerializeField]
    private float angle;
    [SerializeField]
    private Transform playerTransf;
    [SerializeField]
    private float cameraDistance = 5;

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
