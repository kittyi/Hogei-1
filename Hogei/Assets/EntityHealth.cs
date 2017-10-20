using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {

    bool InvincibilityFrame = false; 

    public float CurrentHealth;
    [Tooltip("Maximum health the entity can have")]
    public float MaxHealth = 10;

    bool DOTActive;
    float DOTDamage;
    float DOTDuration;
    float DOTStart;

	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if(CurrentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
		if(DOTActive)
        {
            CurrentHealth -= DOTDamage * Time.deltaTime;
            if(Time.time - DOTStart > DOTDuration)
            {
                DOTActive = false;
            }
        }
	}

    public void DecreaseHealth(float _value)
    {
        CurrentHealth -= _value;
        if (CurrentHealth < 0) CurrentHealth = 0;
    }
    
    public void IncreaseHealth(float _value)
    {
        CurrentHealth += _value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    //Deals the given damage spread over the time given
    public void DecreaseHealthOverTime(float _totalDamage, float _time)
    {
        DOTActive = true;
        DOTDamage = _totalDamage / _time;
        DOTDuration = _time;
    }
}
