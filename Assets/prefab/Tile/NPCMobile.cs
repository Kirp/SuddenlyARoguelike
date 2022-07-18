using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCMobile : MonoBehaviour
{
    [SerializeField] TextMeshPro tileText;
    TileData tileData = null;
    bool visibility = false;
    bool isMoving = false;
    [SerializeField] DungeonFloorManager dfm;

    // Start is called before the first frame update
    void Start()
    {
        dfm = FindObjectOfType<DungeonFloorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeTurn()
    {

    }

    IEnumerator MoveStep(Vector3 direction)
    {


        var oldLocation = transform.position;
        var newLocation = transform.position + direction;
        if (dfm.IsValidMoveToTile((int)newLocation.x, (int)newLocation.z))
        {
            isMoving = true;
            float currentMoveTime = 0f;


            while (currentMoveTime < 1f)
            {
                currentMoveTime += Time.deltaTime * 10;
                transform.position = Vector3.Lerp(oldLocation, newLocation, currentMoveTime);

                yield return new WaitForEndOfFrame();
            }
            isMoving = false;
            dfm.PlayerCallFOV();
        }
    }
}
