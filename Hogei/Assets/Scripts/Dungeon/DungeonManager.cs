using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Delay before player is transported out after death")]
    public float exitDelayDeath = 2.0f;

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    [HideInInspector]
    public int currentFloor = 0;
    private float delayStartTime = 0;
    private bool isExiting = false;

    //script refs
    private EntityHealth playerHealth;
    private DungeonToTown dungeonToTown;
    private WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
        dungeonToTown = GetComponent<DungeonToTown>();
        playerHealth = GameObject.FindGameObjectWithTag(playerTag).GetComponent<EntityHealth>();
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isExiting)
        {
            if (Time.time > delayStartTime + exitDelayDeath)
            {
                //move to town
                dungeonToTown.MoveToTown();
                //once moved, allow movement again
                canDo.canMove = true;
            }
        }
        else
        {
            CheckPlayerHealth();
        }
	}

    //track player health, and when at 0, prepare to move player out
    private void CheckPlayerHealth()
    {
        //if health below 0
        if (playerHealth.CurrentHealth <= 0)
        {
            //delay start time is now
            delayStartTime = Time.time;
            //set leave to true
            isExiting = true;
            //prevent player from acting
            canDo.canMove = false;
            canDo.canShoot = false;
            canDo.canAbility = false;
        }
    }
}
