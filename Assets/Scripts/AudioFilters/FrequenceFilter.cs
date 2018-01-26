using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter))]
[RequireComponent(typeof(AudioHighPassFilter))]
[RequireComponent(typeof(AudioListener))]
public class FrequenceFilter : MonoBehaviour
{
    [Range(10.0f, 22000.0f)]
    private float frequence = 5000;

    [Range(10.0f, 22000.0f)]
    private float range = 1000;
    public float Range
    {
        get { return range; }
        set
        {
            range = value;
            Frequence = frequence;
        }
    }

    public float Frequence
    {
        get { return frequence; }
        set
        {
            frequence = value;
            HighFilter.cutoffFrequency = Mathf.Max(10.0f, frequence - Range);
            LowFilter.cutoffFrequency = Mathf.Min(22000.0f, frequence + Range);
        }
    }

    private AudioHighPassFilter highFilter;
    private AudioHighPassFilter HighFilter
    {
        get
        {
            if (highFilter == null)
            {
                highFilter = GetComponent<AudioHighPassFilter>();
            }
            return highFilter;
        }
    }

    private AudioLowPassFilter lowFilter;
    private AudioLowPassFilter LowFilter
    {
        get
        {
            if (lowFilter == null)
            {
                lowFilter = GetComponent<AudioLowPassFilter>();
            }
            return lowFilter;
        }
    }

    public void OnEnable()
    {
        Range = range;
    }
}
