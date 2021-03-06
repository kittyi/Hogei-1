﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularStraightBullet : MonoBehaviour {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Particle effect")]
    [Tooltip("Particle emitted by bullet on impact")]
    public GameObject particleObject;

    //script ref
    //private BulletBank bulletBank;

    private Rigidbody myRigid;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
	}

    //set up func
    public void SetupVars(float speed)
    {
        isActive = true;
        travelSpeed = speed;
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
    //    myRigid.velocity = Vector3.zero;
    //    travelSpeed = 0;
    //    //return to queue
    //    bulletBank.ReturnRegularStraightBullet(gameObject);
    //    transform.position = bulletBank.transform.position;
    //}

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        }
        //Deactivate();
        Destroy(gameObject);
    }
}
