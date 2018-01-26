using UnityEngine;
using UnityEngine.Events;

public class ArrivalZone : MonoBehaviour
{
    public UnityEvent<ArrivalZone> StaticOnArriveEvent;
    public UnityEvent OnArriveEvent;

    public bool OnlyOnce = false;

    [SerializeField]
    private Transform player;

    public float AcceptanceRadius = 1.0f;

    private bool alreadyTriggered = false;
    public bool AlreadyTriggered
    {
        get { return alreadyTriggered; }
        private set { alreadyTriggered = value; }
    }

    private bool playerIsInZone = false;
    public bool PlayerIsInZone
    {
        get { return playerIsInZone; }
        private set { playerIsInZone = value; }
    }

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
        OnArriveEvent.AddListener(OnArrive);
    }

    public void OnDisable()
    {
        AlreadyTriggered = false;
        OnArriveEvent.AddListener(OnArrive);
    }

    public void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= AcceptanceRadius)
        {
            if (CanBeTriggered)
            {
                if (StaticOnArriveEvent != null)
                    StaticOnArriveEvent.Invoke(this);
                if (OnArriveEvent != null)
                    OnArriveEvent.Invoke();
            }
        }
        else
        {
            PlayerIsInZone = false;
        }
    }

    public void OnArrive()
    {
        AlreadyTriggered = true;
    }
}
