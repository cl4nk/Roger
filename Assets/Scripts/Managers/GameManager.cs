using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        Play,
        Pause,
        Count
    }

    public enum Step
    {
        Intro, Play, GameOver, Count
    }

    public UnityEvent<State> OnStateChanged;
    public UnityEvent<Step> OnStepChanged;


    private State currentState;
    private State CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            if (OnStateChanged != null)
            {
                OnStateChanged.Invoke(value);
            }
        }
    }

    private Step currentStep;
    private Step CurrentStep
    {
        get { return currentStep; }
        set
        {
            currentStep = value;
            if (OnStepChanged != null)
            {
                OnStepChanged.Invoke(value);
            }
        }
    }

    public void OnEnable()
    {
        OnStateChanged.AddListener(HandleStateChange);
    }

    public void OnDisable()
    {
        OnStateChanged.RemoveListener(HandleStateChange);

    }

    private void HandleStateChange(State state)
    {
        switch (state)
        {
            case State.Play:
                Time.timeScale = 1.0f;
                break;
            case State.Pause:
                Time.timeScale = 0.0f;
                break;
            case State.Count:
                break;
            default:
                throw new ArgumentOutOfRangeException("state", state, null);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        CurrentState = pauseStatus ? State.Pause : State.Play;
    }
}
