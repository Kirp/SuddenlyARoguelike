using System.Collections;
using System.Collections.Generic;

public class TileData 
{
    public int x, y, tileType;
    public bool walkable, transparent, dark, light;

    
    public TileData(int x, int y, int tileType)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        dark = true;
        light = false;
    }

    public void SetWalkableTransparentSettings(bool walkable, bool transparent)
    {
        this.walkable = walkable;
        this.transparent = transparent;
    }



}
