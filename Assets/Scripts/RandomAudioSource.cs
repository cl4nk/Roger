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

    public void OnEnable()
    {
        Source.clip = clips[Random.Range(0, clips.Length)];
    }
}
