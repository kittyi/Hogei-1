using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour {

    [Header("Player Bullet")]
    [Tooltip("Player bullet ref")]
    public GameObject peaBullet;

    [Header("Bullet vars")]
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeedFast = 10.0f;
    [Tooltip("Bullet travel speed before empowering")]
    public float bulletTravelSpeedSlow = 5.0f;
    [Tooltip("Bullet max travel distance before empowering")]
    public float bulletMaxTravelDist = 8.0f;

    [Header("Timing vars")]
    [Tooltip("The amount of time between shots")]
    public float timeBetweenShots = 2.0f;

    [Header("Tags")]
    [Tooltip("Bullet bank tag")]
    public string bankTag = "Bullet Bank";

    //bullet bank ref
    private BulletBank bank;

    //control vars
    private float lastShotTime = 0.0f; //time last bullet was fired

	// Use this for initialization
	void Start () {
        bank = GameObject.FindGameObjectWithTag(bankTag).GetComponent<BulletBank>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //attack use logic
    public void UseWeapon(bool empowered)
    {
        //check if input 
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //get a bullet
            GameObject bullet = bank.GetPlayerStraightBullet();
            //set the bullets position to this pos
            bullet.transform.position = transform.position;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = transform.rotation;
            //setup the bullet and fire
            if (empowered)
            {
                bullet.GetComponent<PlayerStraightBullet>().SetupVars(bulletTravelSpeedFast, bulletMaxTravelDist, false);
            }
            else
            {
                bullet.GetComponent<PlayerStraightBullet>().SetupVars(bulletTravelSpeedSlow, bulletMaxTravelDist, true);
            }
            
        }
    }
}
