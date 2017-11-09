using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public struct Coordinates
    {
        public int x;
        public int y;
    }

    public enum RoomShape
    {
        SQUARE,
        CIRCLE
    }

    private float TileSize = 10;
    public Coordinates RoomCoord;
    public GameObject Floor;
    public GameObject Wall;
    public List<Transform> DoorTiles;
    public RoomShape Shape;
    public List<GameObject> CorridorsTo;
    public Dictionary<Transform, Transform> DoorPairs;
    public bool CornerRoom = false;
    public bool LockPos = false;
    public bool AlignmentRan = false;

    List<GameObject> FloorTiles;
    List<GameObject> WallTiles;
    GameObject[,] Tiles;
    private int RoomWidth;
    private int RoomLength;


    // Use this for initialization
    void Start()
    {
        Tiles = new GameObject[0, 0];
        if (DoorPairs == null || DoorPairs.Count == 0) DoorPairs = new Dictionary<Transform, Transform>();
        if (CorridorsTo == null || CorridorsTo.Count == 0) CorridorsTo = new List<GameObject>();
    }

    public void Init(int _Width, int _Length, float _TileSize, bool _Draw)
    {
        RoomWidth = _Width;
        RoomLength = _Length;
        TileSize = _TileSize;

        if (RoomWidth <= 0 && RoomLength <= 0)
        {
            RoomWidth = Random.Range(3, 10);
            RoomLength = Random.Range(3, 10);
        }
        Tiles = new GameObject[RoomWidth, RoomLength];
        if (DoorPairs == null || DoorPairs.Count == 0) DoorPairs = new Dictionary<Transform, Transform>();
        if (CorridorsTo == null || CorridorsTo.Count == 0) CorridorsTo = new List<GameObject>();

        if (_Draw) GenerateRoom();
    }

    public void GenerateRoom()
    {
        DeleteRoom();

        Tiles = new GameObject[RoomWidth, RoomLength];

        for (int i = 0; i < RoomWidth; ++i)
        {
            for (int j = 0; j < RoomLength; ++j)
            {
                Vector3 NewPos = transform.position + new Vector3((float)(i - RoomWidth / 2) * TileSize, 0f, (float)(j - RoomLength / 2) * TileSize);
                Tiles[i, j] = Instantiate(Floor, NewPos, Quaternion.identity, this.transform);
            }
        }
    }

    public void GenerateCorridors()
    {

        print("\n####" + name + "####");
        LockPos = true;


        if (CorridorsTo.Count == 0)
        {
            Debug.LogError("No corridor connections to this room");
        }

        print("##Checking Corridors##");
        print("Corridor List Data: " + CorridorsTo.ToString() + " Count: " + CorridorsTo.Count);
        foreach (GameObject cor in CorridorsTo)
        {
            float ClosestDoors = 100000;
            Transform Room1Door = null;
            Transform Room2Door = null;
            //Figure out the closest door 
            foreach (Transform door in DoorTiles)
            {
                print(door.name + " DoorTiles Count: " + DoorTiles.Count);

                print("##Getting Closest Doors##");
                foreach (Transform otherDoor in cor.GetComponent<RoomGenerator>().DoorTiles)
                {
                    if ((otherDoor.position - door.position).magnitude < ClosestDoors)
                    {
                        Room1Door = door;
                        Room2Door = otherDoor;
                        ClosestDoors = (otherDoor.position - door.position).magnitude;
                    }
                }
            }

            Vector3 Dist = Room2Door.position - Room1Door.position;
            print("Dist: " + Dist.ToString());
            print(this.name + Room1Door.name + ": " + Room1Door.position.ToString() + cor.name + Room2Door + ": " + Room2Door.position.ToString());
            print("##Entering Alignment##");
            //Align the doors on the same axis
            if (Mathf.Abs(Dist.x) > Mathf.Abs(Dist.z) && Room1Door.position.z != (Room2Door.position + Room2Door.right * TileSize).z)
            {
                print("X Difference Is Bigger\nTile Size: " + TileSize);
                if (cor.GetComponent<RoomGenerator>().LockPos || cor.GetComponent<RoomGenerator>().LockPos && LockPos)
                {
                    print("#Moving this room");
                    Vector3 Adjustment = (Room2Door.position + Room2Door.right * TileSize) - Room1Door.position;
                    print(transform.position.ToString());
                    Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + Adjustment.z);
                    transform.position = newPos;
                    print(transform.position.ToString());
                }
                else
                {
                    print("#Moving other room");
                    Vector3 Adjustment = (Room1Door.position + Room1Door.right * TileSize) - (Room2Door.position);
                    print(cor.transform.position.ToString());
                    Vector3 newPos = new Vector3(cor.transform.position.x, cor.transform.position.y, cor.transform.position.z + Adjustment.z);
                    cor.transform.position = newPos;
                    print(cor.transform.position.ToString());
                    cor.GetComponent<RoomGenerator>().LockPos = true;
                }
            }
            else if (Mathf.Abs(Dist.z) > Mathf.Abs(Dist.x) && Room1Door.position.x != (Room2Door.position + Room2Door.right * TileSize).x)
            {
                print("Z Difference Is Bigger\nTile Size: " + TileSize);
                if (cor.GetComponent<RoomGenerator>().LockPos || cor.GetComponent<RoomGenerator>().LockPos && LockPos)
                {
                    print("#Moving this room");
                    Vector3 Adjustment = (Room2Door.position + Room2Door.right * TileSize) - Room1Door.position;
                    print(transform.position.ToString());
                    Vector3 newPos = new Vector3(transform.position.x + Adjustment.x, transform.position.y, transform.position.z);
                    transform.position = newPos;
                    print(transform.position.ToString());
                }
                else
                {
                    print("#Moving other room");
                    Vector3 Adjustment = (Room1Door.position + Room1Door.right * TileSize) - (Room2Door.position);
                    print(cor.transform.position.ToString());
                    Vector3 newPos = new Vector3(cor.transform.position.x + Adjustment.x, cor.transform.position.y, cor.transform.position.z);
                    print(newPos.ToString());
                    cor.transform.position = newPos;
                    print(cor.transform.position.ToString());
                    cor.GetComponent<RoomGenerator>().LockPos = true;
                }
            }
            print("##Exiting Alignment##\n");

            DoorPairs.Add(Room1Door, Room2Door);
        }
    }

    public void PlaceCorridorHalf()
    {
        foreach (Transform door in DoorTiles)
        {
            Transform Room1Door = door;
            Transform Room2Door = null;
            DoorPairs.TryGetValue(door, out Room2Door);

            //Create the corridor models
            float CorridorLength = 0.0f;
            Vector3 Dist = Room2Door.position - Room1Door.position;
            if (Mathf.Abs(Dist.x) > Mathf.Abs(Dist.z))
            {
                CorridorLength = Mathf.Abs((Room2Door.position - Room1Door.position).x);
            }
            else if (Mathf.Abs(Dist.z) > Mathf.Abs(Dist.x))
            {
                CorridorLength = Mathf.Abs((Room2Door.position - Room1Door.position).z);
            }
            print(Room1Door.position.ToString() + " / " + Room2Door.position.ToString());
            print("Corridor Length: " + CorridorLength);
            Vector3 newPos = Room1Door.position;
            Vector3 newWallPos = Room1Door.position + (Room1Door.forward * TileSize) / 2 - (Room1Door.right * TileSize) / 2;
            print(newPos.ToString() + " / " + newWallPos.ToString());
            for (int i = 0; i < (CorridorLength - 1) / TileSize; i++)
            {
                newPos += Room1Door.forward * TileSize;
                newWallPos += Room1Door.forward * TileSize;

                GameObject newFloorTile = Instantiate(Floor, newPos, Quaternion.identity);
                newFloorTile.transform.parent = transform;
                newFloorTile.name = "CorridorFloor_" + i;

                GameObject newWallTile = Instantiate(Wall, newWallPos, Quaternion.LookRotation(-Room1Door.right, Room1Door.up));
                newWallTile.transform.parent = transform;
                newWallTile.name = "CorridorWall_" + i;
             }
        }
    }

    public void AlignDoorsToNeighbors()
    {
        AlignmentRan = true;
        int Tries = 0;
        while (!CheckDoorsFacing())
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            Tries++;
            if (Tries > 8)
            {
                break;
            }
        }

        foreach (GameObject room in CorridorsTo)
        {
            if (!room.GetComponent<RoomGenerator>().AlignmentRan)
            {
                room.GetComponent<RoomGenerator>().AlignDoorsToNeighbors();
            }
        }
    }

    bool CheckDoorsFacing()
    {
        int DoorsAligned = 0;
        if (DoorTiles == null || DoorTiles.Count == 0)
        {
            return true;
        }
        foreach (GameObject cor in CorridorsTo)
        {
            Vector3 Direction = cor.transform.position - transform.position;
            foreach (Transform door in DoorTiles)
            {
                float Angle = Vector3.Angle(door.forward, Direction);
                if (Mathf.Abs(Angle) < 45f)
                {
                    DoorsAligned += 1;
                    break;
                }
            }
        }
        if (DoorsAligned == DoorTiles.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Return if the room is a corner room and needs a special room model
    public bool IsCornerRoom()
    {
        if (CorridorsTo.Count != 2)
        {
            return false;
        }
        else
        {
            Vector3 p1 = CorridorsTo[0].transform.position - this.transform.position;
            Vector3 p2 = CorridorsTo[1].transform.position - this.transform.position;
            float Angle = Vector3.Angle(p1, p2);
            print(name + ": " + Angle);
            if (Mathf.Abs(Angle) > 45f && Mathf.Abs(Angle) < 135)
            {
                CornerRoom = true;
            }
            return CornerRoom;
        }
    }

    //realigns the room with the grid
    void RealignRoom()
    {
        Vector3 newPos = Vector3.zero;
        newPos = new Vector3(TileSize * Mathf.Ceil(transform.position.x / TileSize), transform.position.y, TileSize * Mathf.Ceil(transform.position.z / TileSize));
        transform.position = newPos;
    }


    void DeleteRoom()
    {
        for (int i = 0; i < RoomWidth; ++i)
        {
            for (int j = 0; j < RoomLength; ++j)
            {
                GameObject.Destroy(Tiles[i, j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCorridor(GameObject _Room)
    {
        CorridorsTo.Add(_Room);
    }

    public void SetRoomWidth(int _Width)
    {
        RoomWidth = _Width;
    }

    public void SetRoomLength(int _Length)
    {
        RoomLength = _Length;
    }

    public int GetRoomWidth()
    {
        return RoomWidth;
    }

    public int GetRoomLength()
    {
        return RoomLength;
    }
}
