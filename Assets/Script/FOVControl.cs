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

        //get the player position and then the immediate sides



    }

    
}
