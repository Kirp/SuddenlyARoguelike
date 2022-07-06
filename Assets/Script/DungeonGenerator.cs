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
        Wall,
        Floor,
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

            int roomWidth = Mathf.RoundToInt(Random.Range(4,8));
            int roomHeight = Mathf.RoundToInt(Random.Range(4,8));

            int posX = Mathf.RoundToInt(Random.Range(0, dungeonWidth-roomWidth));
            int posY = Mathf.RoundToInt(Random.Range(0, dungeonHeight-roomHeight));

            RoomData generatedRoom = new RoomData(posX, posY, roomWidth, roomHeight);

            //then check if its valid
            if (!AreThereFloorTilesOnRoomProposedSite(generatedRoom))
            {
                Debug.Log("pass");
                AddRoom(generatedRoom);
            }
            else
            {
                Debug.Log("fail");
            }
        }

        ConnectRoomToRoom(roomList[0], roomList[1]);

    }





    /*
     For alterations
         */


    public void ConnectRoomToRoom(RoomData startRoom, RoomData targetRoom)
    {
        Vector2Int startRoomCenter = new Vector2Int(startRoom.x + startRoom.width / 2, startRoom.y + startRoom.height / 2);
        Vector2Int targetRoomCenter = new Vector2Int(targetRoom.x + targetRoom.width / 2, targetRoom.y + targetRoom.height / 2);

        //lets not do a check and just plow thru anything
        Vector2Int digTrajectory = Vector2Int.zero;
        bool startWithYAxis = Random.value > 0.5f ? true : false;
        if (startWithYAxis)
        {
            DigACorridor(startRoomCenter, new Vector2Int(startRoomCenter.x, targetRoomCenter.y));
        }else
        {
            DigACorridor(startRoomCenter, new Vector2Int(targetRoomCenter.x, startRoomCenter.y));
        }

    }

    public void DigACorridor(Vector2Int fromPoint, Vector2Int targetPoint)
    {
        for (var ctry = fromPoint.y; ctry < targetPoint.y; ctry++)
        {
            for (var ctrx = fromPoint.x; ctrx < targetPoint.x; ctrx++)
            {
                ChangeTile(ctrx, ctry, 1);
            }
        }


    }

    public void AddRoom(RoomData room)
    {
        for (var ctry = room.y; ctry < room.y + room.height; ctry++)
        {
            for (var ctrx = room.x; ctrx < room.x + room.width; ctrx++)
            {
                ChangeTile(ctrx, ctry, 1);
            }
        }


        for (var ctry = room.y+1; ctry < room.y + room.height-1; ctry++)
        {
            for (var ctrx = room.x+1; ctrx < room.x + room.width-1; ctrx++)
            {
                ChangeTile(ctrx, ctry, 2);
            }
        }
        


        roomList.Add(room);


    }

    bool IsRoomOutOfBounds(RoomData data)
    {
        //check edges
        bool outOfBounds = false;
        if(data.x-data.width >= 0 &&
            data.x+data.width < dungeonWidth &&
            data.y - data.height >= 0 &&
            data.y+data.height < dungeonHeight
            )
        {
            outOfBounds = true;
        }
        return outOfBounds;
    }

    public void ChangeTile(int x, int y, int changeTo)
    {
        string tileName = x + "x" + y;
        dungeonFloor[tileName] =  changeTo;
    }

    bool AreThereFloorTilesOnRoomProposedSite(RoomData room)
    {
        for (int ty = room.y; ty < room.y + room.height; ty++)
        {
            for (int tx = room.x; tx < room.x + room.width; tx++)
            {
                if (GetTileAt(tx, ty) != 0) return true;
            }
        }
        return false;
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
                return false;
            case 2:
                return true;
            default:
                return false;
        }

    }

    
}
