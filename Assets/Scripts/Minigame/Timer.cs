using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float delay = 10.0f;

    [SerializeField]
    private UnityEvent OnTimerFinished;

    public float currDelay { get; private set; }
    private bool isRunning = true;

    void Start()
    {
        currDelay = delay;
    }
	
	void Update ()
    {
        if (isRunning)
        {
            currDelay -= Time.deltaTime;
            if (currDelay <= 0.0f)
            {
                currDelay = 0.0f;
                if (OnTimerFinished != null)
                    OnTimerFinished.Invoke();
            }
        }
	}

    public void Pause()
    {
        isRunning = false;
    }

    public void Stop()
    {
        isRunning = false;
        currDelay = delay;
    }

    public void Run()
    {
        isRunning = true;
    }
}
