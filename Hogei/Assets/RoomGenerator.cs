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

    public Coordinates RoomCoord;
    public GameObject Floor;
    public GameObject Wall;
    public RoomShape Shape;

    List<GameObject> FloorTiles;
    List<GameObject> WallTiles;
    GameObject[,] Tiles;
    private int RoomWidth;
    private int RoomLength;


    // Use this for initialization
    void Start () {
        Tiles = new GameObject[RoomWidth, RoomLength];
	}

    public void GenerateRoom()
    {
        DeleteRoom();
        RoomWidth = Random.Range(3, 10);
        RoomLength = Random.Range(3, 10);
        Tiles = new GameObject[RoomWidth, RoomLength];
        for (int i = 0; i < RoomWidth; ++i)
        {
            for(int j = 0; j < RoomLength; ++j)
            {
                Tiles[i,j] = Instantiate(Floor, new Vector3((float)i * 10, 0f, (float)j * 10), Quaternion.identity, this.transform);
            }
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

    public int GetRoomWidth()
    {
        return RoomWidth;
    }

    public int GetRoomLength()
    {
        return RoomLength;
    }
}
