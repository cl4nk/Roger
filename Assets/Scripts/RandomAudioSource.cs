using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioSource : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource source;
    public AudioSource Source
    {
        get
        {
            if (source == null)
            {
                source = GetComponent<AudioSource>();
            }
            return source;
        }
    }

    [SerializeField]
    private bool continous = false;

    public bool Continous
    {
        get { return continous; }
        set
        {
            continous = value;
            if (continous)
            {
                if (coroutine != null)
                    coroutine = StartCoroutine(ContinousCoroutine());
            }
            else if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    private Coroutine coroutine;

    public void OnEnable()
    {
        Source.clip = clips[Random.Range(0, clips.Length)];

        if (Continous)
        {
            coroutine = StartCoroutine(ContinousCoroutine());
        }
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator ContinousCoroutine()
    {
        while (true)
        {
            if (!Source.isPlaying)
            {
                Source.clip = clips[Random.Range(0, clips.Length)];
            }
            yield return null;
        }
    }
}
