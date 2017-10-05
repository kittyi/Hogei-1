using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Number of bullet waves")]
    public int numBulletWaves = 5;

    [Header("Bullet set up vars")]
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

    [Header("Angle Control")]
    [Tooltip("Facing angle")]
    [Range(0.0f, 360.0f)]
    public float facingAngle = 0.0f;

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";

    //script refs
    private BulletBank bank;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began

    // Use this for initialization
    void Start () {
        bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timeLastSprayFired + timeBetweenSprays)
        {
            StartCoroutine(BulletSprayRoutine());
        }
    }

    //bullet firing coroutine
    private IEnumerator BulletSprayRoutine()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //for each wave
        for (int i = 0; i < numBulletWaves; i++)
        {
            //get the distance to set up
            float distanceToSetup = bulletBaseSetupDistance + (bulletStepDistanceIncrease * i);
            //get a bullet from the bank
            GameObject bullet1 = bank.GetSetupStraightBullet();

            //set the bullets position to this pos
            bullet1.transform.position = transform.position;

            //get rotation 90 degree from facing angle
            Quaternion alteredRotation = new Quaternion();
            alteredRotation.eulerAngles = new Vector3(0.0f, facingAngle + 90.0f, 0.0f);

            bullet1.transform.rotation = alteredRotation;

            //setup the bullet
            bullet1.GetComponent<SetupStraightBullet>().SetupVars(distanceToSetup, bulletSetupTime, bulletSetupTime + bulletStartMoveTimeDelay, 90.0f, patternBulletSpeed);

            //get a second bullet from the bank
            GameObject bullet2 = bank.GetSetupStraightBullet();

            //set the bullet's pos to this pos
            bullet2.transform.position = transform.position;

            //get rotation 270 degree from facing angle
            alteredRotation = new Quaternion();
            alteredRotation.eulerAngles = new Vector3(0.0f, facingAngle + 270.0f, 0.0f);

            bullet2.transform.rotation = alteredRotation;

            //setup the bullet
            bullet2.GetComponent<SetupStraightBullet>().SetupVars(distanceToSetup, bulletSetupTime, bulletSetupTime + bulletStartMoveTimeDelay, 270.0f, patternBulletSpeed);
        }

        yield return new WaitForSecondsRealtime(timeBetweenSprays);
    }
}
