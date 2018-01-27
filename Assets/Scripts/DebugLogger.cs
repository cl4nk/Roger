using UnityEngine;

public class DebugLogger : Singleton<DebugLogger>
{
    public Character Character;
    public CommandController CommandController;

    public void OnEnable()
    {
        NoiseDetection.StaticOnNoiseDetected += this.NoiseDetection_StaticOnNoiseDetected;
        VisualDetection.StaticOnPlayerDetected += VisualDetectionOnStaticOnPlayerDetected;
        ArrivalZone.StaticOnArriveEvent += this.ArrivalZone_StaticOnArriveEvent;

        if (Character)
        {
            Character.OnDamageTakenEvent.AddListener(LogDamageTaken);
            Character.OnDeathEvent.AddListener(LogDeath);

        }

        if (CommandController)
        {
            CommandController.OnCommandEvent.AddListener(LogCommandChanged);
        }
    }

    public void OnDisable()
    {
        NoiseDetection.StaticOnNoiseDetected -= this.NoiseDetection_StaticOnNoiseDetected;
        VisualDetection.StaticOnPlayerDetected -= VisualDetectionOnStaticOnPlayerDetected;
        ArrivalZone.StaticOnArriveEvent -= this.ArrivalZone_StaticOnArriveEvent;

        if (Character)
        {
            Character.OnDamageTakenEvent.RemoveListener(LogDamageTaken);
            Character.OnDeathEvent.RemoveListener(LogDeath);

        }

        if (CommandController)
        {
            CommandController.OnCommandEvent.RemoveListener(LogCommandChanged);
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

    private void LogCommandChanged(int index)
    {
        Debug.Log("Command changed : " + index + CommandController);
    }
}
