using System;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>
{
    public GameObject PauseStatePrefab;
    private GameObject pauseStateObject;

    public GameObject IntroStepPrefab;
    private GameObject introStepObject;

    public GameObject HudStepPrefab;
    private GameObject hudStepObject;

    public GameObject GameOverStepPrefab;
    private GameObject gameOverStepObject;

    public void OnEnable()
    {
        GameManager.Instance.OnStateChanged += HandleStateChange;
        GameManager.Instance.OnStepChanged += HandleStepChange;
    }

    public void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= HandleStateChange;
        GameManager.Instance.OnStepChanged -= HandleStepChange;
    }

    private void HandleStateChange(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.Play:
                HandlePlayState();
                break;
            case GameManager.State.Pause:
                HandlePauseState();
                break;
            case GameManager.State.Count:
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
                HandleIntroStep();
                break;
            case GameManager.Step.Play:
                HandlePlayStep();
                break;
            case GameManager.Step.GameOver:
                HandleGameOverStep();
                break;
            default:
                throw new ArgumentOutOfRangeException("step", step, null);
        }
    }

    private void HandlePlayState()
    {
        if (pauseStateObject)
        {
            pauseStateObject.SetActive(false);
        }
    }

    private void HandlePauseState()
    {
        if (pauseStateObject == null)
        {
            pauseStateObject = Instantiate(PauseStatePrefab);
        }

        pauseStateObject.SetActive(true);
    }

    private void HandleIntroStep()
    {
        if (introStepObject == null)
        {
            introStepObject = Instantiate(IntroStepPrefab);
        }

        introStepObject.SetActive(true);

        if (gameOverStepObject)
        {
            gameOverStepObject.SetActive(false);
        }

        if (hudStepObject)
        {
            hudStepObject.SetActive(false);
        }
    }

    private void HandlePlayStep()
    {
        if (hudStepObject == null)
        {
            hudStepObject = Instantiate(HudStepPrefab);
        }

        hudStepObject.SetActive(true);

        if (gameOverStepObject)
        {
            gameOverStepObject.SetActive(false);
        }

        if (introStepObject)
        {
            introStepObject.SetActive(false);
        }
    }

    private void HandleGameOverStep()
    {
        if (gameOverStepObject == null)
        {
            gameOverStepObject = Instantiate(GameOverStepPrefab);
        }

        gameOverStepObject.SetActive(true);

        if (hudStepObject)
        {
            hudStepObject.SetActive(false);
        }

        if (introStepObject)
        {
            introStepObject.SetActive(false);
        }
    }
}
