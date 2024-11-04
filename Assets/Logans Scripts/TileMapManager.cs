using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap MyTilemap;
    public int[,] TileInt = new int[10, 10];
    

    private void Start()
    {
        TileInt[2, 3] = 1;
    }
}
