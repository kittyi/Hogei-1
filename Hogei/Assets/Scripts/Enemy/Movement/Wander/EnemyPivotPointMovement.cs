using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPivotPointMovement : MonoBehaviour {

    [Header("Movement range")]
    [Tooltip("The max distance allowed to move along radius from origin")]
    public float wanderRadius = 4.0f;

    [Header("Timing vars")]
    [Tooltip("Time between steps")]
    public float timeBetweenSteps = 3.0f;
    [Tooltip("Time taken when moving")]
    public float timeToMove = 1.0f;


    //control vars
    private float lastMoveTime = 0.0f;
    private Vector3 startPos;
    //private Vector3 destination;

    //nav agent ref
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        lastMoveTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastMoveTime + timeBetweenSteps)
        {
            Vector3 destinaiton = FindMovablePos();
            navAgent.SetDestination(destinaiton);
        }
	}

    //get a random position around the starting pos
    private Vector3 FindMovablePos()
    {
        //set last move time to now
        lastMoveTime = Time.time;

        //get a random position around start pos of target
        Vector3 newDestination = new Vector3(startPos.x + Random.Range(-wanderRadius, wanderRadius), startPos.y, startPos.z + Random.Range(-wanderRadius, wanderRadius));

        //check navmesh for applicable position
        NavMeshHit navHit;
        NavMesh.SamplePosition(newDestination, out navHit, wanderRadius, -1);

        return navHit.position;
    }
}
