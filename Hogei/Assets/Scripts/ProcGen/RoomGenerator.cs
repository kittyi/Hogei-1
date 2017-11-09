using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {

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
    public bool CornerRoom = false;
    public bool LockPos = false;

    List<GameObject> FloorTiles;
    List<GameObject> WallTiles;
    GameObject[,] Tiles;
    private int RoomWidth;
    private int RoomLength;


    // Use this for initialization
    void Start () {
        Tiles = new GameObject[0, 0];
        if(CorridorsTo == null || CorridorsTo.Count == 0) CorridorsTo = new List<GameObject>();
    }

    public void Init(int _Width, int _Length, float _TileSize)
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

        GenerateRoom();
    }

    public void GenerateRoom()
    {
        DeleteRoom();

        Tiles = new GameObject[RoomWidth, RoomLength];
        
        for (int i = 0; i < RoomWidth; ++i)
        {
            for(int j = 0; j < RoomLength; ++j)
            {
                Vector3 NewPos = transform.position + new Vector3((float)(i - RoomWidth/2) * TileSize, 0f, (float)(j - RoomLength/2)  * TileSize);
                Tiles[i,j] = Instantiate(Floor, NewPos, Quaternion.identity, this.transform);
            }
        }
    }

    public void AlignDoorsToNeighbors()
    {
        LockPos = true;
        int Tries = 0;
        while(!CheckDoorsFacing())
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            Tries++;
            if(Tries > 8)
            {
                break;
            }
        }
        
        foreach (GameObject cor in CorridorsTo)
        {
            //Figure out the closest door 
            foreach (Transform door in DoorTiles)
            {
                float ClosestDoors = 100000;
                Transform Room1Door = null;
                Transform Room2Door = null;

                foreach (Transform otherDoor in cor.GetComponent<RoomGenerator>().DoorTiles)
                {
                    if((otherDoor.position - door.position).magnitude < ClosestDoors)
                    {
                        Room1Door = door;
                        Room2Door = otherDoor;
                        ClosestDoors = (otherDoor.position - door.position).magnitude;
                    }
                }

                Vector3 Dist = Room2Door.position - Room1Door.position;
                //Align the doors on the same axis
                if (Dist.x > Dist.z)
                {
                    if (cor.GetComponent<RoomGenerator>().LockPos)
                    {
                        transform.position += new Vector3(0f, 0f, Room2Door.position.z - Room1Door.position.z);
                    }
                    else
                    {
                        cor.transform.position += new Vector3(0f, 0f, Room2Door.position.z - Room1Door.position.z);
                    }
                }
                else
                {
                    if (cor.GetComponent<RoomGenerator>().LockPos)
                    {
                        transform.position += new Vector3(Room2Door.position.x - Room1Door.position.x, 0f, 0f);
                    }
                    else
                    {
                        cor.transform.position += new Vector3(Room2Door.position.x - Room1Door.position.x, 0f, 0f);
                    }
                }
            }           
        }
    }

    bool CheckDoorsFacing()
    {
        int DoorsAligned = 0;
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
        if(DoorsAligned == DoorTiles.Count)
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
        if(CorridorsTo.Count != 2)
        {
            return false;
        }
        else
        {
            Vector3 p1 = CorridorsTo[0].transform.position - this.transform.position;
            Vector3 p2 = CorridorsTo[1].transform.position - this.transform.position;
            float Angle = Vector3.Angle(p1, p2);
            print(name + ": " + Angle);
            if(Mathf.Abs(Angle) > 45f && Mathf.Abs(Angle) < 135)
            {
                CornerRoom = true;                
            }
            return CornerRoom;
        }
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
	void Update () {
		
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
