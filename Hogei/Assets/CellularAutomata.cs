using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour {

    public int RoomWidth = 5;
    public int RoomHeight = 5;
    [Range(0f,1f)]
    public float InitTileChance = 0.5f;
    public int WallThreshold = 4;
    public int FloorThreshold = 5;
    public GameObject Floor;
    int[,] Tiles = new int[0,0];
    GameObject[] RoomTiles;


    // Use this for initialization
    void Start () {
        Tiles = new int[RoomWidth,RoomHeight];
        FillRandom();
        CreateRoom();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateRoom()
    {
        for (int i = 0; i < RoomWidth; ++i)
        {
            for (int j = 0; j < RoomHeight; ++j)
            {
                if(Tiles[i, j] == 0)
                {
                    Instantiate(Floor, new Vector3((float)i * 10, 0f, (float)j * 10), Quaternion.identity);
                }
            }
        }
    }

    public void Interate()
    {
        for (int i = 0; i < RoomWidth; ++i)
        {
            for (int j = 0; j < RoomHeight; ++j)
            {
                int Neighbors = CheckNeighbors(i, j);
                if(Neighbors > WallThreshold)
                {
                    Tiles[i, j] = 1;
                }
            }
        }
    }
    
    int CheckNeighbors(int x, int y)
    {
        int OffsetX, OffsetY, NumNeighbor;
        OffsetX = 0;
        OffsetY = 1;
        NumNeighbor = 0;

        for (int j = 0; j < 8; ++j)
        {
            if (x + OffsetX > 0 && x + OffsetX < RoomWidth && y + OffsetY > 0 && y + OffsetY < RoomHeight)
            {
                if (Tiles[x + OffsetX, y + OffsetY] == 1)
                {
                    NumNeighbor++;
                }
            }
            switch (j)
            {
                case 0:
                    OffsetX += 1;
                    break;
                case 1:
                case 2:
                    OffsetY -= 1;
                    break;
                case 3:
                case 4:
                    OffsetX -= 1;
                    break;
                case 5:
                case 6:
                case 7:
                    OffsetY -= 1;
                    break;
                
            }

        }

        return 0;
    }

    void FillRandom()
    {
        float RandomChance;
        for(int i = 0; i < RoomWidth; ++i )
        {
            for(int j = 0; j < RoomHeight; ++j)
            {
                RandomChance = Random.Range(0f, 1f);
                if(RandomChance < InitTileChance)
                {
                    Tiles[i, j] = 1;
                }
                else
                {
                    Tiles[i, j] = 0;
                }    
            }
        }
    }
}
