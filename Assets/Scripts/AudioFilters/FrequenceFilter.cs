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

    public float Speed = 100.0f;
    protected Vector2 LastInput = Vector2.zero;

    public void OnEnable()
    {
        Range = range;
    }

    public void EnterInputVector(Vector2 direction)
    {
        if (direction.sqrMagnitude == 0.0f)
        {
            LastInput = Vector2.zero;
        }
        else
        {
            Vector3 newDirection = direction.normalized;
            if (LastInput.sqrMagnitude > 0.0f)
            {
                Frequence += Vector2.SignedAngle(LastInput, newDirection) * Time.deltaTime * -Speed;
                Frequence = Mathf.Clamp(Frequence, MIN_FREQUENCE, MAX_FREQUENCE);
            }
            LastInput = newDirection;
        }
    }
}
