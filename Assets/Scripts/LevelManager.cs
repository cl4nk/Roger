using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent OnArrive;

    [SerializeField]
    private ArrivalZone arrivalZone;

    public void OnEnable()
    {
        arrivalZone.OnArriveEvent.AddListener(OnArrive.Invoke);
    }

    public void OnDisable()
    {
        arrivalZone.OnArriveEvent.RemoveListener(OnArrive.Invoke);
    }

    public void OnDrawGizmos()
    {
        if (!arrivalZone)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(arrivalZone.transform.position, 0.1f);
    }
}
