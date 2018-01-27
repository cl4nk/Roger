using UnityEngine;

public class WalkNoise : MonoBehaviour
{
    public float DistanceBetween = 2.0f;

    public float NoiseRange = 1.0f;

    private Vector3 LastPosition;

	// Use this for initialization
	void Start ()
	{
	    LastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float distance = Vector3.Distance(LastPosition, transform.position);
	    if (distance > DistanceBetween)
	    {
	        NoiseDetection.EmitNoise(transform.position, NoiseRange);
	        LastPosition = transform.position;
	    }
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(LastPosition, 0.05f);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, NoiseRange);
    }
}
