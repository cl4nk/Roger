﻿using UnityEngine;

public class DistanceFilter : MonoBehaviour, ICommand
{
    [SerializeField]
    private DistanceFilterIndication indication;
    public float CurrentDistance = 5.0f;

    public float MaxDistance = 15.0f;
    public float MinDistance = 0.5f;

    public float Speed = 2.0f;

    protected Vector2 LastInput = Vector2.zero;

    public void Awake()
    {
        CurrentDistance = MinDistance;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, CurrentDistance);
    }

    public void OnCommandEnable()
    {
        if (indication)
        {
            indication.gameObject.SetActive(true);
        }
    }

    public void OnCommandDisable()
    {
        if (indication)
        {
            indication.gameObject.SetActive(false);
        }
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
                CurrentDistance += Vector2.SignedAngle(LastInput, newDirection) * Time.deltaTime * Speed;
                CurrentDistance = Mathf.Clamp(CurrentDistance, MinDistance, MaxDistance);
            }
            LastInput = newDirection;
        }
    }
}
