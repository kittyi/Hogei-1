using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour {

    [Header("Player Bullet")]
    [Tooltip("Player bullet ref")]
    public GameObject peaBullet;

    [Header("Bullet vars")]
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 5.0f;

    [Header("Timing vars")]
    [Tooltip("The amount of time between shots")]
    public float timeBetweenShots = 2.0f;

    [Header("Input")]
    [Tooltip("Keyboard input key for weapon fire")]
    public int mouseInputKey = 0;

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
    public void UseWeapon()
    {
        //check if input 
        if (CheckKeyboardInputWeapon() && Time.time > lastShotTime + timeBetweenShots)
        {
            //get a bullet
        }
    }

    //keyboard input check for firing weapon <- to avoid clunkiness in code
    private bool CheckKeyboardInputWeapon()
    {
        bool valid = false;
        if (Input.GetMouseButtonDown(mouseInputKey))
        {
            valid = true;
        }
        return valid;
    }
}
