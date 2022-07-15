using System.Collections;
using System.Collections.Generic;

public class TileData 
{
    public int x, y, tileType;
    public bool walkable, transparent, isLit, visible;


    
    public TileData(int x, int y, int tileType)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        AutoWalkTransparentSettingViaType(tileType);
        isLit = false;
        visible = false;
        
    }

    public void SetToVisible()
    {
        visible = true;
    }

    public void SetIsLit(bool litUp)
    {
        //if (transparent)
        //{
            isLit = litUp;
        //}
    }

    public void ChangeTileType(int tileType)
    {
        this.tileType = tileType;
        AutoWalkTransparentSettingViaType(tileType);
    }

    public void SetWalkableTransparentSettings(bool walkable, bool transparent)
    {
        this.walkable = walkable;
        this.transparent = transparent;
    }


    public void AutoWalkTransparentSettingViaType(int tileType)
    {
        switch (tileType)
        {
            case 0:
                this.walkable = false;
                this.transparent = false;
                break;
            case 1:
                this.walkable = false;
                this.transparent = false;
                break;
            case 2:
                this.walkable = true;
                this.transparent = true;
                break;
            default:
                this.walkable = false;
                this.transparent = false;
                break;
        }
    }



}
