using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Material matWall;
    [SerializeField] Material matFloor;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTileTo(int type)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        switch (type)
        {
            case 0:
                
                meshRenderer.material = matWall;
                break;
            case 1:
                meshRenderer.material = matFloor;
                break;
            default:
                //do nothing
                break;
        }
    }
}
