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
        Tiles = new GameObject[0, 0];
    }

    public void Init(int _Width, int _Length)
    {
        RoomWidth = _Width;
        RoomLength = _Length;
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
                Vector3 NewPos = transform.position + new Vector3((float)(i - RoomWidth/2) * 10, 0f, (float)(j - RoomLength/2)  * 10);
                Tiles[i,j] = Instantiate(Floor, NewPos, Quaternion.identity, this.transform);
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
