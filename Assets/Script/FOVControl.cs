using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVControl : MonoBehaviour
{
    DungeonFloorManager dungeonFloorManager;

    bool activated = false;

    private void Awake()
    {
        dungeonFloorManager = GetComponent<DungeonFloorManager>();
    }

    public void StartFOV()
    {
        activated = true;
    }

    private void Update()
    {
        if (activated == false) return;

        


    }

    public void RunFOVCheck()
    {
        //get the player position and then the immediate sides
        if (dungeonFloorManager.CurrentPlayerTile == null) return;
        Vector2Int[] PositionList = GetCardinalDirectionsFromGameObject(dungeonFloorManager.CurrentPlayerTile);
        IDictionary<string, float> lightTileValues = new Dictionary<string, float>();






        //lets auto light up the room a player is in
        Vector2Int[] RoomTiles;
        RoomData pRoom = dungeonFloorManager.IsPlayerInARoom();
        if (pRoom != null)
        {
            Debug.Log("light up the room!");
            RoomTiles = dungeonFloorManager.GetRoomTileVectorList(pRoom);
            dungeonFloorManager.LightUpTiles(RoomTiles);
        }


        //now light em up in the dungeonFloorManager
        dungeonFloorManager.LightUpTiles(PositionList);
        
    }

    void spreadTheLight(Vector2Int startingPoint)
    {

    }

    Vector2Int[] GetCardinalDirectionsFromGameObject(GameObject looker)
    {
        Vector2Int startPoint = new Vector2Int((int) looker.transform.position.x, (int)looker.transform.position.z);

        Vector2Int[] NWES =
        {
            new Vector2Int(startPoint.x, startPoint.y+1),
            new Vector2Int(startPoint.x-1, startPoint.y),
            new Vector2Int(startPoint.x+1, startPoint.y),
            new Vector2Int(startPoint.x, startPoint.y-1)
        };

        return NWES;
    }

    Vector2Int[] GetCardinalDirectionsFromVector2Int(Vector2Int startPoint)
    {
        
        Vector2Int[] NWES =
        {
            new Vector2Int(startPoint.x, startPoint.y+1),
            new Vector2Int(startPoint.x-1, startPoint.y),
            new Vector2Int(startPoint.x+1, startPoint.y),
            new Vector2Int(startPoint.x, startPoint.y-1)
        };

        return NWES;
    }




}
