using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public Tile wall;
    public Tile floor;
    public Tile chest;
    public Tile door;

    //public int[,] map = new int[20, 20];
    char[,] map = new char[20, 20];


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
                    map[x, y] = '#';
                }
                else
                {
                    map[x, y] = '.';
                }
            }
        }

        for (int y = 1; y < map.GetLength(1) - 1; y++)
        {
            for (int x = 1; x < map.GetLength(1) - 1; x++)
            {
                if (map[x,y] == '.' && (map[x - 1, y] == '#' || map[x + 1, y] == '#' || map[x, y - 1] == '#' || map[x, y + 1] == '#'))
                {
                    map[x, y] = '0'; //door
                }
            }
        }

        //places chests ($) in random places 
        int chestCount = 0; 
        System.Random rand = new System.Random();
        while (chestCount < 2) // max chests
        {
            int randX = rand.Next(1, map.GetLength(0) - 1);
            int randY = rand.Next(1, map.GetLength(1) - 1);

            if (map[randX, randY] == '.' && map[randX - 1, randY] != '#' && map[randX + 1, randY] != '#' && map[randX, randY - 1] != '#' && map[randX, randY + 1] != '#')
            {
                map[randX, randY] = '$'; //chest
                chestCount++;
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
                if (map[x,y] == '#')
                {
                    MyTilemap.SetTile(Cellposition, wall);
                }
                else if (map[x,y] == '.')// floor 
                {
                    MyTilemap.SetTile(Cellposition, floor);
                }
                else if (map[x,y] == '0')
                {
                    MyTilemap.SetTile(Cellposition, door);
                }
                else if (map[x,y] == '$')
                {
                    MyTilemap.SetTile(Cellposition, chest);
                }
            }
        }
    }

}
