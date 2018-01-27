using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class VisualDetection : MonoBehaviour
{
    public static event Action<VisualDetection, Transform> StaticOnPlayerDetected;

    public UnityEvent<Transform> OnPlayerDetected;

    [Range(0, 360)]
    public float Angle = 15.0f;

    [Range(0, float.MaxValue)]
    public float Distance = 5.0f;

    [SerializeField]
    private Transform target;
    public Transform Target
    {
        get
        {
            if (target == null)
            {
                target = Player.Instance.transform;
            }
            return target;
        }
    }

    public void FixedUpdate()
    {

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Target.position - transform.position, Vector2.Distance(Target.position, transform.position), layerMask);



        if (raycastHit.collider is TilemapCollider2D)
        {
            Debug.Log(raycastHit.collider);
        }
    }

    public void Update()
    {
        float playerDistance = Vector3.Distance(Target.position, transform.position);
        

        if (playerDistance > Distance)
            return;


        Vector3 directionToPlayer = Target.position - transform.position;
        float angle = Vector3.Angle(transform.right, directionToPlayer.normalized);

        if (angle > Angle)
            return;

        if (StaticOnPlayerDetected != null)
            StaticOnPlayerDetected(this, Target);

        if (OnPlayerDetected != null)
            OnPlayerDetected.Invoke(Target);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Angle);
        Vector3 direction = transform.right * Distance;
        Gizmos.DrawRay(transform.position, quaternion * direction);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -Angle);

        Gizmos.DrawRay(transform.position, quaternion * direction);

        Gizmos.DrawRay(transform.position, direction);

    }

    public void Test()
    {
        SceneManager.LoadScene("Test3C");
    }
}
