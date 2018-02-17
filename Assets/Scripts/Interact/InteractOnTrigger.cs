using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractOnTrigger : MonoBehaviour
{
    public LayerMask layers;
    public UnityEvent OnEnter, OnExit;

    Collider m_Collider;

    void Reset()
    {
        layers = LayerMask.NameToLayer("Everything");
        m_Collider = GetComponent<Collider>();
        m_Collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (layers.Contains(other.gameObject))
        {
            ExecuteOnEnter(other);
        }
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (layers.Contains(other.gameObject))
        {
            ExecuteOnExit(other);
        }
    }

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }
}
