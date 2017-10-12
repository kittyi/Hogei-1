using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemySetupMove : MonoBehaviour {

    [Header("Movement control vars")]
    [Tooltip("Waypoints to pass")]
    public Transform[] wayPoints = new Transform[0];
    [Tooltip("Time taken to move between points")]
    public float timeToMoveBetweenPoints = 1.0f;

    //nav agent ref
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine( BeginAction());
	}

    //called when set to active
    private IEnumerator BeginAction()
    {
        //for all waypoints
        for (int i = 0; i < wayPoints.Length; i++)
        {
            //set nav mesh agent to next destination
            navAgent.SetDestination(wayPoints[i].position);
            //wait between moves
            yield return new WaitForSecondsRealtime(timeToMoveBetweenPoints);
        }
    }
}
