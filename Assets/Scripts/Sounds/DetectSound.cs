using UnityEngine;

[RequireComponent(typeof(NoiseDetection))]
[RequireComponent(typeof(RandomAudioSource))]
public class DetectSound : MonoBehaviour
{
    public RandomAudioSource randomPlayer;

    public void OnEnable()
    {
        GetComponent<NoiseDetection>().OnNoiseDetected += Play;
    }

    public void OnDisable()
    {
        GetComponent<NoiseDetection>().OnNoiseDetected -= Play;
    }

    private void Play(Vector3 position)
    {
        randomPlayer.Play();
    }
}
