using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileTile : MonoBehaviour
{

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
        Vector3 dPad = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
        
        if (dPad.x != 0 || dPad.z!=0)
        {
            if(!isMoving) StartCoroutine(MoveStep(dPad));
        }
    }

    IEnumerator MoveStep(Vector3 direction)
    {
        
        
        var oldLocation = transform.position;
        var newLocation = transform.position + direction;
        if(dfm.IsValidMoveToTile((int) newLocation.x,(int)newLocation.z))
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
        }
        

        
        
    }
}
