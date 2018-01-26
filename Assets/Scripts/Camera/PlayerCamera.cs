using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

    private Transform transf;

    [SerializeField]
    private float angle;
    [SerializeField]
    private Transform playerTransf;
    [SerializeField]
    private float cameraDistance = 5;
    [SerializeField]
    private float smoothTime = 0.3f;
    

	void Start () {
        transf = transform;
        transf.rotation = Quaternion.Euler(new Vector3(90 - angle, 0, 0));
        transf.position = playerTransf.position - transf.forward * cameraDistance;
    }

    Vector3 velocity;
    void Update () {
        transf.rotation = Quaternion.Euler(new Vector3(90 - angle, 0, 0));
        transf.position = Vector3.SmoothDamp(transf.position, playerTransf.position - transf.forward * cameraDistance, ref velocity, smoothTime);

    }
}
