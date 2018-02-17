using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : Singleton<ScreenFader>
{
    public float FadeDuration = 0.5f;
    public CanvasGroup LoadingCanvas;

    private delegate float EvaluateRatio(float current, float start, float invTotal);

    private Coroutine coroutine;

    public override void Awake()
    {
        base.Awake();

        if (LoadingCanvas)
        {
            DontDestroyOnLoad(LoadingCanvas.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        SceneController.Instance.OnSceneLoading.AddListener(FadeInLoading);
        SceneController.Instance.OnSceneLoaded.AddListener(FadeOutLoading);
    }

    public void OnDisable()
    {
        SceneController.Instance.OnSceneLoading.RemoveListener(FadeInLoading);
        SceneController.Instance.OnSceneLoaded.RemoveListener(FadeOutLoading);
    }

    public void FadeIn(CanvasGroup group)
    {
        Fade(group, true);
    }

    public void FadeOut(CanvasGroup group)
    {
        Fade(group, false);
    }

    public void FadeInLoading()
    {
        FadeIn(LoadingCanvas);
    }

    public void FadeOutLoading()
    {
        FadeOut(LoadingCanvas);
    }

    private void Fade(CanvasGroup group, bool fadeIn)
    {
        if (group == null)
        {
            return;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(FadeCoroutine(group, fadeIn));
    }

    private float EvaluateFadeIn(float current, float start, float invTotal)
    {
        return (current - start) * invTotal;
    }

    private float EvaluateFadeOut(float current, float start, float invTotal)
    {
        return 1.0f - ((current - start) * invTotal);
    }

    private IEnumerator FadeCoroutine (CanvasGroup group, bool fadeIn)
    {
        float startTime = Time.time;
        float endingTime = Time.time + FadeDuration;
        float inverseDuration = 1.0f / FadeDuration;

        group.gameObject.SetActive(true);

        EvaluateRatio evaluation = EvaluateFadeIn;
        if (!fadeIn)
        {
            evaluation = EvaluateFadeOut;
        }

        while (Time.time < endingTime)
        {
            group.alpha = evaluation(Time.time, startTime, inverseDuration);
            yield return null;
        }

        if (fadeIn == false)
        {
            group.gameObject.SetActive(false);
        }

        coroutine = null;
    }
}
