using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Input")]
    [Tooltip("Keyboard input key for weapon fire")]
    public int mouseInputKey = 0;

    //control vars
    private int currentWeaponIndex = 0;
    public bool peaShootStrengthened = false;

    //script refs
    private PeaShooter peaShooter;
    private PlayerStreamShot streamShot;
    private PlayerHomingShot homingShot;

    // Use this for initialization
    void Start () {
		peaShooter = GetComponentInChildren<PeaShooter>();
        streamShot = GetComponentInChildren<PlayerStreamShot>();
        homingShot = GetComponentInChildren<PlayerHomingShot>();
    }
	
	// Update is called once per frame
	void Update () {
        UseWeapon();
	}

    //attack use logic
    public void UseWeapon()
    {
        //check if input 
        if (CheckKeyboardInputWeapon())
        {
            //try to use current weapon
            switch (currentWeaponIndex)
            {
                case 0:
                    //StartCoroutine(peaShooter.UseWeapon(peaShootStrengthened));
                    //streamShot.UseWeapon();
                    homingShot.UseWeapon();
                    break;

                    
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
