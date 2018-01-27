using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VisualDetection : MonoBehaviour
{
    public static event Action<VisualDetection, Transform> StaticOnPlayerDetected;

    public UnityEvent<Transform> OnPlayerDetected;

    [Range(0, 360)]
    public float Angle = 15.0f;

    [Range(0, float.MaxValue)]
    public float Distance = 5.0f;

    public Transform player;

    public void Update()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);
        if (playerDistance > Distance)
            return;


        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.right, directionToPlayer.normalized);

        if (angle > Angle)
            return;

        if (StaticOnPlayerDetected != null)
            StaticOnPlayerDetected(this, player);
        OnPlayerDetected.Invoke(player);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Angle);
        Gizmos.DrawRay(transform.position, quaternion * transform.right);

        quaternion = Quaternion.Euler(0.0f, 0.0f, -Angle);

        Gizmos.DrawRay(transform.position, quaternion * transform.right);

        Gizmos.DrawRay(transform.position, transform.right);

    }

    public void Test()
    {
        SceneManager.LoadScene("Test3C");
    }
}
