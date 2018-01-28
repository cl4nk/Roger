using System.Collections;
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

    private Coroutine couroutine;

    public void OnEnable()
    {
        LastPosition = transform.position;
        couroutine = StartCoroutine(CalcVelocity());
    }

    public void OnDisable()
    {
        if (couroutine != null)
        {
            StopCoroutine(couroutine);
        }
    }

    IEnumerator CalcVelocity()
    {
        while (true)
        {
            // Position at frame start
            LastPosition = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            Controller.SetBool(boolKey, ((LastPosition - transform.position) / Time.deltaTime).magnitude > AcceptanceRadius);
        }
    }
}
