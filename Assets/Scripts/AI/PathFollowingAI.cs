using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class PathFollowingAI : MonoBehaviour {

    private Transform transf;

    [SerializeField]
    private Transform[] pathPoints;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float reachMargin = 0.5f;

    private int currentDestination = 0;
    private int increment = 1;

    //private NavMeshAgent agent;




	// Use this for initialization
	void Start () {
        /*agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(pathPoints[currentDestination].position);*/
        transf = transform;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            currentDestination += increment;
            agent.SetDestination(pathPoints[currentDestination].position);

            if (currentDestination == 0 || currentDestination == pathPoints.Length)
            {
                increment = -increment;
            }
        }*/

        Vector3 direction = pathPoints[currentDestination].position - transf.position;
        direction = new Vector3(direction.x, 0.0f, direction.z);
        direction.Normalize();

        transf.Translate(direction * Time.deltaTime * speed);

        if (ReachDestination())
        {
            currentDestination += increment;

            if (currentDestination == 0 || currentDestination == pathPoints.Length - 1)
            {
                increment = -increment;
            }
        }
	}

    bool ReachDestination()
    {
        Vector3 groundedPoint = new Vector3(pathPoints[currentDestination].position.x, transf.position.y, pathPoints[currentDestination].position.z);
        return Vector3.Distance(transf.position, groundedPoint) <= reachMargin;
    }
}
