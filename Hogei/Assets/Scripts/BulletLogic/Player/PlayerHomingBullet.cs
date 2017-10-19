using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingBullet : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Tag for targets")]
    public string targetTag = "Enemy";

    [Header("Adjustment force")]
    [Tooltip("Amount of force used to steer homing")]
    public float adjustForce = 0.5f;
    [Tooltip("Time between adjustments")]
    public float homingAdjustInterval = 0.1f;

    //script ref
    private BulletBank bulletBank;

    //control vars
    private Rigidbody myRigid;
    private bool isActive = false;
    private float travelSpeed = 3.0f;
    private float maxHomingTime = 4.0f;
    private float homingStartDelay = 1.0f;
    private float startTime = 0.0f;
    private float lastAdjustTime = 0.0f;
    private GameObject target;
    private GameObject[] targetArray = new GameObject[0];

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            MoveBullet();
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    //set up func
    public void SetupVars(float speed, float hominTime, float hominStartDelay)
    {
        isActive = true;
        travelSpeed = speed;
        maxHomingTime = hominTime;
        homingStartDelay = hominStartDelay;
        startTime = Time.time;
        lastAdjustTime = 0.0f;
    }

    //ref func
    public void SetBulletBank(BulletBank bank)
    {
        bulletBank = bank;
    }

    //find all possible targets active in scene
    private void GetTargets()
    {
        targetArray = GameObject.FindGameObjectsWithTag(targetTag);
    }

    //movement logic
    private void MoveBullet()
    {
        if (/*Time.time < startTime + maxHomingTime &&*/ Time.time > startTime + homingStartDelay)
        {
            
                GetTargets();

                if (targetArray.Length >= 1)
                {
                    SteerToTarget();
                }
                else
                {
                    myRigid.velocity = transform.forward * travelSpeed;
                }

        }
        
        else
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }

    //find a target and steer
    private void SteerToTarget()
    {
        lastAdjustTime = Time.time;

        //set a large compare value
        float closestDist = 5000.0f;

        //for all in target array
        for(int i = 0; i < targetArray.Length; i++)
        {
            //compare the distance, and if closer, make that object the target
            if (closestDist > Vector3.Distance(transform.position, targetArray[i].transform.position))
            {
                target = targetArray[i];
            }
        }

        //steer towards target
        //get vector towards target
        Vector3 desireVelocity = target.transform.position - transform.position;
        float distance = desireVelocity.magnitude;
        desireVelocity = Vector3.Normalize(desireVelocity) * travelSpeed;
        //get steering force
        Vector3 steeringForce =  desireVelocity - myRigid.velocity;
        //steeringForce /= adjustForce;
        //adjust velocity
        myRigid.velocity = Vector3.ClampMagnitude( myRigid.velocity + (steeringForce * Time.deltaTime), travelSpeed);
        transform.rotation = Quaternion.LookRotation(myRigid.velocity);
    }

    //deactivate func
    private void Deactivate()
    {
        //set active to false
        isActive = false;
        //reset values
        myRigid.velocity = Vector3.zero;
        travelSpeed = 0;
        maxHomingTime = 0;
        //return to queue
        bulletBank.ReturnPlayerHomingBullet(gameObject);
        transform.position = bulletBank.transform.position;
    }

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        Deactivate();
    }
}
