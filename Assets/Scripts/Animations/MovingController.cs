using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovingController : MonoBehaviour
{
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

    public float AcceptanceRadius = 0.001f;

    private Vector3 LastPosition;

    [SerializeField]
    private string boolKey;

    public void Start()
    {
        LastPosition = transform.position;
    }

    public void Update()
    {
        Controller.SetBool(boolKey, Vector3.Distance(LastPosition, RefTransform.position) > AcceptanceRadius);
        LastPosition = RefTransform.position;
    }
}
