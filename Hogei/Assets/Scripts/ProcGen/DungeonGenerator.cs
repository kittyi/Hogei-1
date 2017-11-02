using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    struct CorridorData
    {
        public int Room1;
        public int Room2;
    };

    public GameObject FloorTile;

    public int RoomAmount = 5;
    [Tooltip("Minimum distance between the rooms (Will be rounded up to multiplies of 10)")]
    public int RoomPadding = 10;

    public int DungeonWidth = 25;
    public int DungeonLength = 50;

    public int RoomWidthMin = 3, RoomWidthMax = 10;
    public int RoomLengthMin = 3, RoomLengthMax = 10;

    GameObject[] Rooms;
    List<CorridorData> Corridors;

    void Start()
    {
        Rooms = new GameObject[RoomAmount];
        Corridors = new List<CorridorData>();
    }

    public void GenerateCorridors()
    {
        for (int i = 0; i < Rooms.Length; ++i)
        {
            for (int j = 0; j < Rooms.Length; ++j)
            {
                Vector3 Distance = Rooms[i].transform.localPosition - Rooms[j].transform.localPosition;
                print(Distance.ToString());
                if (Distance.magnitude < RoomWidthMax || Distance.magnitude < RoomLengthMax)
                {
                    print("Corridor Generated");
                    CorridorData NewCorridor;
                    NewCorridor.Room1 = i;
                    NewCorridor.Room2 = j;
                    Corridors.Add(NewCorridor);
                }
            }
        }
    }

    public void GenerateRooms()
    {
        DeleteDungeon();

        Rooms = new GameObject[RoomAmount];

        for (int i = 0; i < RoomAmount; ++i)
        {
            //Get random room size
            int RoomWidth = Random.Range(RoomWidthMin, RoomWidthMax);
            int RoomLength = Random.Range(RoomLengthMin, RoomLengthMax);
            //Get random room position
            int RandomX = (int)Random.Range(transform.position.x - DungeonWidth, transform.position.x + DungeonWidth);
            int RandomZ = (int)Random.Range(transform.position.z - DungeonLength, transform.position.z + DungeonLength);
            //Round to the nearest multiple of 10
            RandomX = (int)Mathf.Ceil(RandomX / 10) * 10;
            RandomZ = (int)Mathf.Ceil(RandomZ / 10) * 10;

            GameObject tempRoom = new GameObject();
            tempRoom.name = "Room" + i.ToString();
            tempRoom.transform.parent = this.gameObject.transform;
            tempRoom.transform.localPosition = new Vector3(RandomX, transform.position.y, RandomZ);
            tempRoom.AddComponent<RoomGenerator>();
            tempRoom.GetComponent<RoomGenerator>().Floor = FloorTile;
            tempRoom.GetComponent<RoomGenerator>().Init(RoomWidth, RoomLength);

            Rooms[i] = tempRoom;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void CheckRoomCollisions()
    {
        bool isColliding = true;

        //print("Checking Collisions");

        for (int i = 0; i < RoomAmount; ++i)
        {
            for (int j = 0; j < RoomAmount; ++j)
            {
                if (j == i) break;
                isColliding = false;

                isColliding = CheckCollision(Rooms[i], Rooms[j]);
                //print(j + ": " + isColliding);

                //if (isColliding) --j;

            }
        }

    }

    bool CheckCollision(GameObject _Room1, GameObject _Room2)
    {
        RoomPadding = (int)Mathf.Ceil(RoomPadding / 10) * 10;
        RoomPadding = (int)Mathf.Ceil(RoomPadding / 10) * 10;
        Vector3 Between = _Room1.transform.localPosition - _Room2.transform.localPosition;
        int MinimumX = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomWidth() + _Room2.GetComponent<RoomGenerator>().GetRoomWidth()) * 10 + RoomPadding;
        int MinimumZ = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomLength() + _Room2.GetComponent<RoomGenerator>().GetRoomLength()) * 10 + RoomPadding;
        MinimumX /= 2;
        MinimumZ /= 2;


        if (Mathf.Abs(Between.z) < MinimumZ && Mathf.Abs(Between.x) < MinimumX)
        {
            //print("Collision Between: " + _Room1.name + "(" + _Room1.transform.position.ToString() + ") - " + _Room2.name + "(" + _Room2.transform.position.ToString() + ")");
            if (Mathf.Abs(Between.x) > Mathf.Abs(Between.z))
            {
                //print("Adjusting X");
                //print(MinimumX * (Between / Between.magnitude).x);
                int NewX = (int)Mathf.Ceil((MinimumX - Between.x) / 10) * 10;
                //_Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(MinimumX * (Between / Between.magnitude).x, 0, 0);
                _Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(NewX, 0, 0);
            }
            else
            {
                //print("Adjusting Z");
                //print(MinimumX * (Between / Between.magnitude).z);
                int NewZ = (int)Mathf.Ceil((MinimumZ - Between.z) / 10) * 10;
                // _Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(0, 0, MinimumZ * (Between / Between.magnitude).z);
                _Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(0, 0, NewZ);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    void DeleteDungeon()
    {
        for (int i = 0; i < Rooms.Length; ++i)
        {
            if (Rooms[i] != null)
            {
                GameObject.Destroy(Rooms[i].gameObject);
            }
        }
        Corridors.Clear();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && this.enabled)
        {
            for (int i = 0; i < Rooms.Length; ++i)
            {
                if (Rooms[i] != null)
                {
                    UnityEditor.Handles.Label(Rooms[i].transform.position + transform.up, i.ToString());
                }
            }

            //foreach (CorridorData cor in Corridors)
            //{
            //    print("Drawing Corridor");
            //    print(Rooms[cor.Room1].transform.position.ToString() + " -> " + Rooms[cor.Room2].transform.position.ToString());
            //    Gizmos.color = Color.red;
            //    Gizmos.DrawLine(Rooms[cor.Room1].transform.position, Rooms[cor.Room2].transform.position);

            //UnityEditor.Handles.Label(Rooms[cor.Room1].transform.position + transform.up, cor.Room1.ToString());
            //UnityEditor.Handles.Label(Rooms[cor.Room2].transform.position + transform.up, cor.Room2.ToString());
            //}
        }
    }
}
