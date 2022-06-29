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

    public int GetTileAt(int x, int y)
    {
        string tileName = x + "x" + y;
        return dungeonFloor[tileName];
    }



}
