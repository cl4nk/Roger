using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ArrivalZone : MonoBehaviour
{
    public static event Action<ArrivalZone> StaticOnArriveEvent;
    public UnityEvent OnArriveEvent = new UnityEvent();

    public bool OnlyOnce = false;

    public bool DetectByTag = true;
    public string TagToDetect = "Player";

    private bool alreadyTriggered = false;
    public bool AlreadyTriggered
    {
        get { return alreadyTriggered; }
        private set { alreadyTriggered = value; }
    }

    private List<Collider> CollidersInZone = new List<Collider>();

    public void OnEnable()
    {
        OnArriveEvent.AddListener(OnArrive);
    }

    public void OnDisable()
    {
        OnArriveEvent.RemoveListener(OnArrive);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (OnlyOnce && AlreadyTriggered)
            return;

        if (DetectByTag && other.CompareTag(TagToDetect) || DetectByTag == false)
        {
            CollidersInZone.Add(other);
            OnArriveEvent.Invoke();
            if (StaticOnArriveEvent != null)
                StaticOnArriveEvent(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (CollidersInZone.Contains(other))
        {
            CollidersInZone.Remove(other);
        }
    }

    public void OnArrive()
    {
        AlreadyTriggered = true;
    }

}
