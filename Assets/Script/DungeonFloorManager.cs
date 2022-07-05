using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform floorTileParent;
    [SerializeField] int dungeonWidth = 30;
    [SerializeField] int dungeonHeight = 20;
    DungeonGenerator dungeonGenerator;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = new DungeonGenerator(dungeonWidth, dungeonHeight);
        dungeonGenerator.GenerateDungeon();
        //dungeonGenerator.AddRoom(14,11,2,4);
        GenerateTileMap();

    }

    private void GenerateTileMap()
    {
        TileData[] getDat = dungeonGenerator.GetAllTiles();

        foreach (TileData tile in getDat)
        {
            GameObject genTile = Instantiate(tilePrefab, new Vector3(tile.x, 0f, tile.y), Quaternion.identity);
            genTile.GetComponent<Tile>().ChangeTileTo(tile.tileType);
            genTile.transform.parent = floorTileParent;
            //Debug.Log(tile.x + " : " + tile.y + " = " + tile.tileType);
        }
    }

    

    //interface for mobile tiles
    public bool IsValidMoveToTile(int x, int y)
    {
        return dungeonGenerator.IsWithinBounds(x, y) && dungeonGenerator.CheckIfWalkable(x,y);
    }

    
}
