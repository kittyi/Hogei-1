using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet Object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullets in each spray")]
    public int numBulletsPerSpray = 5;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Angle change per spray")]
    public float angleChangePerSpray = 20.0f;
    [Tooltip("Positive or negative (1 or -1)")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;
    [Tooltip("Minimum angle")]
    public float minimumAngle = 4.0f;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private bool canShootBullet = false; //checks whether bullet can be fired

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(angleChangePerShot < minimumAngle)
        {
            angleChangePerShot = minimumAngle;
        }
        if (Time.time > timeLastSprayFired + timeBetweenSprays)
        {
            StartCoroutine(BulletSprayRoutine());
        }
    }

    //coroutine version of bullet spray
    private IEnumerator BulletSprayRoutine()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //for the number of shots in a spray
        for (int i = 0; i < numBulletsPerSpray; i++)
            {
                //create a shot
                //get the current angle as a quaternion
                Quaternion currentRotation = new Quaternion();
                currentRotation.eulerAngles = new Vector3(0.0f, currentAngle, 0.0f);
                //create a bullet clone, and orient it using the current angle
                GameObject bulletClone = Instantiate(bulletObject, transform.position, currentRotation);

                //change the angle between shots
                currentAngle += angleChangePerShot * rotationDirection;

                //wait on self on next bullet delay
                //yield return new WaitForSecondsRealtime(timeBetweenShots);
            }
            //wait for the next spray
            yield return new WaitForSecondsRealtime(timeBetweenSprays);

            //increase angle in prep of next spray
            currentAngle += angleChangePerSpray * rotationDirection;
        }
        
    
}
