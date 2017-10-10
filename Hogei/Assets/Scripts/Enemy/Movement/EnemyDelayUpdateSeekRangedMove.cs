using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyDelayUpdateSeekRangedMove : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time between adjustments")]
    public float timeBetweenAdjustment = 2.0f;
    [Tooltip("Rotate duration")]
    public float rotateDuration = 1.0f;

    [Header("Range vars")]
    [Tooltip("Distance kept from target")]
    public float distanceKeptFromTarget = 5.0f;
    [Tooltip("Distance to try backstepping")]
    public float backstepDistance = 0.5f;

    [Header("Speed vars")]
    [Tooltip("Speed when moving towards target")]
    public float forwardSpeed = 3.5f;
    [Tooltip("Speed when moving away from target")]
    public float backwardSpeed = 0.5f;

    [Header("Angle control vars")]
    [Tooltip("The maximum limit that the ai will attempt to search for an unblocked path around self")]
    public float angleSearchLimit = 180.0f;
    [Tooltip("The incremental value for searching")]
    public float angleSearchIncrementation = 20.0f;

    [Header("Tags")]
    [Tooltip("Target tag")]
    public string targetTag = "Player";

    //control vars
    private float lastMoveTime = 0.0f;
    private float distanceToTarget = 0.0f;

    //target ref
    private GameObject target;

    //nav agent ref
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        //don't allow nav agent to turn
        navAgent.angularSpeed = 0;
        lastMoveTime = Time.time;
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    // Update is called once per frame
    void Update()
    {
        CalcDistanceToPlayer();
        
        if (Time.time > lastMoveTime + timeBetweenAdjustment)
        {
            FaceTarget();
            SeekPlayer();
        }
    }

    //calc distance to player
    private void CalcDistanceToPlayer()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    }

    //seek after the player, called between delays
    private void SeekPlayer()
    {
        //set last adjust time to now
        lastMoveTime = Time.time;

        //if target is still far away
        if (distanceKeptFromTarget < distanceToTarget)
        {
            //while moving towards target move at set speed
            navAgent.speed = forwardSpeed;
            //set destination to target's current pos
            navAgent.SetDestination(target.transform.position);
        }
        //else try to move back
        else
        {
            //setup for checking free area
            NavMeshHit navHit;
            bool blocked = false;
            float angleAlteration = 0.0f;
            //get a position behind self
            Vector3 targetPos = -transform.forward * backstepDistance;
            //attempt to raycast backwards
            blocked = NavMesh.Raycast(transform.position, targetPos, out navHit, NavMesh.AllAreas);

            //in case of being blocked, keep trying till either find spot not blocked or angle at limit
            //increase angle in prep
            angleAlteration += angleSearchIncrementation;
            while (blocked && angleAlteration > angleSearchLimit)
            {
                //get slight alteration in direction
                Vector3 alteredTargetPos = Quaternion.AngleAxis(angleAlteration, -transform.forward) * targetPos;
                //attempt to raycast backwards
                blocked = NavMesh.Raycast(transform.position, alteredTargetPos, out navHit, NavMesh.AllAreas);
                //if still blocked, try alteration other way
                if (blocked)
                {
                    alteredTargetPos = Quaternion.AngleAxis(-angleAlteration, -transform.forward) * targetPos;
                    //attempt to raycast backwards
                    blocked = NavMesh.Raycast(transform.position, alteredTargetPos, out navHit, NavMesh.AllAreas);
                }
                //increase angle in prep for next search if needed
                angleAlteration += angleSearchIncrementation;
            }
            print(blocked);
            //set back away speed for agent
            navAgent.speed = backwardSpeed;
            //take the last altered navhit position and set as destination
            navAgent.SetDestination(navHit.position);
        }
    }

    //turn to face the target
    private void FaceTarget()
    {
        //get direction to target
        Vector3 directionToTarget = target.transform.position - transform.position;
        //remove any changes in y
        directionToTarget.y = 0;
        //get quaternion
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        //look at target
        transform.DORotate(rotationToTarget.eulerAngles, rotateDuration, RotateMode.Fast);
    }
}
