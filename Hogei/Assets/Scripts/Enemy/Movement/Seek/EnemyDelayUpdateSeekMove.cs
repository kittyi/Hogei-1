using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDelayUpdateSeekMove : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time between adjustments")]
    public float timeBetweenAdjustment = 2.0f;

    [Header("Tags")]
    [Tooltip("Target tag")]
    public string targetTag = "Player";

    //control vars
    private float lastMoveTime = 0.0f;

    //nav agent ref
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        lastMoveTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastMoveTime + timeBetweenAdjustment)
        {
            SeekPlayer();
        }
	}

    //seek after the player, called between delays
    private void SeekPlayer()
    {
        //set last adjust time to now
        lastMoveTime = Time.time;

        //set destination to target's current pos
        navAgent.SetDestination(GameObject.FindGameObjectWithTag(targetTag).transform.position);
    }
}
