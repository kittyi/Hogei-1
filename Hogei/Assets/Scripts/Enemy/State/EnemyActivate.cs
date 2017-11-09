using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivate : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Dungeon room tag")]
    public string dungeonRoomTag = "Room";

    //array of room game objects
    private GameObject[] dungeonRoomArray = new GameObject[0];
    private GameObject myRoom;

	// Use this for initialization
	void Start () {
        FindMyRoom();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //searches out all rooms on start, and assigns my room
    private void FindMyRoom()
    {
        //get all the rooms
        dungeonRoomArray = GameObject.FindGameObjectsWithTag(dungeonRoomTag);
        //search for the room closest to you
        float furthestDistance = 5000.0f;
        //iterate through all found rooms
        for (int i = 0; i < dungeonRoomArray.Length; i++)
        {
            //if closer, set as my room
            if (Vector3.Distance(transform.position, dungeonRoomArray[i].transform.position) < furthestDistance)
            {
                //make this room my room
                myRoom = dungeonRoomArray[i];
                //set furthest distance to this distance
                Vector3.Distance(transform.position, dungeonRoomArray[i].transform.position);
            }
        }
    }
}
