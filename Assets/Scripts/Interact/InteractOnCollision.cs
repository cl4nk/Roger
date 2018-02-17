﻿using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractOnCollision : MonoBehaviour
{
    public LayerMask layers;
    public UnityEvent OnCollision;

    void Reset()
    {
        layers = LayerMask.NameToLayer("Everything");
    }

    void OnCollisionEnter(Collision c)
    {
        if (0 != (layers.value & 1 << c.transform.gameObject.layer))
        {
            OnCollision.Invoke();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }
}
