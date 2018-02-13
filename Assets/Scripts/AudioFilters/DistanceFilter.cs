using UnityEngine;

public class DistanceFilter : MonoBehaviour, ICommand
{
    [SerializeField]
    private float currentDistance = 5.0f;

    public float CurrentDistance
    {
        get { return currentDistance; }
        set
        {
            currentDistance = Mathf.Clamp(value, MinDistance, MaxDistance);
        }
    }

    public float MaxDistance = 15.0f;
    public float MinDistance = 0.5f;

    public float VariationSpeed = 2.0f;

    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

    public void Awake()
    {
        CurrentDistance = MinDistance;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, CurrentDistance);
    }

    public float GetVolume(Vector3 position)
    {
        return Curve.Evaluate(Vector3.Distance(position, transform.position) /
                                CurrentDistance);
    }

    public void OnUpdate(float value)
    {
        CurrentDistance += value * VariationSpeed * Time.deltaTime;
    }
}
