using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    IDictionary<string, int> dungeonFloor = new Dictionary<string, int>();
    int dungeonWidth;
    int dungeonHeight;

    List<RoomData> roomList = new List<RoomData>();

    public enum TileLookup
    {
        Stone,
        Floor,
        Wall
    }



    
    public DungeonGenerator(int width, int height)
    {
        dungeonWidth = width;
        dungeonHeight = height;
    }

    public void GenerateDungeon()
    {
        //first fill it with walls
        for (var coordY = 0; coordY < dungeonHeight; coordY++)
        {
            for (var coordX = 0; coordX < dungeonWidth; coordX++)
            {
                string tileName = coordX + "x" + coordY;
                dungeonFloor.Add(tileName, 0);
            }
        }

        //then we dig out the rooms
        int maxRoomCalls = 20;
        for(int ctr = 0; ctr <maxRoomCalls; ctr++)
        {
            //for each room call lets roll coord, width, height

            int roomWidth = Mathf.RoundToInt(Random.Range(3,6));
            int roomHeight = Mathf.RoundToInt(Random.Range(3,6));

            int posX = Mathf.RoundToInt(Random.Range(0, dungeonWidth));
            int posY = Mathf.RoundToInt(Random.Range(0, dungeonHeight));

            RoomData generatedRoom = new RoomData(posX, posY, roomWidth, roomHeight);

            //then check if its valid
            if (ValidRoomPlace(generatedRoom))
            {
                AddRoom(generatedRoom);
            }
        }

    }





    /*
     For alterations
         */

    public void AddRoom(RoomData room)
    {
        if (room.width % 2 != 0)
        {
            room.width++;
        }

        if(room.height%2 != 0)
        {
            room.height++;
        }

        //RoomData room = new RoomData(x,y,width,height);


        int halfHeight = Mathf.FloorToInt(room.height / 2);
        int halfWidth = Mathf.FloorToInt(room.width/2);


        for (var ctry = room.y - halfHeight+1; ctry <= room.y + halfHeight-1; ctry++)
        {
            for (var ctrx = room.x - halfWidth+1; ctrx <= room.x + halfWidth-1; ctrx++)
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

    bool ValidRoomPlace(RoomData data)
    {
        //check edges
        if(data.x-data.width >= 0 &&
            data.x+data.width < dungeonWidth &&
            data.y - data.height >= 0 &&
            data.y+data.height < dungeonHeight &&

            GetTileAt(data.x - data.width/2, data.y - data.height/2) == 0 &&
            GetTileAt(data.x - data.width/2, data.y + data.height/2) == 0 &&
            GetTileAt(data.x + data.width/2, data.y - data.height/2) == 0 &&
            GetTileAt(data.x + data.width/2, data.y + data.height/2) == 0 &&
            GetTileAt(data.x, data.y) == 0
            )
        {
            return true;
        }
        return false;
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

    public bool CheckIfWalkable(int x, int y)
    {
        return CheckIfWalkable(GetTileAt(x, y));
    }

    public bool CheckIfWalkable(int tileType)
    {
        switch (tileType)
        {
            case 0:
                return false;
            case 1:
                return true;
            case 2:
                return false;
            default:
                return false;
        }

    }


}
