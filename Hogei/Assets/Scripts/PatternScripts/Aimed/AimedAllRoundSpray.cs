﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedAllRoundSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Positive or negative (1 or -1)")]

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";
    [Tooltip("Target Tag")]
    public string targetTag = "Player";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngleTotal = 0.0f; //the current angle the bullet is angled at in regards to owner

    //target ref
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        enemyState = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState.GetIsActive())
        {
            if (Time.time > timeLastSprayFired + timeBetweenSprays)
            {
                StartCoroutine(BulletSprayRoutine());
            }
        }
    }

    //bullet firing coroutine
    private IEnumerator BulletSprayRoutine()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //get direction to target
        Vector3 directionToTarget = target.transform.position - transform.position;
        //remove any changes in y
        directionToTarget.y = 0;
        //get quaternion
        Quaternion targetedRotation = Quaternion.LookRotation(directionToTarget);
        //set angle to start going towards target
        float angle = targetedRotation.eulerAngles.y;

        //reset the angle total
        currentAngleTotal = 0.0f;

        //while current angle total not reached 360, keep spawning bullets
        while (currentAngleTotal < 360.0f)
        {
            //create a shot
            //get the current angle as a quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angle, 0.0f);

            /*get a bullet from the bank
            GameObject bullet = bank.GetRegularStraightBullet();*/

            //spawn a bullet
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = transform.position;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = currentRotation;
            //setup the bullet and fire
            bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);

            //change the angle between shots
            angle += angleChangePerShot;
            //add the amount angle changed to current angle total
            currentAngleTotal += angleChangePerShot;
        }

        //wait for next spray
        yield return new WaitForSecondsRealtime(timeBetweenSprays);
    }
}