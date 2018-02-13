using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    public float Length;
    [SerializeField]
    private float currentDuration;
    public float CurrentDuration
    {
        get
        {
            return currentDuration;
        }
        private set
        {
            currentDuration = value;
            if (currentDuration < 0)
            {
                OnStart.Invoke();
            }
        }
    }

    public UnityEvent OnStart = new UnityEvent();

    public bool IsPending { get; private set; }

    public void Awake()
    {
        CurrentDuration = 0;
    }

    // Use this for initialization
    void StartCountdown ()
    {
        CurrentDuration = Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (CurrentDuration > 0.0f)
        {
            CurrentDuration -= Time.deltaTime;
        }
	}
}
