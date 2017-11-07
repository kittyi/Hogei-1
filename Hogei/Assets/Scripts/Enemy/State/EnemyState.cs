using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    [Header("Active")]
    [Tooltip("Check if enemy is active")]
    public bool isActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //get set methods
    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool active)
    {
        isActive = active;
    }
}
