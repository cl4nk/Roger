using UnityEngine;

public class DirectionController : MonoBehaviour
{
    enum Direction { North, East, South, West };

    [SerializeField]
    private Transform refTransform;
    public Transform RefTransform
    {
        get
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }
            return refTransform;
        }
    }

    private Animator controller;
    public Animator Controller
    {
        get
        {
            if (controller == null)
            {
                controller = GetComponent<Animator>();
            }
            return controller;
        }
    }

    public void Update()
    {
        Direction dir;
        float angle = RefTransform.localRotation.z % 360;
        if (Approximately(angle, 0, 45))
        {
            dir = Direction.East;
        }
        else if (Approximately(angle, 90, 45))
        {
            dir = Direction.North;
        }
        else if (Approximately(angle, 180, 45))
        {
            dir = Direction.West;
        }
        else
        {
            dir = Direction.South;
        }

        Controller.SetTrigger(dir.ToString());
    }

    private bool Approximately(float a, float b, float acceptance)
    {
        return Mathf.Abs(a - b) < acceptance;
    }
}
