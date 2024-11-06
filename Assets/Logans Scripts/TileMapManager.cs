using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public TileBase wallTile; // Tile for a wall (choose in inspector with a sprite)
    public TileBase FloorTile;// Tile for a floor (choose in inspector with a sprite)

    public int[,] map = new int[20, 20];
        

    private void Start()
    {
        GenerateMap(); //Gererate the map 
        DrawTileMap(); // draw the map
    }

    void GenerateMap()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (x == 0 || x == map.GetLength(0) - 1 || y == 0 || y == map.GetLength(1) - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    // random walls and floors
                    map[x, y] = Random.value < 0.2f ? 1 : 0;
                }
            }
        }

    }

    void DrawTileMap()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Vector3Int Cellposition = new Vector3Int(x, y, 0);
                //wall
                if (map[x,y] == 1)
                {
                    MyTilemap.SetTile(Cellposition, wallTile);
                }
                else // floor 
                {
                    MyTilemap.SetTile(Cellposition, FloorTile);
                }
            }
        }
    }

}
