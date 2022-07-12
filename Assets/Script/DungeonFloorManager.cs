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
    [SerializeField] GameObject playerTile;
    
    IDictionary<string, TileData> generatedMap = null;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = new DungeonGenerator(dungeonWidth, dungeonHeight);
        generatedMap = dungeonGenerator.GenerateDungeon();
        GenerateTileMap();



        RoomData randomStartup = dungeonGenerator.roomList[Random.Range(0, dungeonGenerator.roomList.Count - 1)];
        Vector2Int centerRoom = new Vector2Int(randomStartup.x+randomStartup.width/2, randomStartup.y+randomStartup.height/2);
        Instantiate(playerTile, new Vector3(centerRoom.x, 1, centerRoom.y), Quaternion.identity);

    }

    private void GenerateTileMap()
    {
        TileData[] tiles = dungeonGenerator.GetAllTiles();

        foreach (TileData tile in tiles)
        {
            GameObject genTile = Instantiate(tilePrefab, new Vector3(tile.x, 0f, tile.y), Quaternion.identity);
            genTile.GetComponent<Tile>().ChangeTileTo(tile.tileType);
            genTile.transform.parent = floorTileParent;
        }
    }

    

    //interface for mobile tiles
    public bool IsValidMoveToTile(int x, int y)
    {
        return IsCoordinateWithinBounds(x, y) && dungeonGenerator.CheckIfWalkable(x,y);
    }

    public bool CheckIfWalkable(int x, int y)
    {
        return false;
        /*
        string coord = x + "x" + y;
        if (generatedMap==null && !generatedMap.ContainsKey(coord)) return false;
        */
        

    }

    public bool IsCoordinateWithinBounds(int x, int y)
    {
        if (
            x >= 0 &&
            x < dungeonWidth &&
            y >= 0 &&
            y < dungeonHeight
           )
        {
            return true;
        }

        return false;
    }

    
}
