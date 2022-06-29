using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = new DungeonGenerator(20, 20);
        dungeonGenerator.GenerateDungeon();
        dungeonGenerator.ChangeTile(1, 0, 1);
        dungeonGenerator.ChangeTile(5,5,1);
        GenerateTileMap();

    }

    private void GenerateTileMap()
    {
        TileData[] getDat = dungeonGenerator.GetAllTiles();

        foreach (TileData tile in getDat)
        {
            GameObject genTile = Instantiate(tilePrefab, new Vector3(tile.x, 0f, tile.y), Quaternion.identity);
            genTile.GetComponent<Tile>().ChangeTileTo(tile.tileType);
            Debug.Log(tile.x + " : " + tile.y + " = " + tile.tileType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
