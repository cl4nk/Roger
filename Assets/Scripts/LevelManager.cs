using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityAction OnArrive;

    [SerializeField]
    private ArrivalZone arrivalZone;

    public void OnEnable()
    {
        arrivalZone.OnArriveEvent += OnArrive.Invoke;
    }

    public void OnDisable()
    {
        arrivalZone.OnArriveEvent -= OnArrive.Invoke;
    }
}
