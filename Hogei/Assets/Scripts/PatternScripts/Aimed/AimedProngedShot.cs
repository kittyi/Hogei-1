using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedProngedShot : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 5.0f;
    [Tooltip("Minimum time between sprays")]
    public float minTimeBetweenSprays = 0.1f;
    //scaled time between sprays
    private float scaledTimeBetweenSprays = 0.0f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Tooltip("Number of bullet layers")]
    public int numBulletLayers = 1;
    [Tooltip("Max num of bullet layers")]
    public int maxNumBulletLayers = 5;
    //scaled num of layers
    private int scaledNumLayers = 0;

    [Tooltip("Number of arcs")]
    public int numArcs = 2;
    [Tooltip("Max num of arcs")]
    public int maxNumArcs = 4;
    //scaled num of arcs
    private int scaledNumArcs = 0;

    [Tooltip("First layer speed")]
    public float firstLayerBulletSpeed = 1.0f;
    [Tooltip("Max speed of bullet")]
    public float maxBulletSpeed = 10.0f;
    //scaled speed of bullet
    private float scaledBulletSpeed = 0.0f;
    [Tooltip("Layer speed increment value")]
    public float layerSpeedIncrementValue = 0.5f;

    [Header("Angle Control")]
    [Tooltip("Starting angle")]
    [Range(0.0f, 360.0f)]
    public float startingAngle = 0.0f;

    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;
    [Tooltip("Minimum angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float minAngleChangePerShot = 1.0f;
    //scaled angle change per shot
    private float scaledAngleChangePerShot = 0.0f;

    [Header("Tags")]
    //[Tooltip("Bullet bank tag")]
    //public string bulletBankTag = "Bullet Bank";
    [Tooltip("Target Tag")]
    public string targetTag = "Player";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //target ref
    private GameObject target;

    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner

    // Use this for initialization
    void Start()
    {
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        enemyState = GetComponent<EnemyState>();
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        enemyState = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState.GetIsActive())
        {
            if (Time.time > timeLastSprayFired + scaledTimeBetweenSprays)
            {
                StartCoroutine(BulletSprayRoutine());
            }
        }
    }

    //scales the values based on how deep player is
    public void ScaledShotVars(int level)
    {
        //time between sprays
        scaledTimeBetweenSprays = timeBetweenSprays - level;
        //check not below min
        if (scaledTimeBetweenSprays < minTimeBetweenSprays)
        {
            scaledTimeBetweenSprays = minTimeBetweenSprays;
        }

        //num of layers
        scaledNumLayers = numBulletLayers + level;
        //check not above max
        if (scaledNumLayers > maxNumBulletLayers)
        {
            scaledNumLayers = maxNumBulletLayers;
        }

        //num of arcs
        scaledNumArcs = numArcs + (level / 4);
        //check if above max
        if (scaledNumArcs > maxNumArcs)
        {
            scaledNumArcs = maxNumArcs;
        }

        //bullet speed
        scaledBulletSpeed = firstLayerBulletSpeed + level;
        //check not above max
        if (scaledBulletSpeed > maxBulletSpeed)
        {
            scaledBulletSpeed = maxBulletSpeed;
        }

        //angle per shot
        scaledAngleChangePerShot = angleChangePerShot - (level * 1.5f);
        //check not below min
        if (scaledAngleChangePerShot < minAngleChangePerShot)
        {
            scaledAngleChangePerShot = minAngleChangePerShot;
        }
    }

    //bullet firing coroutine
    private IEnumerator BulletSprayRoutine()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //speed var
        float speed = scaledBulletSpeed;

        //for all layers
        for (int i = 0; i < scaledNumLayers; i++)
        {
            //get direction to target
            Vector3 directionToTarget = target.transform.position - transform.position;
            //remove any changes in y
            directionToTarget.y = 0;
            //get quaternion
            Quaternion targetedRotation = Quaternion.LookRotation(directionToTarget);


            //for all arcs
            for (int j = 0; j < scaledNumArcs; j++)
            {
                //get a rotation
                Quaternion alteredRotation = transform.rotation;
                alteredRotation.eulerAngles = new Vector3(0.0f, targetedRotation.eulerAngles.y + (startingAngle + (scaledAngleChangePerShot * j)), 0.0f);

                /*get a bullet from the bank
                GameObject bullet = bank.GetRegularStraightBullet();*/

                //create a bullet
                GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
                //set the bullets position to this pos
                bullet.transform.position = transform.position;
                //set the bullet's rotation to current rotation
                bullet.transform.rotation = alteredRotation;
                //setup the bullet and fire
                bullet.GetComponent<RegularStraightBullet>().SetupVars(speed);

                //if not first (middle) shot, than create a second shot with negative angle
                if (j > 0)
                {
                    //get a rotation
                    alteredRotation = transform.rotation;
                    alteredRotation.eulerAngles = new Vector3(0.0f, targetedRotation.eulerAngles.y + (startingAngle + (-scaledAngleChangePerShot * j)), 0.0f);

                    /*get a bullet from the bank
                    GameObject bullet2 = bank.GetRegularStraightBullet();*/

                    GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);

                    //set the bullets position to this pos
                    bullet2.transform.position = transform.position;
                    //set the bullet's rotation to current rotation
                    bullet2.transform.rotation = alteredRotation;
                    //setup the bullet and fire
                    bullet2.GetComponent<RegularStraightBullet>().SetupVars(speed);
                }
            }
            //increment the speed between layers
            speed += layerSpeedIncrementValue;
        }
        //wait for next spray
        yield return new WaitForSecondsRealtime(scaledTimeBetweenSprays);
    }
}
