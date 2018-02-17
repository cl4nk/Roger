using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class NoiseDetection : MonoBehaviour
{
    private static readonly List<NoiseDetection> noiseDetectors = new List<NoiseDetection>();
    public static List<NoiseDetection> NoiseDetectors
    {
        get { return noiseDetectors; }
    }

    public static event Action<NoiseDetection, Vector3> StaticOnNoiseDetected;

    public event Action<Vector3> OnNoiseDetected;

    [Range(0, float.MaxValue)]
    public float Distance = 5.0f;

    public void OnEnable()
    {
        NoiseDetectors.Add(this);
    }

    public void OnDisable()
    {
        NoiseDetectors.Remove(this);
    }

    public static void EmitNoise(Vector3 position, float range)
    {
        foreach (NoiseDetection detector in NoiseDetectors)
        {
            float noiseDistance = Vector3.Distance(position, detector.transform.position);
            if (noiseDistance < range + detector.Distance)
            {
                if (detector.OnNoiseDetected != null)
                    detector.OnNoiseDetected.Invoke(position);
                if (StaticOnNoiseDetected != null)
                    StaticOnNoiseDetected.Invoke(detector, position);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
