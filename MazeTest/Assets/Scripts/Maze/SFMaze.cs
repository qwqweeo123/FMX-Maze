using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFMaze : Maze
{
    private Direction dir;
    private List<BaseRoom> points;
    public bool isVisted(int x, int y)
    {
        if ((x > 0 && x < Width) && (y > 0 && y <Hight) && Map[x,y] == 0)
            return false;
        return true;
    }
    public bool isClosed(BaseRoom br)
    {
        if (br.x < 5 && br.y < 5)
        {
            return true;
        }
        if (map[br.x + 1, br.y + 1] == 1 || map[br.x + 1, br.y - 1] == 1 || map[br.x - 1, br.y + 1] == 1 || map[br.x - 1, br.y - 1] == 1)
        {
            return true;
        }
        if (Rooms.Count > 0)
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                if ((Rooms[i].x >= br.x - 4 && Rooms[i].x <= br.x + 4) && (Rooms[i].y >= br.y - 4 && Rooms[i].y <= br.y + 4))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void GenerateMaze(int width, int hight, BaseRoom op,int roomNumber) 
    {
        originPos = op;
        int index=0;
        points = new List<BaseRoom>();
        this.width = width;
        this.hight = hight;
        map = new int[width+1, hight+1];
        rooms = new List<BaseRoom>();
        map[op.x, op.y] = 1;
        points.Add(op);
        bool []b = { false, false, false, false };
        bool isCheck;
        while (points.Count > 0)
        {
            isCheck = false;
            dir = (Direction)Random.Range(0, 4);
            switch (dir)
            {
                case Direction.up:
                    if (!isVisted(points[index].x, points[index].y + 2))
                    {
                        map[points[index].x, points[index].y + 1] = 1;
                        map[points[index].x, points[index].y + 2] = 1;
                        points.Add(new BaseRoom(points[index].x, points[index].y + 2));
                        index++;
                        isCheck = true;
                    }
                        b[0]=true;
                    break;
                case Direction.down:
                    if (!isVisted(points[index].x, points[index].y - 2))
                    {
                        map[points[index].x, points[index].y - 1] = 1;
                        map[points[index].x, points[index].y - 2] = 1;
                        points.Add(new BaseRoom(points[index].x, points[index].y - 2));
                        index++;
                        isCheck = true;
                    }
                        b[1] = true;
                    break;
                case Direction.left:
                    if (!isVisted(points[index].x - 2, points[index].y))
                    {
                        map[points[index].x - 1, points[index].y] = 1;
                        map[points[index].x - 2, points[index].y] = 1;
                        points.Add(new BaseRoom(points[index].x - 2, points[index].y));
                        index++;
                        isCheck = true;
                    }
                        b[2] = true;
                    break;
                case Direction.right:
                    if (!isVisted(points[index].x + 2, points[index].y))
                    {
                        map[points[index].x+1, points[index].y] = 1;
                        map[points[index].x+2, points[index].y] = 1;
                        points.Add(new BaseRoom(points[index].x + 2, points[index].y));
                        index++;
                        isCheck = true;
                    }
                        b[3] = true;
                    break;
            }
            if (!isCheck)
            {
                if (b[0] && b[1] && b[2] && b[3])
                {
                    points.RemoveAt(index);
                    index--;
                    b[0] = b[1] = b[2] = b[3] = false;
                }
            }
            else 
            {
                b[0] = b[1] = b[2] = b[3] = false;
            }
        }
        BaseRoom room;
        while (roomNumber > 0)
        {
            int tx = Random.Range(1, Width - 1);
            int ty = Random.Range(1, Hight - 1);
            if (Map[tx, ty] == 1)
            {
                room = new BaseRoom(tx, ty);
                if (isClosed(room))
                {
                    roomNumber++;
                }
                else
                {
                    Rooms.Add(room);
                    Map[tx, ty] = 2;
                }
                roomNumber--;
            }

        }
        for (int i = 0; i < Rooms.Count; i++)
        {
            int number = 0;
            int curNumber =0;
            int x=Rooms[i].x,y=Rooms[i].y;

            if (Map[x + 1, y] == 1)
            {
                Map[x + 1, y] = 2;
            }
            if (Map[x - 1, y] == 1)
            {
                Map[x - 1, y] = 2;
            }
            if (Map[x, y+1] == 1)
            {
                Map[x, y + 1] = 2;
            }
            if (Map[x, y -1] == 1)
            {
                Map[x, y-1] = 2;
            }

            if (x < width-1 && Map[x + 2, y] == 0)
            {
                number++;
                curNumber = 1;
            }
            else if (x == width && Map[x + 1, y] == 0)
            {
                number++;
            }
            if (x > 1 && Map[x - 2, y] == 0)
            {
                number++;
                curNumber = 2;
            }
            else if (x == 1 && Map[x - 1, y] == 0)
            {
                number++;
            }
            if (y < hight-1 && Map[x, y + 2] == 0)
            {
                number++;
                curNumber=3;
            }
            else if (y == hight && Map[x , y+1] == 0)
            {
                number++;
            }
            if (y > 1 && Map[x, y - 2] == 0)
            {
                number++;
                curNumber = 4;
            }
            else if (y == 1 && Map[x , y-1] == 0)
            {
                number++;
            }
            if (number >= 2)
            {
                switch (curNumber)
                {
                    case 1:
                        Map[x + 2, y] = 1;
                        break;
                    case 2:
                        Map[x - 2, y] = 1;
                        break;
                    case 3:
                        Map[x, y + 2] = 1;
                        break;
                    case 4:
                        Map[x, y - 2] = 1;
                        break;
                }
            }
            
        }
        ChoseEndRoom();
    }
    private void ChoseEndRoom()
    {
        int distance=0;
        BaseRoom endRoom=new BaseRoom(0,0);
        foreach (BaseRoom room in Rooms)
        {
            int x = room.x; int y = room.y;
            int temp = x * x + y * y;
            if (temp > distance)
            {
                distance = temp;
                endRoom = room;
            }
        }
        if (endRoom != null)
        {
            endRoom.isEndRoom = true;
        }
    }

}
public enum Direction { up,down,left,right}
