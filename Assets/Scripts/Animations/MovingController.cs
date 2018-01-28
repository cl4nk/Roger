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

    private Vector3 LastPosition;

    [SerializeField]
    private string boolKey;

	public float Distance;

    public void Start()
    {
		LastPosition = RefTransform.position;
    }

    public void Update()
    {
		Distance = Vector3.Distance (LastPosition, RefTransform.position);
		Controller.SetBool(boolKey, Distance != 0.0f);
        LastPosition = RefTransform.position;
    }
}
