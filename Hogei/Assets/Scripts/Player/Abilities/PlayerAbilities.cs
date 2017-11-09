using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    
    public float TimeScale = 0.5f;
    private GameObject Player;

    [Header("Inputs")]
    [Tooltip("Slow ability key")]
    public KeyCode slowAbilityKey = KeyCode.F;
    [Tooltip("Shield ablity key")]
    public KeyCode shieldAbilityKey = KeyCode.G;

    [Header("Tags")]
    [Tooltip("Shield object tag")]
    public string shieldTag = "Shield";

    // Slow Time Ability
    [Header("Slow ability vars")]
    private bool SlowingTime = false;
    private float SlowTimer;
    public float SlowTimerReset = 1.0f;
    public float SlowTimeCooldownReset = 0.0f;
    private float SlowTimeCooldown;
    private bool SlowOnCD = false;
    // Shield Ability
    [Header("Shield ability vars")]
    public float ShieldDistance = 1.0f;
    public GameObject Shield;
    private bool ShieldActive = false; 
    public float ShieldResetTime = 4.0f;
    private float ShieldActiveTime;
    public float ShieldCooldownReset = 0.0f;
    private float ShieldCooldown;
    // CannonBall Ability


	// Use this for initialization
	void Start () {
        Player = this.gameObject;
        SlowTimer = SlowTimerReset;
        ShieldActiveTime = ShieldResetTime;
        SlowTimeCooldown = SlowTimeCooldownReset;
        ShieldCooldown = ShieldCooldownReset;
	}
	
	// Update is called once per frame
	void Update () {
        //time ability
        SlowTime();
        SlowAbilityTimer();
        SlowTimeAbilityCooldown();
        //Shield ability
        DropShield();
        ShieldTimer();
        ShieldAbilityCooldown();       
    }
    //Time ability functions
    void SlowTime()
    {
        if(Input.GetKeyDown(slowAbilityKey) && SlowOnCD == false)
        {
            SlowingTime = true;
            Time.timeScale = TimeScale;
        }
    }

    void SlowAbilityTimer()
    {
        if (SlowingTime == true)
        {
            SlowTimer -= 1.0f *Time.deltaTime;
            SlowOnCD = true;

            if (SlowTimer <= 0.0f)
            {
                Time.timeScale = 1.0f;
                SlowTimer = SlowTimerReset;
                SlowingTime = false;
            }
        }
    }

    void SlowTimeAbilityCooldown()
    {
        if(SlowOnCD == true)
        {
            SlowTimeCooldown -= 1.0f * Time.deltaTime;
            if(SlowTimeCooldown <= 0.0f)
            {
                SlowOnCD = false;
                SlowTimeCooldown = SlowTimeCooldownReset;
            } 
        }
    }
    //shield ability functions
    void DropShield()
    {
        Vector3 SpawnPosition = Player.transform.position + (Player.transform.forward * ShieldDistance);
        Quaternion PlayerRotation = Player.transform.rotation;

        if(Input.GetKeyDown(shieldAbilityKey) && ShieldActive == false)
        {
            Instantiate(Shield, SpawnPosition, PlayerRotation);
            ShieldActive = true;                      
        }
    }

    void ShieldTimer()
    {
        if(ShieldActive == true)
        {
            ShieldActiveTime -= 1.0f * Time.deltaTime;

            if(ShieldActiveTime <= 0.0f)
            {
                ShieldActiveTime = ShieldResetTime;
                Destroy(GameObject.FindGameObjectWithTag(shieldTag));                
            }
        }
    }

    void ShieldAbilityCooldown()
    {
        if(ShieldActive == true)
        {
            ShieldCooldown -= 1.0f * Time.deltaTime;
            if(ShieldCooldown <= 0.0f)
            {
                ShieldActive = false;
                ShieldCooldown = ShieldCooldownReset;
            }
        }
    }
}
//