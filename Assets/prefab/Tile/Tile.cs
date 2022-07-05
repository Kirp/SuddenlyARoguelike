using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] Material matStone;
    [SerializeField] Material matWall;
    [SerializeField] Material matFloor;
    [SerializeField] Transform meshObj;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] TextMeshPro tileText;


    [SerializeField] bool isWalkable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTileTo(int type)
    {
        
        switch (type)
        {
            case 0:
                
                meshRenderer.material = matWall;
                tileText.text = " ";
                isWalkable = false;
                break;
            case 1:
                meshRenderer.material = matFloor;
                tileText.text = ".";
                break;
            case 2:
                meshRenderer.material = matWall;
                tileText.text = ".";
                break;
            default:
                //do nothing
                break;
        }
    }
}
