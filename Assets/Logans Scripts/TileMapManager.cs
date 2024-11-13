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

    char[,] map = new char[20, 20];


    private void Start()
    {
        GenerateMap(); //Gererate the map 
        DrawTileMap(); // draw the map
    }

    void GenerateMap()
    {
        System.Random randW = new System.Random();

        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                int randWX = randW.Next(1, map.GetLength(0) - 1);
                int randWY = randW.Next(1, map.GetLength(1) - 1);

                if (x == 0 || x == map.GetLength(0) - 1 || y == 0 || y == map.GetLength(1) - 1)
                {
                    
                    map[randWX, randWY] = '#';//wall
                }
                else
                {
                    map[x, y] = '.';//floor
                }
            }
        }

        System.Random randD = new System.Random();

        for (int y = 1; y < map.GetLength(1) - 1; y++)
        {
            for (int x = 1; x < map.GetLength(1) - 1; x++)
            {
                int randDX = randD.Next(1, map.GetLength(0) - 1);
                int randDY = randD.Next(1, map.GetLength(1) - 1);

                if (map[x, y] == '.' && (map[x - 1, y] == '#' || map[x + 1, y] == '#' || map[x, y - 1] == '#' || map[x, y + 1] == '#'))
                {
                    map[x, y] = '0'; //door
                }
            }
        }

        //places chests ($) in random places 
        int chestCount = 0; 
        // Spawn random chest (randC)
        System.Random randC = new System.Random();
        while (chestCount < 2) // max chests
        {
            int randCX = randC.Next(1, map.GetLength(0) - 1);
            int randCY = randC.Next(1, map.GetLength(1) - 1);

            // place a chest in the map 
            if (map[randCX, randCY] == '.' && map[randCX - 1, randCY] != '#' && map[randCX + 1, randCY] != '#' && map[randCX, randCY - 1] != '#' && map[randCX, randCY + 1] != '#')
            {
                map[randCX, randCY] = '$'; //chest
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
                if (map[x,y] == '#')// wall
                {
                    MyTilemap.SetTile(Cellposition, wall);
                }
                else if (map[x,y] == '.')// floor 
                {
                    MyTilemap.SetTile(Cellposition, floor);
                }
                else if (map[x, y] == '0')// door
                {
                    MyTilemap.SetTile(Cellposition, door);
                }
                else if (map[x,y] == '$')// chest
                {
                    MyTilemap.SetTile(Cellposition, chest);
                }
            }
        }
    }

}
