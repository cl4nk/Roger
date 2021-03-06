﻿using System;

using UnityEngine;
using UnityEngine.Events;

public class ArrivalZone : MonoBehaviour
{
    public static event Action<ArrivalZone> StaticOnArriveEvent;
    public UnityEvent OnArriveEvent = new UnityEvent();

    public bool OnlyOnce = false;

    [SerializeField]
    private Transform target;

    public Transform Target
    {
        get
        {
            if (target == null)
            {
                target = Player.Instance.transform;
            }
            return target;
        }
    }

    public float AcceptanceRadius = 1.0f;

    private bool alreadyTriggered = false;
    public bool AlreadyTriggered
    {
        get { return alreadyTriggered; }
        private set { alreadyTriggered = value; }
    }

    private bool playerIsInZone = false;
    public bool PlayerIsInZone
    {
        get { return playerIsInZone; }
        private set { playerIsInZone = value; }
    }

    public bool CanBeTriggered
    {
        get
        {
            if (PlayerIsInZone)
                return false;
            return (OnlyOnce && !AlreadyTriggered) || !OnlyOnce;
        }
    }

    public void OnEnable()
    {
        AlreadyTriggered = true;
        PlayerIsInZone = false;
        OnArriveEvent.AddListener(OnArrive);
    }

    public void OnDisable()
    {
        AlreadyTriggered = false;
        OnArriveEvent.RemoveListener(OnArrive);
    }

    public void Update()
    {
        if (Vector3.Distance(Target.position, transform.position) <= AcceptanceRadius)
        {
            if (CanBeTriggered)
            {
                if (StaticOnArriveEvent != null)
                    StaticOnArriveEvent.Invoke(this);
                if (OnArriveEvent != null)
                    OnArriveEvent.Invoke();
            }
        }
        else
        {
            PlayerIsInZone = false;
        }
    }

    public void OnArrive()
    {
        AlreadyTriggered = true;
        PlayerIsInZone = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, AcceptanceRadius);
    }
}
