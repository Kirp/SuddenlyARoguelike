using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform floorTileParent;
    DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = new DungeonGenerator(60, 30);
        dungeonGenerator.GenerateDungeon();
        dungeonGenerator.AddRoom(10,10,2,2);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    //interface for mobile tiles
    public bool IsValidMoveToTile(int x, int y)
    {
        return dungeonGenerator.IsWithinBounds(x, y);
    }
}
