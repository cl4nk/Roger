using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter))]
[RequireComponent(typeof(AudioHighPassFilter))]
public class FrequenceFilter : MonoBehaviour, ICommand
{
    [Range(10.0f, 22000.0f)]
    private float frequence = 5000;

    private const float MAX_FREQUENCE = 22000.0f;
    private const float MIN_FREQUENCE = 10.0f;


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
            frequence = Mathf.Clamp(value, MIN_FREQUENCE, MAX_FREQUENCE);
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

    public float VariationSpeed = 100.0f;

    public void OnEnable()
    {
        Range = range;
    }

    public void OnUpdate(float value)
    {
        Frequence += VariationSpeed * value * Time.deltaTime;
    }
}
