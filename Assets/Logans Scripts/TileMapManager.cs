using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public int[,] map = new int[10, 10];
        

    private void Start()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (Random.value < 0.5f)
                {
                    map[x, y] = 1;
                }
            }
        }
        DrawTileMap();
    }


    private void Update()
    {
        
    }

    void DrawTileMap()
    {

    }

}
