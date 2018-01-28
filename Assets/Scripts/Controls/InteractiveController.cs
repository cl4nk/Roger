using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour
{
    public event Action<InteractiveObject> OnInteractiveObjectChange;

    private InteractiveObject nearObject;
    public InteractiveObject NearObject
    {
        get { return nearObject; }
        set
        {
            nearObject = value;
            if (OnInteractiveObjectChange != null)
            {
                OnInteractiveObjectChange.Invoke(value);
            }
        }
    }

    public string Key = "Fire1";

    public void Update()
    {
        if (Input.GetKeyUp(Key) && NearObject)
        {
            NearObject.Interact(this);
        }
        else
        {
            List<InteractiveObject> interactiveObjects = new List<InteractiveObject>(FindObjectsOfType<InteractiveObject>());
            interactiveObjects.Sort(Comparaison);

            foreach (InteractiveObject interactiveObject in interactiveObjects)
            {
                if (Vector3.Distance(transform.position, interactiveObject.transform.position) < interactiveObject.Range)
                {
                    NearObject = interactiveObject;
                    return;
                }
            }
        }
    }

    private int Comparaison(InteractiveObject first, InteractiveObject second)
    {
        return Vector3.Distance(transform.position, first.transform.position)
            .CompareTo(Vector3.Distance(transform.position, second.transform.position));
    }
}
