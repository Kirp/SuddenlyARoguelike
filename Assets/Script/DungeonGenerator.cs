using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    IDictionary<string, TileData> dungeonFloor = new Dictionary<string, TileData>();
    int dungeonWidth;
    int dungeonHeight;
    int maxRoomAttempts = 20;
    int roomMinimumSizeRange = 4;
    int roomMaximumSizeRange = 10;

    public List<RoomData> roomList = new List<RoomData>();

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

    public IDictionary<string, TileData> GenerateDungeon()
    {
        //first fill it with walls
        for (var coordY = 0; coordY < dungeonHeight; coordY++)
        {
            for (var coordX = 0; coordX < dungeonWidth; coordX++)
            {
                string tileName = coordX + "x" + coordY;
                TileData genDat = new TileData(coordX, coordY, 0);
                genDat.SetWalkableTransparentSettings(false, false);
                dungeonFloor.Add(tileName, genDat);
            }
        }

        //then we dig out the rooms
        
        for(int ctr = 0; ctr < maxRoomAttempts; ctr++)
        {
            //for each room call lets roll coord, width, height

            int roomWidth = Mathf.RoundToInt(Random.Range(roomMinimumSizeRange, roomMaximumSizeRange));
            int roomHeight = Mathf.RoundToInt(Random.Range(roomMinimumSizeRange, roomMaximumSizeRange));

            int posX = Mathf.RoundToInt(Random.Range(0, dungeonWidth-roomWidth));
            int posY = Mathf.RoundToInt(Random.Range(0, dungeonHeight-roomHeight));

            RoomData generatedRoom = new RoomData(posX, posY, roomWidth, roomHeight);

            //then check if its valid
            if (!AreThereFloorTilesOnRoomProposedSite(generatedRoom))
            {
                Debug.Log("pass");
                AddRoom(generatedRoom);
                //Debug.Log("total rooms: "+roomList.Count);
                if (roomList.Count > 1)
                {
                    ConnectRoomToRoom(generatedRoom, roomList[roomList.Count-2]);
                }
            }
            else
            {
                Debug.Log("fail");
            }
        }

        /*
        for(var ctr = 0; ctr<roomList.Count-1; ctr++)
        {
            ConnectRoomToRoom(roomList[ctr], roomList[ctr+1]);
        }
        */

        return dungeonFloor;

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
            DigACorridorVertical(startRoomCenter, new Vector2Int(startRoomCenter.x, targetRoomCenter.y));
            DigACorridorHorizontal(new Vector2Int(startRoomCenter.x, targetRoomCenter.y), new Vector2Int(targetRoomCenter.x, startRoomCenter.y));
        }
        else
        {
            DigACorridorHorizontal(startRoomCenter, new Vector2Int(targetRoomCenter.x, startRoomCenter.y));
            DigACorridorVertical(new Vector2Int(targetRoomCenter.x, startRoomCenter.y), new Vector2Int(startRoomCenter.x, targetRoomCenter.y));
        }

    }


    void DigACorridorVertical(Vector2Int pointA, Vector2Int pointB)
    {
        int from = pointA.y; 
        int target = pointB.y;
        if(pointB.y<pointA.y)
        {
            from = pointB.y;
            target = pointA.y;
        }

        for(var ctr = from; ctr<=target; ctr++)
        {
            ChangeTile(pointA.x, ctr, 2);
        }
    }

    void DigACorridorHorizontal(Vector2Int pointA, Vector2Int pointB)
    {
        int from = pointA.x; 
        int target = pointB.x;
        if(pointB.x<pointA.x)
        {
            from = pointB.x;
            target = pointA.x;
        }

        for(var ctr = from; ctr<=target; ctr++)
        {
            ChangeTile(ctr, pointA.y, 2);
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
        dungeonFloor[tileName].tileType = changeTo;


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
        return dungeonFloor[tileName].tileType;
    }

    public TileData[] GetAllTiles()
    {
        TileData[] tileDataList = new TileData[dungeonFloor.Count];
        int ctr = 0;
        foreach(KeyValuePair<string, TileData> tile in dungeonFloor)
        {
            tileDataList[ctr] = tile.Value;
            //string[] split = tile.Key.Split("x");
            //tileDataList[ctr] = new TileData( int.Parse(split[0]), int.Parse(split[1]),tile.Value);
            //tileDataList[ctr].SetWalkableTransparentSettings(CheckIfWalkable(tile.Value), CheckIfTransparent(tile.Value));
            
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

    public bool CheckIfTransparent(int tileType)
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
