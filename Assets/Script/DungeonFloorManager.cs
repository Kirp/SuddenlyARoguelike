using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorManager : MonoBehaviour
{

    DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = new DungeonGenerator(20,20);
        dungeonGenerator.GenerateDungeon();

        Debug.Log(dungeonGenerator.GetTileAt(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
