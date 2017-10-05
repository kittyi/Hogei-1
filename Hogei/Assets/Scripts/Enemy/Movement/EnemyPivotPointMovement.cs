using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPivotPointMovement : MonoBehaviour {

    //control vars
    private Transform startPos;

    //nav agent ref
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        startPos = transform;
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //get a random position around the starting pos

}
