using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    
    public float TimeScale = 0.5f;
    private bool SlowingTime = false;
    private float SlowTimer;
    public float SlowTimerReset = 1.0f;

    public float ShieldDistance = 1.0f;
    public GameObject Shield;
    private GameObject Player;
    public bool ShieldCooldown = false; //public for testing
    public float ShieldCooldownReset = 4.0f;
    private float ShieldCooldownTime;




	// Use this for initialization
	void Start () {
        Player = this.gameObject;
        SlowTimer = SlowTimerReset;
        ShieldCooldownTime = ShieldCooldownReset;
	}
	
	// Update is called once per frame
	void Update () {
        SlowTime();
        SlowSlowTimer();
        DropShield();
        ShieldTimer();       
    }

    void SlowTime()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SlowingTime = true;
            Time.timeScale = TimeScale;
        }
    }

    void SlowSlowTimer()
    {
        if (SlowingTime == true)
        {
            SlowTimer -= 1.0f *Time.deltaTime;

            if (SlowTimer <= 0.0f)
            {
                SlowingTime = false;
                Time.timeScale = 1.0f;
                SlowTimer = SlowTimerReset;
                print(SlowTimer);
            }
        }
    }

    void DropShield()
    {
        Vector3 SpawnPosition = Player.transform.position + (Player.transform.forward * ShieldDistance);
        Quaternion PlayerRotation = Player.transform.rotation;

        if(Input.GetKeyDown(KeyCode.Q) && ShieldCooldown == false)
        {
            Instantiate(Shield, SpawnPosition, PlayerRotation);
            ShieldCooldown = true;                      
        }
    }

    void ShieldTimer()
    {
        if(ShieldCooldown == true)
        {

            ShieldCooldownTime -= 1.0f * Time.deltaTime;

            if(ShieldCooldownTime <= 0.0f)
            {
                ShieldCooldown = false;
                ShieldCooldownTime = ShieldCooldownReset;
            }
        }
    }
}
