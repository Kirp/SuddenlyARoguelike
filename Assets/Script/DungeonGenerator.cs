using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    IDictionary<string, int> dungeonFloor = new Dictionary<string, int>();
    int dungeonWidth;
    int dungeonHeight;

    List<Vector4> roomList = new List<Vector4>();

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





    /*
     For alterations
         */

    public void AddRoom(int x, int y, int width, int height)
    {
        if (width % 2 != 0)
        {
            width++;
        }

        if(height%2 != 0)
        {
            height++;
        }

        Vector4 room = new Vector4(x,y,width,height);

        Debug.Log(room);

        int halfHeight = Mathf.FloorToInt(height / 2);
        int halfWidth = Mathf.FloorToInt(width/2);

        Debug.Log(halfHeight);
        Debug.Log(halfWidth);


        for (var ctry = y - halfHeight; ctry <= y + halfHeight; ctry++)
        {
            for (var ctrx = x - halfWidth; ctrx <= x + halfWidth; ctrx++)
            {
                ChangeTile(ctrx, ctry, 1);
            }
        }
        

        /*
        ChangeTile(x, y, 1);
        ChangeTile(x-width/2, y, 1);
        ChangeTile(x+width / 2, y, 1);
        ChangeTile(x, y-height/2, 1);
        ChangeTile(x, y+height/2, 1);
        */

        roomList.Add(room);


    }

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

    public bool IsWithinBounds(int x, int y)
    {
        if(
            x>=0              &&
            x<dungeonWidth   &&
            y>=0              &&
            y<dungeonHeight
           )
        {
            return true;
        }

        return false;
    }


}
