using System;
using UnityEngine;

public class ArrivalZone : MonoBehaviour
{
    public static event Action<ArrivalZone> OnArriveEvent;

    public bool OnlyOnce = false;

    [SerializeField]
    private Transform player;

    public float AcceptanceRadius = 1.0f;

    private bool AlreadyTriggered = false;

    private bool PlayerIsInZone = false;

    public bool CanBeTriggered
    {
        get
        {
            if (PlayerIsInZone)
                return false;
            return (OnlyOnce && !AlreadyTriggered) || !OnlyOnce;
        }
    }

    public void OnEnable()
    {
        AlreadyTriggered = true;
        OnArriveEvent += OnArrive;
    }

    public void OnDisable()
    {
        AlreadyTriggered = false;
        OnArriveEvent -= OnArrive;
    }

    public void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= AcceptanceRadius)
        {
            if (CanBeTriggered && OnArriveEvent != null)
                OnArriveEvent.Invoke(this);
        }
        else
        {
            PlayerIsInZone = false;
        }
    }

    public void OnArrive(ArrivalZone zone)
    {
        if (zone == this)
            AlreadyTriggered = true;
    }

}
