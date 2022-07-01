using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileTile : MonoBehaviour
{
    Vector3 dPadDetector = Vector3.zero;
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
        dPadDetector.x = Input.GetAxisRaw("Horizontal");
        dPadDetector.z = Input.GetAxisRaw("Vertical");
        
        if (dPadDetector.x != 0 || dPadDetector.z!=0)
        {
            //No diagonals yo
            if (dPadDetector.x != 0 && dPadDetector.z != 0) return;

            if(!isMoving) StartCoroutine(MoveStep(dPadDetector));
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
