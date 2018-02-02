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

    public event Action<State> OnStateChanged;
    public event Action<Step> OnStepChanged;


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

    public void Start()
    {
        OnStateChanged += HandleStateChange;
        OnStepChanged += HandleStepChange;
        Player.Instance.GetComponent<Character>().OnDeathEvent.AddListener(GameOver);
        CurrentStep = Step.Intro;
    }

    public void OnDisable()
    {
        OnStepChanged -= HandleStepChange;
        OnStateChanged -= HandleStateChange;
        Player.Instance.GetComponent<Character>().OnDeathEvent.RemoveListener(GameOver);
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

    private void HandleStepChange(GameManager.Step step)
    {
        switch (step)
        {
            case GameManager.Step.Intro:
                Invoke("GoToPlayStep", 1.0f);
                break;
            case GameManager.Step.Play:
                break;
            case GameManager.Step.GameOver:
                Time.timeScale = 0.0f;
                break;
            default:
                throw new ArgumentOutOfRangeException("step", step, null);
        }
    }

    private void GoToPlayStep()
    {
        CurrentStep = Step.Play;
    }

    private void GameOver()
    {
        CurrentStep = Step.GameOver;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        CurrentState = pauseStatus ? State.Pause : State.Play;
    }
}
