using UnityEngine;

[RequireComponent(typeof(HideSpot))]
[RequireComponent(typeof(RandomAudioSource))]
public class BrushSound : MonoBehaviour
{
    public RandomAudioSource randomPlayer;

    public void OnEnable()
    {
        GetComponent<HideSpot>().OnHidden += randomPlayer.Play;
    }

    public void OnDisable()
    {
        GetComponent<HideSpot>().OnHidden -= randomPlayer.Play;
    }
}
