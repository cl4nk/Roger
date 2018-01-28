using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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

    // Update is called once per frame
    void Update ()
    {
        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, filter.Angle);
        Vector3 first = transform.position + quaternion * transform.right;
        first.z = -Depth;
        renderer.SetPosition(0, first);

        Vector3 second = transform.position;
        second.z = -Depth;
        renderer.SetPosition(1, second);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -filter.Angle);
        Vector3 last = transform.position + quaternion * transform.right;
        last.z = -Depth;
        renderer.SetPosition(2, last);
    }
}
