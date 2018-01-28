using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public event Action OnArrive;

    [SerializeField]
    private Character player;
    public Character Player
    {
        get { return player; }
    }

    [SerializeField]
    private ArrivalZone FinishZone;

    [SerializeField]
    private List<ArrivalZone> Checkpoints;

    private Vector3 lastCheckpoint;

    public void OnEnable()
    {
        lastCheckpoint = Player.transform.position;

        Player.OnDeathEvent += RespawnPlayer;
        FinishZone.OnArriveEvent += FinishZone_OnArriveEvent;
        ;

        VisualDetection.StaticOnPlayerDetected += OnPlayerDetected;
        ArrivalZone.StaticOnArriveEvent += SaveCheckpoint;
    }

    private void FinishZone_OnArriveEvent()
    {
        if (OnArrive != null)
            OnArrive();
    }

    private void OnPlayerDetected(VisualDetection finder, Transform found)
    {
        if (player.transform == found)
        {
            RespawnPlayer();
        }
    }

    public void OnDisable()
    {
        ArrivalZone.StaticOnArriveEvent -= SaveCheckpoint;
        VisualDetection.StaticOnPlayerDetected -= OnPlayerDetected;

        Player.OnDeathEvent -= RespawnPlayer;
        FinishZone.OnArriveEvent -= FinishZone_OnArriveEvent;
    }

    private void SaveCheckpoint(ArrivalZone zone)
    {
        if (Checkpoints.Contains(zone))
        {
            lastCheckpoint = zone.transform.position;
        }
    }

    private void RespawnPlayer()
    {
        Player.enabled = false;
        Player.transform.position = lastCheckpoint;
        Player.enabled = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(lastCheckpoint, 0.05f);

        if (!FinishZone)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(FinishZone.transform.position, 0.1f);
    }
}
