using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetupStraightBullet : MonoBehaviour {

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Speed")]
    [Tooltip("Travel speed of bullet")]
    public float travelSpeed = 2.0f;

    //set up vars
    public Vector3 setupDestination = new Vector3(0, 0, 0);
    public float setupDestinationDistance = 0.0f;
    public float setupTime = 0.0f;
    public float startDelay = 0.0f;
    public float angleChange = 0.0f;

    //control vars
    private float startTime = 0.0f;
    public bool isStarting = false;
    private bool isActive = false;

    //script ref
    //private BulletBank bulletBank;

    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
        //SetUp();
    }
	
	// Update is called once per frame
	void Update () {
		if (isStarting)
        {
            SetUp();
        }
	}

    //sets up vars for bullet behaviour
    public void SetupVars(float dist , float setTime, float delay, float angle, float speed)
    {
        setupDestinationDistance = dist;
        setupTime = setTime;
        startDelay = delay;
        angleChange = angle;
        travelSpeed = speed;
        isStarting = true;
    }

    //func called when setup ready
    private void SetUp()
    {
        isStarting = false;
        setupDestination = transform.position + (transform.forward * setupDestinationDistance);
        transform.DOMove(setupDestination, setupTime, false);
        //transform.DOMove(new Vector3(2, 1, 3), 2, false);
        StartCoroutine(BeginMove());
    }

    private IEnumerator BeginMove()
    {
        //waits for setup to finish
        yield return new WaitForSecondsRealtime(startDelay);
        //gets a new rotation
        Quaternion newRotation = new Quaternion();
        //alters rotation based own rotation + given rotation
        newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + angleChange, 0.0f);
        transform.rotation = newRotation;
        //start moving
        myRigid.velocity = transform.forward * travelSpeed;
    }

    ////ref func
    //public void SetBulletBank(BulletBank bank)
    //{
    //    bulletBank = bank;
    //}

    ////deactivate func
    //private void Deactivate()
    //{
    //    //set active to false
    //    isActive = false;
    //    //reset values
    //    setupDestination = new Vector3(0, 0, 0);
    //    setupDestinationDistance = 0.0f;
    //    setupTime = 0.0f;
    //    startDelay = 0.0f;
    //    angleChange = 0.0f;
    //    travelSpeed = 0;
    //    myRigid.velocity = Vector3.zero;
    //    //return to queue
    //    bulletBank.ReturnSetupStraightBullet(gameObject);
    //    transform.position = bulletBank.transform.position;
    //    transform.rotation = Quaternion.identity;
    //}

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
        }
        //Deactivate();
        Destroy(gameObject);
    }
}
