using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightBullet : MonoBehaviour {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    //script ref
    private BulletBank bulletBank;

    //control vars
    private Rigidbody myRigid;
    private bool isActive = false;
    private bool doExpire = false;
    private float maxTravelDistance = 0;
    private Vector3 startPos = Vector3.zero;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;

            CheckExpire();
            
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    //set up func
    public void SetupVars(float speed, float travelDist, bool expire)
    {
        isActive = true;
        maxTravelDistance = travelDist;
        doExpire = expire;
        travelSpeed = speed;
        startPos = transform.position;
    }

    //ref func
    public void SetBulletBank(BulletBank bank)
    {
        bulletBank = bank;
    }

    public void CheckExpire()
    {
        if (doExpire)
        {
            if (Vector3.Distance(transform.position, startPos) > maxTravelDistance)
            {
                Deactivate();
            }
        }
    }

    //deactivate func
    private void Deactivate()
    {
        //set active to false
        isActive = false;
        //reset values
        myRigid.velocity = Vector3.zero;
        travelSpeed = 0;
        maxTravelDistance = 0;
        doExpire = false;
        startPos = Vector3.zero;
        //return to queue
        bulletBank.ReturnRegularStraightBullet(gameObject);
        transform.position = bulletBank.transform.position;
    }

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        Deactivate();
    }
}
