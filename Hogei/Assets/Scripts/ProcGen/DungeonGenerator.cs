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
    public float TileSize = 10;

    public GameObject[] RoomPrefabs;


    public int RoomAmount = 5;
    [Tooltip("Minimum distance between the rooms (Will be rounded up to multiplies of 10)")]
    public float RoomPadding = 10;

    public int RoomWidthMin = 3, RoomWidthMax = 10;
    public int RoomLengthMin = 3, RoomLengthMax = 10;

    [Header("Dungeon Settings")]
    public int DungeonWidth = 25;
    public int DungeonLength = 50;

    [Header("Corridor Settings")]
    public int MaximumCorridorDis = 10;

    public GameObject[] Rooms;
    List<CorridorData> Corridors;

    void Start()
    {
        Rooms = new GameObject[RoomAmount];
        Corridors = new List<CorridorData>();
        //FloorTile.transform.localScale *= TileSize / 10;
    }

    //Generate Corridors between the rooms
    public void GenerateCorridors()
    {
        Corridors.Clear();

        //Figure out corridors
        for (int i = 0; i < Rooms.Length; ++i)
        {
            for (int j = 0; j < Rooms.Length; ++j)
            {
                if (j == i)
                {
                    continue;
                }
                Vector3 Distance = Rooms[i].transform.localPosition - Rooms[j].transform.localPosition;
                //print("Room" + i + " -> Room" + j + " Dist: " + Distance.ToString() + " Mag: " + Distance.magnitude + " Thres: " + RoomWidthMax * 3 + "/" + RoomLengthMax * 3);
                //if (Distance.magnitude < MaximumCorridorDis * 10)
                if (Mathf.Abs(Distance.x) + Mathf.Abs(Distance.z) < MaximumCorridorDis * TileSize)
                {
                    if (!CheckCorridorExists(i, j))// If the corridor doesn't already exist
                    {
                        print("Corridor Generated");
                        CorridorData NewCorridor;
                        NewCorridor.Room1 = i;
                        NewCorridor.Room2 = j;
                        Corridors.Add(NewCorridor);

                        Rooms[i].GetComponent<RoomGenerator>().CorridorsTo.Add(Rooms[j]);
                        Rooms[j].GetComponent<RoomGenerator>().CorridorsTo.Add(Rooms[i]);
                    }
                }
            }
        }
        //Add the room models
        ReplaceRooms();
        //Align the room doors
        AlignRoomDoors();
    }

    //Check a corridor between the two points don't already exist
    bool CheckCorridorExists(int _r1, int _r2)
    {
        foreach (CorridorData cor in Corridors)
        {
            if (cor.Room1 == _r1 && cor.Room2 == _r2 || cor.Room1 == _r2 && cor.Room2 == _r1)
            {
                return true;
            }
        }
        return false;
    }

    //Makes the room doors align with each other
    void AlignRoomDoors()
    {
        //foreach(GameObject Room in Rooms)
        //{
        //    Room.GetComponent<RoomGenerator>().AlignDoorsToNeighbors();
        //}
        Rooms[0].GetComponent<RoomGenerator>().AlignDoorsToNeighbors();

        foreach (CorridorData cor in Corridors)
        {
            //Get closet doorTiles
            RoomGenerator Room1 = Rooms[cor.Room1].GetComponent<RoomGenerator>();
            RoomGenerator Room2 = Rooms[cor.Room2].GetComponent<RoomGenerator>();

            Transform Room1Door;
            Transform Room2Door;
            float ClosestDoors = 100000;

            foreach(Transform Door1 in Room1.DoorTiles)
            {
                foreach(Transform Door2 in Room2.DoorTiles)
                {
                     
                }
            }
        }
    }

    void PlaceCorridor(GameObject Door1, GameObject Door2)
    {

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
            float RandomX = (int)Random.Range(transform.position.x - DungeonWidth, transform.position.x + DungeonWidth);
            float RandomZ = (int)Random.Range(transform.position.z - DungeonLength, transform.position.z + DungeonLength);
            //Round to the nearest multiple of 10
            RandomX = Mathf.Ceil(RandomX / TileSize) * TileSize;
            RandomZ = Mathf.Ceil(RandomZ / TileSize) * TileSize;


            GameObject tempRoom = new GameObject();
            tempRoom.name = "Room" + i.ToString();
            tempRoom.transform.parent = this.gameObject.transform;
            tempRoom.transform.localPosition = new Vector3(RandomX, transform.position.y, RandomZ);
            tempRoom.AddComponent<RoomGenerator>();
            tempRoom.GetComponent<RoomGenerator>().Floor = FloorTile;
            tempRoom.GetComponent<RoomGenerator>().Init(RoomWidth, RoomLength, TileSize, true);

            Rooms[i] = tempRoom;
        }
    }

    public void ReplaceRooms()
    {
        int[,] CorridorPerRoom = new int[Rooms.Length, 1];
        foreach (CorridorData cor in Corridors)
        {
            CorridorPerRoom[cor.Room1, 0] += 1;
            CorridorPerRoom[cor.Room2, 0] += 1;
        }
        for (int i = 0; i < Rooms.Length; ++i)
        {
            GameObject oldRoom = Rooms[i];
            switch (CorridorPerRoom[i, 0])
            {
                case 1:
                    Rooms[i] = Instantiate(RoomPrefabs[0], oldRoom.transform.position, oldRoom.transform.rotation, this.gameObject.transform);                    
                    break;
                case 2:
                    if (Rooms[i].GetComponent<RoomGenerator>().IsCornerRoom())
                    {
                        Rooms[i] = Instantiate(RoomPrefabs[1], oldRoom.transform.position, oldRoom.transform.rotation, this.gameObject.transform);
                    }
                    else
                    {
                        Rooms[i] = Instantiate(RoomPrefabs[2], oldRoom.transform.position, oldRoom.transform.rotation, this.gameObject.transform);
                    }
                    break;
                case 3:
                    Rooms[i] = Instantiate(RoomPrefabs[3], oldRoom.transform.position, oldRoom.transform.rotation, this.gameObject.transform);
                    break;
                case 4:
                    Rooms[i] = Instantiate(RoomPrefabs[3], oldRoom.transform.position, oldRoom.transform.rotation, this.gameObject.transform);
                    break;
                default:
                    break;
            }
            Rooms[i].name = "Room_" + i;
            Rooms[i].GetComponent<RoomGenerator>().Init(0, 0, TileSize, false);
            Destroy(oldRoom);
        }
        //Re-add Corridor data
        foreach(CorridorData cor in Corridors)
        {           
            Rooms[cor.Room1].GetComponent<RoomGenerator>().AddCorridor(Rooms[cor.Room2]);
            Rooms[cor.Room2].GetComponent<RoomGenerator>().AddCorridor(Rooms[cor.Room1]);
        }
        print("PIANO");
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
        RoomPadding = Mathf.Ceil(RoomPadding / TileSize) * TileSize;
        RoomPadding = Mathf.Ceil(RoomPadding / TileSize) * TileSize;
        Vector3 Between = _Room1.transform.localPosition - _Room2.transform.localPosition;
        float MinimumX = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomWidth() + _Room2.GetComponent<RoomGenerator>().GetRoomWidth()) * TileSize + RoomPadding;
        float MinimumZ = Mathf.Abs(_Room1.GetComponent<RoomGenerator>().GetRoomLength() + _Room2.GetComponent<RoomGenerator>().GetRoomLength()) * TileSize + RoomPadding;
        MinimumX /= 2;
        MinimumZ /= 2;


        if (Mathf.Abs(Between.z) < MinimumZ && Mathf.Abs(Between.x) < MinimumX)
        {
            //print("Collision Between: " + _Room1.name + "(" + _Room1.transform.position.ToString() + ") - " + _Room2.name + "(" + _Room2.transform.position.ToString() + ")");
            if (Mathf.Abs(Between.x) > Mathf.Abs(Between.z))
            {
                //print("Adjusting X");
                //print(MinimumX * (Between / Between.magnitude).x);
                float NewX = Mathf.Ceil((MinimumX - Between.x) / TileSize) * TileSize;
                //_Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(MinimumX * (Between / Between.magnitude).x, 0, 0);
                _Room1.transform.localPosition = _Room1.transform.localPosition + new Vector3(NewX, 0, 0);
            }
            else
            {
                //print("Adjusting Z");
                //print(MinimumX * (Between / Between.magnitude).z);
                float NewZ = Mathf.Ceil((MinimumZ - Between.z) / TileSize) * TileSize;
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

    public GameObject GetRoom(int _Index)
    {
        if (_Index >= Rooms.Length)
        {
            return null;
        }
        else
        {
            return Rooms[_Index];
        }
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

            foreach (CorridorData cor in Corridors)
            {
                Gizmos.color = Color.red;
                Debug.DrawLine(Rooms[cor.Room1].transform.position + transform.up, Rooms[cor.Room2].transform.position + transform.up, Color.red);
                string vector = (Rooms[cor.Room1].transform.position - Rooms[cor.Room2].transform.position).ToString() + "\n" + (Rooms[cor.Room1].transform.position - Rooms[cor.Room2].transform.position).magnitude;
                UnityEditor.Handles.Label((Rooms[cor.Room1].transform.position + Rooms[cor.Room2].transform.position) / 2 + transform.up, vector);
            }
        }
    }
}
