using System;
using UnityEngine;

public enum FacingModes
{
    CameraForward,
    PositionToCamera
}

public class AutoFacing : MonoBehaviour
{
    [Tooltip("For UI (or 2D), invert it")]
    public bool Inverted = false;

    public void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (Inverted)
        {
            transform.forward = transform.forward * -1.0f;
        }
    }
}
