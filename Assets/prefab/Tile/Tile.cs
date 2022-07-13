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
    TileData tileData = null;
    bool visibility = false;


    public void LoadTileData(TileData tileData)
    {
        this.tileData = tileData;
    }

    public void UpdateAppearenceWithTileData()
    {
        if (tileData == null) return;
    }

    
    public void UpdateVisibility()
    {
        this.meshObj.gameObject.SetActive(visibility);
    }
    

    public void ChangeTileTo(int type)
    {
        
        switch (type)
        {
            case 0:
                
                meshRenderer.material = matStone;
                tileText.text = " ";
                //gameObject.SetActive(false);
                break;
            
            case 1:
                meshRenderer.material = matWall;
                tileText.text = "#";
                Vector3 posi = gameObject.transform.position;
                posi.y = 1f;
                gameObject.transform.position = posi;
                break;

            case 2:
                meshRenderer.material = matFloor;
                tileText.text = ".";
                break;
            default:
                //do nothing
                break;
        }
    }
}
