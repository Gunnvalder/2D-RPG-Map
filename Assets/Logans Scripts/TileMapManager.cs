using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using TMPro;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public Tile wall;//"#"
    public Tile floor;//"."
    public Tile chest;//"$"
    public Tile door;//"0"
    public Tile playerTile;//"@"
    public Tile EnemyTile;//"!"

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI ChestText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI GameOvertext;

    char[,] map = new char[20, 20];

    private Vector3Int CurrentCell;
    public float moveSpeed = 1f;

    private Vector3Int CurrentEnemyCell;
    public float enemyMoveSpeed = 1f;

    private bool playerHasMoved = false;

    public int playerHealth = 3;
    public int enemyHealth = 2;

    public int enemyDamage = 1;

    private void Start()
    {
        GenerateMap(); //Gererate the map 
        DrawTileMap(); // draw the map

        CurrentEnemyCell = new Vector3Int(9,9,0);
        MyTilemap.SetTile(CurrentEnemyCell, EnemyTile);
        CurrentCell = new Vector3Int(1,1,0);
        MyTilemap.SetTile(CurrentCell, playerTile);

        WinText.gameObject.SetActive(false);
        GameOvertext.gameObject.SetActive(false);
        HealthText.text = "Health: " + playerHealth;
    }
    private void Update()
    {
        HandlePlayerMovement();

        if (playerHasMoved)
        {
            HandleEnemyMovement();
            playerHasMoved = false;
        }
    }

    void TakeDamage(int damage)
    {
        playerHealth -= damage;
        HealthText.text = "Health: " + playerHealth;

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameOvertext.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void HandlePlayerMovement()
    {
        Vector3Int moveDirection = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector3Int(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = new Vector3Int(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = new Vector3Int(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = new Vector3Int(-1, 0, 0);
        }

        if (moveDirection != Vector3Int.zero)
        {
            Vector3Int targetCell = CurrentCell + moveDirection;

            TileBase targetTile = MyTilemap.GetTile(targetCell);

            if (targetTile == EnemyTile)
            {
                TakeDamage(enemyDamage);
            }

            // check if tile is a floor  
            if (targetTile != null && (targetTile == floor))
            {
                MyTilemap.SetTile(CurrentCell, floor);

                // update player position 
                CurrentCell = targetCell;

                // set the player position
                MyTilemap.SetTile(CurrentCell, playerTile);

                playerHasMoved = true;
            }
        }
    }

    void HandleEnemyMovement()
    {
        Vector3Int EnemyMoveDirection = Vector3Int.zero;

        if (CurrentEnemyCell.x < CurrentCell.x)
        {
            EnemyMoveDirection = new Vector3Int(1, 0, 0);
        }
        else if (CurrentEnemyCell.x > CurrentCell.x)
        {
            EnemyMoveDirection = new Vector3Int(-1, 0, 0);
        }
        else if (CurrentEnemyCell.y < CurrentCell.y)
        {
            EnemyMoveDirection = new Vector3Int(0, 1, 0);
        }
        else if (CurrentEnemyCell.y > CurrentCell.y)
        {
            EnemyMoveDirection = new Vector3Int(0, -1, 0);
        }

        if (EnemyMoveDirection != Vector3Int.zero)
        {
            Vector3Int EnemyTargetCell = CurrentEnemyCell + EnemyMoveDirection;

            TileBase EnemyTargetTile = MyTilemap.GetTile(EnemyTargetCell);

            // check if tile is a floor
            if (EnemyTargetTile != null && (EnemyTargetTile == floor))
            {
                MyTilemap.SetTile(CurrentEnemyCell, floor);

                // update enemy position 
                CurrentEnemyCell = EnemyTargetCell;

                // set the enemy position
                MyTilemap.SetTile(CurrentEnemyCell, EnemyTile);
            }

            if (CurrentEnemyCell == CurrentCell)
            {
                TakeDamage(enemyDamage);
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

                    int randx = rand.Next(1, map.GetLength(1) - 1);
                    int randy = rand.Next(1, map.GetLength(1) - 1);

                    //Debug.Log($"      Is X {x} == 0 ? {x == 0} is x == the map length - 1? {x == map.GetLength(0) - 1}");
                    if (x == 0 || x == map.GetLength(0) - 1 || y == 0 || y == map.GetLength(1) - 1)
                    {
                        //Debug.Log($"         Conditions for wall found for  x {x} y is {y} Making it a wall now");
                        map[x, y] = '#';//wall
                    }
                    else
                    {
                        map[x, y] = rand.Next(0, 5) == 0 ? '#' : '.';
                    }
                }
            }

        int doorCount = 0;
        while (doorCount < 1)
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
