using UnityEngine;

[RequireComponent(typeof(HideSpot))]
[RequireComponent(typeof(RandomAudioSource))]
public class BrushSound : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<HideSpot>().OnHidden.AddListener(GetComponent<RandomAudioSource>().Play);
    }

    public void OnDisable()
    {
        GetComponent<HideSpot>().OnHidden.RemoveListener(GetComponent<RandomAudioSource>().Play);
    }
}
