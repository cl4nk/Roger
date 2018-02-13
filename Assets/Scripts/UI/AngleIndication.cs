using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DirectionnalFilter))]
public class AngleIndication : MonoBehaviour
{
    private LineRenderer renderer;

    private DirectionnalFilter filter;
    public float Depth = 1;

    public void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        filter = GetComponent<DirectionnalFilter>();

        renderer.positionCount = 3;
    }

	public void OnEnable()
	{
		renderer.enabled = true;
	}

	public void OnDisable()
	{
		renderer.enabled = false;
	}

    // Update is called once per frame
    void Update ()
    {
        Vector3 direction = filter.Direction;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, filter.Angle);
        Vector3 first = transform.position + quaternion * direction;
        first.z = -Depth;
        renderer.SetPosition(0, first);

        Vector3 second = transform.position;
        second.z = -Depth;
        renderer.SetPosition(1, second);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -filter.Angle);
        Vector3 last = transform.position + quaternion * direction;
        last.z = -Depth;
        renderer.SetPosition(2, last);
    }
}
