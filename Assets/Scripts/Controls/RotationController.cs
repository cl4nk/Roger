using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    #region Fields
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private string horizontalRotationAxis = "HorizontalRotation";

    [SerializeField]
    private string verticalRotationAxis = "VerticalRotation";

    #endregion

    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        Vector3 forward = new Vector3(Input.GetAxis(horizontalRotationAxis), Input.GetAxis(verticalRotationAxis), 0.0f);

        if (forward.sqrMagnitude > 0.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(forward, Vector3.forward), rotationSpeed * Time.deltaTime);
        }
    }
}
