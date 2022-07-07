
public class RoomData
{
    public int x, y, width, height;
    public WallData nWall, sWall, wWall, eWall;
    public RoomData(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;

        sWall = new WallData(x+1,y,width-1,0);
        wWall = new WallData(x,y+1,0,height-1);
        eWall = new WallData(x+width,y, 0,height-1);
        nWall = new WallData(x,y+height,width-1,0);

    }
}


public class WallData
{
    public int x, y, width, height;
    public WallData(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

}
