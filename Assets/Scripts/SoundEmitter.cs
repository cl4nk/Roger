using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private static List<SoundEmitter> emitters = new List<SoundEmitter>();
    public static List<SoundEmitter> Emitters { get { return emitters; } }

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
    private DistanceFilter refDistanceFilter;
    public DistanceFilter RefDistanceFilter
    {
        get
        {
            if (refDistanceFilter == null)
            {
                refDistanceFilter = Player.Instance.GetComponentInChildren<DistanceFilter>();
            }
            return refDistanceFilter;
        }
    }

    [SerializeField]
    private DirectionnalFilter refDirectionnalFilter;
    public DirectionnalFilter RefDirectionnalFilter
    {
        get
        {
            if (refDirectionnalFilter == null)
            {
                refDirectionnalFilter = Player.Instance.GetComponentInChildren<DirectionnalFilter>();
            }
            return refDirectionnalFilter;
        }
    }

    public void Awake()
    {
        Emitters.Add(this);
        Source.spatialBlend = 0.0f;
    }

    public void OnDestroy()
    {
        Emitters.Remove(this);
    }

    public void OnEnable()
    {
        Source.maxDistance = float.MaxValue;
    }

    public void Update()
    {
        if (RefDistanceFilter)
        {
            Source.volume = RefDistanceFilter.GetVolume(transform.position);
        }

        if (RefDirectionnalFilter)
        {
            Vector3 direction = transform.position - RefDirectionnalFilter.transform.position;

            float angle = Vector3.Angle(RefDirectionnalFilter.transform.right,
                direction);

            if (angle > RefDirectionnalFilter.Angle)
            {
                Source.volume = 0.0f;
            }
            else
            {
                angle *= Mathf.Sign(Vector3.Dot(RefDirectionnalFilter.transform.right, direction)) * -1;
                Source.panStereo = angle / RefDirectionnalFilter.Angle;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.green, Color.red, Source.volume);
        Gizmos.DrawWireSphere(transform.position, Source.maxDistance);
    }
}
