using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    IDictionary<string, int> dungeonFloor = new Dictionary<string, int>();
    int dungeonWidth;
    int dungeonHeight;


    public enum TileLookup
    {
        Wall,
        Floor
    }
    
    public DungeonGenerator(int width, int height)
    {
        dungeonWidth = width;
        dungeonHeight = height;
    }

    public void GenerateDungeon()
    {
        for (var coordY = 0; coordY < dungeonHeight; coordY++)
        {
            for (var coordX = 0; coordX < dungeonWidth; coordX++)
            {
                string tileName = coordX + "x" + coordY;
                dungeonFloor.Add(tileName, 0);
            }
        }
    }


    public void AddRoom(int x, int y)
    {

    }


    /*
     For alterations
         */
    
    public void ChangeTile(int x, int y, int changeTo)
    {
        string tileName = x + "x" + y;
        dungeonFloor[tileName] =  changeTo;
    }


    /*
     FOR OUTPUT
         */
    public int GetTileAt(int x, int y)
    {
        string tileName = x + "x" + y;
        return dungeonFloor[tileName];
    }

    public TileData[] GetAllTiles()
    {
        TileData[] tileDataList = new TileData[dungeonFloor.Count];
        int ctr = 0;
        foreach(KeyValuePair<string, int> tile in dungeonFloor)
        {
            string[] split = tile.Key.Split("x");
            tileDataList[ctr] = new TileData( int.Parse(split[0]), int.Parse(split[1]),tile.Value);
            ctr++;
        }
        return tileDataList;
    }



}
