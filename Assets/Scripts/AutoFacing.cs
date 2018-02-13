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

    public FacingModes FacingMode = FacingModes.PositionToCamera;

    private Vector3 GetBackward(FacingModes facingMode)
    {
        switch (facingMode)
        {
            case FacingModes.CameraForward:
                return Camera.main.transform.forward;
            case FacingModes.PositionToCamera:
                return (transform.position - Camera.main.transform.position).normalized;
            default:
                throw new NotImplementedException();
        }
    }

    public void Update()
    {
        Vector2 backward = GetBackward(FacingMode);
        transform.forward = Inverted ? backward : backward * -1;
    }
}
