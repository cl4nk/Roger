using UnityEngine;

[RequireComponent(typeof(HideSpot))]
[RequireComponent(typeof(RandomAudioSource))]
public class BrushSound : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<HideSpot>().OnHidden += GetComponent<RandomAudioSource>().Play;
    }

    public void OnDisable()
    {
        GetComponent<HideSpot>().OnHidden -= GetComponent<RandomAudioSource>().Play;
    }
}
