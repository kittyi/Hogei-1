using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;
    [Tooltip("Minimum time between sprays")]
    public float minTimeBetweenSprays = 0.1f;
    //scaled time between sprays
    private float scaledTimeBetweenSprays = 0.0f;

    [Header("Bullet Vars")]
    [Tooltip("Number of bullet per waves")]
    public int numBulletWaves = 2;
    [Tooltip("Max number of bullets per wave")]
    public int maxNumBulletWaves = 5;
    //scaled number of bullets per wave
    private int scaledNumBulletWaves = 0;

    [Header("Bullet set up vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet base setup distance")]
    public float bulletBaseSetupDistance = 0.5f;
    [Tooltip("Bullet step distance increase value")]
    public float bulletStepDistanceIncrease = 0.5f;
    [Tooltip("Bullet setup move time")]
    public float bulletSetupTime = 0.5f;
    [Tooltip("Bullet active move start time")]
    public float bulletStartMoveTimeDelay = 0.2f;
    [Tooltip("Bullet angle change")]
    public float bulletAngleChange = 90.0f;

    [Tooltip("Pattern bullet set speed")]
    public float patternBulletSpeed = 2.0f;
    [Tooltip("Max speed of bullet")]
    public float maxBulletSpeed = 10.0f;
    //scaled speed of bullet
    private float scaledBulletSpeed = 0.0f;

    [Header("Angle Control")]
    [Tooltip("Facing angle")]
    [Range(0.0f, 360.0f)]
    public float facingAngle = 0.0f;

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began

    // Use this for initialization
    void Start () {
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        enemyState = GetComponent<EnemyState>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyState.GetIsActive())
        {
            if (Time.time > timeLastSprayFired + scaledTimeBetweenSprays)
            {
                BulletSprayRoutine();
            }
        }
    }

    //scales values based on how deep player is
    public void ScaleShotValues(int level)
    {
        //time between sprays
        scaledTimeBetweenSprays = timeBetweenSprays - level;
        //check not below min
        if (scaledTimeBetweenSprays < minTimeBetweenSprays)
        {
            scaledTimeBetweenSprays = minTimeBetweenSprays;
        }

        //num bullets
        scaledNumBulletWaves = numBulletWaves + level;
        if (scaledNumBulletWaves > maxNumBulletWaves)
        {
            scaledNumBulletWaves = maxNumBulletWaves;
        }

        //bullet speed
        scaledBulletSpeed = patternBulletSpeed + level;
        //check not above max
        if (scaledBulletSpeed > maxBulletSpeed)
        {
            scaledBulletSpeed = maxBulletSpeed;
        }
    }

    //bullet firing coroutine
    private void BulletSprayRoutine()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //for each wave
        for (int i = 0; i < scaledNumBulletWaves; i++)
        {
            //get the distance to set up
            float distanceToSetup = bulletBaseSetupDistance + (bulletStepDistanceIncrease * i);
            //get a bullet from the bank
            GameObject bullet1 = Instantiate(bulletObject, transform.position, transform.rotation);

            //set the bullets position to this pos
            bullet1.transform.position = transform.position;

            //get rotation 90 degree from facing angle
            Quaternion alteredRotation = new Quaternion();
            alteredRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + facingAngle + 90.0f, 0.0f);

            bullet1.transform.rotation = alteredRotation;

            //setup the bullet
            bullet1.GetComponent<SetupStraightBullet>().SetupVars(distanceToSetup, bulletSetupTime, bulletSetupTime + bulletStartMoveTimeDelay, 270.0f, scaledBulletSpeed);

            //get a second bullet from the bank
            GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);

            //set the bullet's pos to this pos
            bullet2.transform.position = transform.position;

            //get rotation 270 degree from facing angle
            alteredRotation = new Quaternion();
            alteredRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + facingAngle + 270.0f, 0.0f);

            bullet2.transform.rotation = alteredRotation;

            //setup the bullet
            bullet2.GetComponent<SetupStraightBullet>().SetupVars(distanceToSetup, bulletSetupTime, bulletSetupTime + bulletStartMoveTimeDelay, 90.0f, scaledBulletSpeed);
        }

        //yield return new WaitForSecondsRealtime(scaledTimeBetweenSprays);
    }
}
