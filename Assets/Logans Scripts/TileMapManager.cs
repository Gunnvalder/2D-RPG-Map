using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public Tile wall;
    public Tile floor;
    public Tile chest;
    public Tile door;
    public Tile playerTile;

    char[,] map = new char[20, 15];

    private Vector3Int CurrentCell; 
    public float moveSpeed = 1f;

    private void Start()
    {
        GenerateMap(); //Gererate the map 
        DrawTileMap(); // draw the map

        CurrentCell = MyTilemap.WorldToCell(transform.position);
        DrawPlayerTile();
    }

    private void Update()
    {
        Vector3Int moveDirection = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector3Int(0,1,0);
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        { 
            moveDirection = new Vector3Int(0,-1,0); 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = new Vector3Int(-1,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            moveDirection = new Vector3Int(1, 0, 0);
        }

        if (moveDirection != Vector3Int.zero)
        {
            Vector3Int targetCell = CurrentCell + moveDirection;

            TileBase targetTile = MyTilemap.GetTile(targetCell);

            if (targetTile != null && (targetTile == floor || targetTile == door))
            {
                MyTilemap.SetTile(CurrentCell, floor);

                CurrentCell = targetCell;

                DrawPlayerTile();
            }
        }
    }

    void GenerateMap()
    {
        System.Random rand = new System.Random();


        //  Go for  each position in Y in the grid starting at 0 and ending before 20
        //      Go for each position in X in the grid starting at 0 and ending before 20
       //           Define random number randX between 1 and 19
       //           same for randY but different random number

        //          if the X is 0 or the x is 19 or  the y is 0 or the y is 19
                        // Place a wall in position X and Y
                        // place wall in random position within the borders
                    // If it's not the stuff I said above
                        // place a floor in position X and Y
                        // 

            // Go for each position in Y in the grid starting at 0 
            for (int y = 0; y < map.GetLength(1); y++)
            {
                //Debug.Log($"starting y {y} map length {map.GetLength(1)}");
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    //Debug.Log($"         starting x {x} y is {y} map length {map.GetLength(0)}");

                    int randx = rand.Next(1, map.GetLength(0) - 1);
                    int randy = rand.Next(1, map.GetLength(1) - 1);

                    //Debug.Log($"      Is X {x} == 0 ? {x == 0} is x == the matp length - 1? {x == map.GetLength(0) - 1}");
                    if (x == 0 || x == map.GetLength(0) - 1 || y == 0 || y == map.GetLength(1) - 1)
                    {
                        //Debug.Log($"         Conditions for wall found for  x {x} y is {y} Making it a wall now");
                        map[x, y] = '#';//wall
                        map[randx, randy] = '#';
                    }
                    else
                    {
                        map[x, y] = '.';//floor
                    }
                }
            }
        



        // for each grid read y starting at 1 in the y 
            // for each grid read x starting at 1 in the x 
                // if 



        int doorCount = 0;
        while (doorCount < 4)
        {
            int randx = rand.Next(1, map.GetLength(0) - 1);
            int randy = rand.Next(1, map.GetLength(1) - 1);

            if (map[randx, randy] == '.' && (map[randx - 1, randy] == '#' || map[randx + 1, randy] == '#' || map[randx, randy - 1] == '#' || map[randx, randy + 1] == '#'))
            {
                map[randx, randy] = '0';
                doorCount++;
            }
        }


        //places chests ($) in random places 
        int chestCount = 0; 
        // Spawn random chest (randC)
        System.Random randC = new System.Random();
        while (chestCount < 2) // max chests
        {
            int randx = rand.Next(1, map.GetLength(0) - 1);
            int randy = rand.Next(1, map.GetLength(1) - 1);

            // place a chest in the map 
            if (map[randx, randy] == '.' && map[randx - 1, randy] != '#' && map[randx + 1, randy] != '#' && map[randx, randy - 1] != '#' && map[randx, randy + 1] != '#')
            {
                map[randx, randy] = '$'; //chest
                chestCount++;
            }
        }

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (x > 0 && x < map.GetLength(0) - 1 && y > 0 && y < map.GetLength(1) - 1)
                {
                    map[x, y] = '@';
                }
            }
        }

    }

    void DrawPlayerTile()
    {
        MyTilemap.SetTile(CurrentCell, floor);
        MyTilemap.SetTile(CurrentCell, playerTile);
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
