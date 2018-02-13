using UnityEngine;

public class DebugLogger : Singleton<DebugLogger>
{
    public Character Character;
    public StickCommands CommandController;

    public void OnEnable()
    {
        NoiseDetection.StaticOnNoiseDetected += NoiseDetection_StaticOnNoiseDetected;
        VisualDetection.StaticOnPlayerDetected += VisualDetectionOnStaticOnPlayerDetected;
        ArrivalZone.StaticOnArriveEvent += ArrivalZone_StaticOnArriveEvent;

        if (Character)
        {
            Character.OnDamageTakenEvent += LogDamageTaken;
            Character.OnDeathEvent.AddListener(LogDeath);

        }
    }

    public void OnDisable()
    {
        NoiseDetection.StaticOnNoiseDetected -= NoiseDetection_StaticOnNoiseDetected;
        VisualDetection.StaticOnPlayerDetected -= VisualDetectionOnStaticOnPlayerDetected;
        ArrivalZone.StaticOnArriveEvent -= ArrivalZone_StaticOnArriveEvent;

        if (Character)
        {
            Character.OnDamageTakenEvent -= LogDamageTaken;
            Character.OnDeathEvent.RemoveListener(LogDeath);

        }
    }

    private void NoiseDetection_StaticOnNoiseDetected(NoiseDetection arg1, UnityEngine.Vector3 arg2)
    {
        Debug.Log("Noise detected : " + arg1 + " " + arg2);
    }

    private void VisualDetectionOnStaticOnPlayerDetected(VisualDetection visualDetection, Transform target)
    {
        Debug.Log("Visual detection : " + visualDetection + " " + target);
    }

    private void ArrivalZone_StaticOnArriveEvent(ArrivalZone obj)
    {
        Debug.Log("Arrival zone : " + obj);
    }

    private void LogDamageTaken(float life)
    {
        Debug.Log("Current life : " + life + Character);
    }

    private void LogDeath()
    {
        Debug.Log(Character + " dead");
    }
}
