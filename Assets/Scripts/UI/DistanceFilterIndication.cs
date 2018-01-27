using UnityEngine;

public class DistanceFilterIndication : MonoBehaviour
{
    [SerializeField]
    private DistanceFilter distanceFilter;

    [SerializeField]
    private GameObject filledGameObject;

    public void Update()
    {
        float ratio = distanceFilter.CurrentDistance / distanceFilter.MaxDistance;
        filledGameObject.transform.localScale = new Vector3(ratio, ratio, ratio);
    }
}
