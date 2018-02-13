using UnityEngine;

public class RotationController : MonoBehaviour, ICommand
{
    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0.0f, 0.0f, 5f);

    public void OnUpdate(float value)
    {
        transform.rotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime * value);
    }
}