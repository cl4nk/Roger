using UnityEngine;
using UnityEngine.Events;

public abstract class InteractiveObject : MonoBehaviour
{
    public float Range = 0.5f;
    public string Label = "InteractiveObject";
    public abstract void Interact(InteractiveController controller);
}
