using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDoorCheck : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    //control vars
    [HideInInspector]
    public bool isPlayerPresent = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //check for player in collision
    private void OnTriggerEnter(Collider other)
    {
        //if player enters
        if (other.gameObject.CompareTag(playerTag))
        {
            isPlayerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if player enters
        if (other.gameObject.CompareTag(playerTag))
        {
            isPlayerPresent = false;
        }
    }
}
