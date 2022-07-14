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
    GameObject currentPlayerTile = null;
    FOVControl fovControl;

    public GameObject CurrentPlayerTile
    {
        get { return currentPlayerTile; }
    }
    
    IDictionary<string, TileData> generatedMap = null;
    IDictionary<string, GameObject> generatedTiles = new Dictionary<string, GameObject>();
    public List<RoomData> generatedRoomList = null;


    private void Awake()
    {
        dungeonGenerator = new DungeonGenerator(dungeonWidth, dungeonHeight);
        generatedMap = dungeonGenerator.GenerateDungeon();

        GenerateTileMap();
        generatedRoomList = dungeonGenerator.RoomList;


        RoomData randomStartup = generatedRoomList[Random.Range(0, generatedRoomList.Count - 1)];
        Vector2Int centerRoom = new Vector2Int(randomStartup.x + randomStartup.width / 2, randomStartup.y + randomStartup.height / 2);
        currentPlayerTile = Instantiate(playerTile, new Vector3(centerRoom.x, 1, centerRoom.y), Quaternion.identity);

        fovControl = GetComponent<FOVControl>();
        
    }

    // Start is called before the first frame update
    void Start()
    {

        fovControl.RunFOVCheck();
    }

    private void GenerateTileMap()
    {
        TileData[] tiles = dungeonGenerator.GetAllTiles();

        foreach (TileData tile in tiles)
        {
            GameObject genTile = Instantiate(tilePrefab, new Vector3(tile.x, 0f, tile.y), Quaternion.identity);
            Tile tileScript = genTile.GetComponent<Tile>();
            tileScript.ChangeTileTo(tile.tileType);
            genTile.transform.parent = floorTileParent;
            string coord = tile.x + "x" + tile.y;
            tileScript.LoadTileData(generatedMap[coord]);
            generatedTiles[coord] = genTile;
        }
    }

    

    //interface for mobile tiles
    public bool IsValidMoveToTile(int x, int y)
    {
        bool isVal = IsCoordinateWithinBounds(x, y) && CheckIfWalkable(x,y);
        Debug.Log("move validity: "+isVal);
        return isVal;
    }

    public bool CheckIfWalkable(int x, int y)
    {
        
        
        string coord = x + "x" + y;
        if (generatedMap == null || !generatedMap.ContainsKey(coord))
        {
            Debug.Log("key not found");
            return false;
        }
        Debug.Log("key found: "+coord+" => " + generatedMap[coord].walkable);
        return generatedMap[coord].walkable;
        

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

    public void LightUpTiles(Vector2Int[] lightEm)
    {
        string namer;
        for(var ctr =0; ctr < lightEm.Length; ctr++)
        {
            Vector2Int changeThis = lightEm[ctr];
            if (IsCoordinateWithinBounds(changeThis.x, changeThis.y))
            {
                namer = changeThis.x + "x" + changeThis.y;
                TileData tileData = generatedMap[namer];
                tileData.SetToVisible();
                tileData.SetIsLit(true);
                generatedMap[namer] = tileData;
                generatedTiles[namer].GetComponent<Tile>().LoadTileData(tileData);
            }
        }
    }

    public void PlayerCallFOV()
    {
        fovControl.RunFOVCheck();
    }

    
}
