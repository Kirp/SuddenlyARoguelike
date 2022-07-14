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

        //now light em up in the dungeonFloorManager
        dungeonFloorManager.LightUpTiles(PositionList);
        
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

    
}
