using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableCircularSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Num of sprays")]
    public int numSprays = 3;
    [Tooltip("Number of bullets in each spray")]
    public int numBulletsPerSpray = 5;
    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Angle change per spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerSpray = 20.0f;
    [Tooltip("Positive or negative (1 or -1)")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;
    [Tooltip("Minimum angle")]
    [Range(0.0f, 360.0f)]
    public float minimumAngle = 4.0f;

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";

    //script refs
    private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private bool canShootBullet = false; //checks whether bullet can be fired
    private float angleChangeBetweenSprays = 0.0f; //the difference in angle between sprays

    // Use this for initialization
    void Start () {
        bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        enemyState = GetComponent<EnemyState>();
    }
	
	// Update is called once per frame
	void Update () {
        if (angleChangePerShot < minimumAngle)
        {
            angleChangePerShot = minimumAngle;
        }
        if (enemyState.GetIsActive())
        {
            if (Time.time > timeLastSprayFired + timeBetweenSprays)
            {
                StartCoroutine(BulletSprayRoutine());
            }
        }
    }

    //get the angle change between sprays based on number of specified sprays
    private void GetAngleBetweenSprays()
    {
        angleChangeBetweenSprays = 360 / numSprays;
    }

    //coroutine version of bullet spray
    private IEnumerator BulletSprayRoutine()
    {
        GetAngleBetweenSprays();

        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //for the number of shots in a spray
        for (int i = 0; i < numBulletsPerSpray; i++)
        {
            //for the number of sprays per call
            for (int j = 0; j < numSprays; j++)
            {
                //get an altered angle based on which spray
                float alteredAngle = currentAngle + (angleChangeBetweenSprays * j);
                //get a rotation
                Quaternion alteredRotation = new Quaternion();
                alteredRotation.eulerAngles = new Vector3(0.0f, alteredAngle, 0.0f);
                //get a bullet from the bank
                GameObject bullet = bank.GetRegularStraightBullet();
                //set the bullets position to this pos
                bullet.transform.position = transform.position;
                //set the bullet's rotation to current rotation
                bullet.transform.rotation = alteredRotation;
                //setup the bullet and fire
                bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            }
            //change the angle between shots
            currentAngle += angleChangePerShot * rotationDirection;
        }
        //wait for the next spray
        yield return new WaitForSecondsRealtime(timeBetweenSprays);

        //increase angle in prep of next spray
        currentAngle += angleChangePerSpray * rotationDirection;
    }
}
