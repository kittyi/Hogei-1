using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Input")]
    [Tooltip("Keyboard input key for weapon fire")]
    [Range(0, 1)]
    public int mouseInputKey = 0;
    [Tooltip("Switch weapon <-")]
    public KeyCode prevWeaponInput = KeyCode.Q;
    [Tooltip("Switch weapon ->")]
    public KeyCode nextWeaponInput = KeyCode.E;

    //control vars
    private int currentWeaponIndex = 0;
    private int numWeapons = 0;
    public bool peaShootStrengthened = false;

    //script refs
    private WhatCanIDO canDo;
    private PeaShooter peaShooter;
    private PlayerStreamShot streamShot;
    private PlayerHomingShot homingShot;

    // Use this for initialization
    void Start () {
        canDo = GetComponent<WhatCanIDO>();
        SetupWeapons();
    }
	
	// Update is called once per frame
	void Update () {
        if (canDo.canShoot)
        {
            UseWeapon();
            SwitchWeapon();
        }
	}

    //setup weapon script releationships 
    private void SetupWeapons()
    {
        peaShooter = GetComponentInChildren<PeaShooter>();
        if (peaShooter)
        {
            numWeapons++;
        }
        streamShot = GetComponentInChildren<PlayerStreamShot>();
        if (streamShot)
        {
            numWeapons++;
        }
        homingShot = GetComponentInChildren<PlayerHomingShot>();
        if (homingShot)
        {
            numWeapons++;
        }
    }

    //attack use logic
    private void UseWeapon()
    {
        //check if input 
        if (CheckKeyboardInputWeapon())
        {
            //try to use current weapon
            switch (currentWeaponIndex)
            {
                case 0:
                    StartCoroutine(peaShooter.UseWeapon(peaShootStrengthened));
                    //streamShot.UseWeapon();
                    //homingShot.UseWeapon();
                    break;
                case 1:
                    streamShot.UseWeapon();
                    break;
                case 2:
                    homingShot.UseWeapon();
                    break;
            }
        }
    }

    //switch weapon
    private void SwitchWeapon()
    {
        //switch to prev weapon
        if (Input.GetKeyDown(prevWeaponInput)){
            //decrement the current index
            currentWeaponIndex--;
            //if index becomes -1
            if(currentWeaponIndex < 0)
            {
                //set weapon index to last weapon
                currentWeaponIndex = numWeapons;
            }
        }
        //switch to next weapon
        else if (Input.GetKeyDown(nextWeaponInput))
        {
            //increment the current index
            currentWeaponIndex++;
            //if weapon becomes larger than number of weapons
            if(currentWeaponIndex > numWeapons)
            {
                //set weapon to first weapon
                currentWeaponIndex = 0;
            }
        }
    }

    //keyboard input check for firing weapon <- to avoid clunkiness in code
    private bool CheckKeyboardInputWeapon()
    {
        bool valid = false;
        if (Input.GetMouseButton(mouseInputKey))
        {
            valid = true;
        }
        return valid;
    }
}
