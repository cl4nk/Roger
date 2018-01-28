using UnityEngine;

public class PathFollowingAI : MonoBehaviour, IMoving
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

    [SerializeField]
    private Transform[] pathPoints;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float reachMargin = 0.5f;

    private int currentDestination = 0;
    private int increment = 1;

    private Quaternion? TempRotation;

    public float FixedDirectionDuration = 2.0f;

    private NoiseDetection noiseDetection;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    private float fixedDirectionTime = 0.0f;

    private bool isMoving;

    public void OnEnable()
    {
        noiseDetection = GetComponent<NoiseDetection>();
        if (noiseDetection)
        {
            noiseDetection.OnNoiseDetected += SetTempDestination;
        }
    }

    public void OnDisable()
    {
        if (noiseDetection)
        {
            noiseDetection.OnNoiseDetected += SetTempDestination;
        }
    }

	// Update is called once per frame
	private void Update ()
	{
	    isMoving = false;

        if (TempRotation.HasValue)
        {
            if (fixedDirectionTime > Time.time)
            {
                RefTransform.localRotation = Quaternion.Slerp(RefTransform.localRotation, TempRotation.Value, rotationSpeed * Time.deltaTime);
                return;
            }
            else
            {
                TempRotation = null;
            }
        }

        Vector3? target = GetTarget();
        if (target == null)
            return;

	    isMoving = true;

        Vector3 direction = target.Value - RefTransform.position;
        direction.Normalize();

        transform.position += (direction * Time.deltaTime * speed);

        //direction = new Vector3(direction.y, -direction.x);

        float angle = Vector3.Angle(Vector3.right, direction);
        RefTransform.localRotation = Quaternion.Slerp(RefTransform.localRotation, Quaternion.Euler(0, 0, angle * Mathf.Sign(direction.y)), rotationSpeed * Time.deltaTime);

        if (HasReachDestination())
        {
            Increment();
        }
	}

    private Vector3? GetTarget()
    {
        if (pathPoints == null)
            return null;
        if (pathPoints.Length == 0)
            return null;

        return pathPoints[currentDestination].position;
    }

    private void Increment()
    {
        currentDestination += increment;

        if (currentDestination == 0 || currentDestination == pathPoints.Length - 1)
        {
            increment = -increment;
        }
    }

    private bool HasReachDestination()
    {
        Vector3? target = GetTarget();
        if (target == null)
            return false;
        return Vector3.Distance(transform.position, target.Value) <= reachMargin;
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

    public void SetTempDestination(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        float angle = Vector3.Angle(Vector3.right, direction);
        TempRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(direction.y));

        fixedDirectionTime = FixedDirectionDuration + Time.time;
    }

    public void OnDrawGizmos()
    {
        Vector3? target = GetTarget();
        if (target == null)
            return;
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(target.Value, reachMargin);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
