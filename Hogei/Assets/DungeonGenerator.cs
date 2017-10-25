using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public GameObject FloorTile;

    public int RoomAmount = 5;
    public int DungeonWidth = 25;
    public int DungeonLength = 50;

    public int RoomWidthMin = 3, RoomWidthMax = 10;
    public int RoomLengthMin = 3, RoomLengthMax = 10;

    GameObject[] Rooms;
	
    void Start()
    {
        Rooms = new GameObject[RoomAmount];
    }

    public void GenerateRooms()
    {
        for(int i = 0; i < RoomAmount; ++i)
        {
            int RoomWidth = Random.Range(RoomWidthMin, RoomWidthMax);
            int RoomLength = Random.Range(RoomLengthMin, RoomLengthMax);
            int RandomX = (int)Random.Range(transform.position.x - DungeonWidth, transform.position.x + DungeonWidth);
            int RandomZ = (int)Random.Range(transform.position.z - DungeonLength, transform.position.z + DungeonLength);

            GameObject tempRoom = new GameObject();
            tempRoom.AddComponent<RoomGenerator>();
            tempRoom.GetComponent<RoomGenerator>().Floor = FloorTile;
            tempRoom.GetComponent<RoomGenerator>().Init(RoomWidth, RoomLength);
            

            tempRoom.transform.position = new Vector3(RandomX, transform.position.y, RandomZ);

            Rooms[i] = tempRoom;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    void CheckRoomCollision(GameObject _Room1, GameObject _Room2)
    {
        Vector3 Between = _Room1.transform.position - _Room2.transform.position;
        float MinimumX = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomWidth() + _Room2.GetComponent<RoomGenerator>().GetRoomWidth());
        float MinimumZ = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomLength() + _Room2.GetComponent<RoomGenerator>().GetRoomLength());

        if (Between.z < MinimumZ || Between.x < MinimumX)
        {
            if (Mathf.Abs(Between.x) > Mathf.Abs(Between.z))
            {

            }
            else
            {

            }
        }
    }
}
