using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Maze {

    internal int[,] map;
    internal BaseRoom originPos;
    internal BaseRoom terminal;
    internal int hight;
    internal int width;
    internal List<BaseRoom> rooms;

    public int [,] Map
    {
        get
        {
            return map;
        }
    }

    public BaseRoom OriginPos
    {
        get
        {
            return originPos;
        }
    }

    public BaseRoom Terminal 
    {
        get 
        {
            return terminal;
        }
    }

    public int Hight
    {
        get
        {
            return hight;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }
    public List<BaseRoom> Rooms 
    {
        get 
        {
            return rooms;
        }
    }

    public abstract void GenerateMaze(int width, int height, BaseRoom originPos,int roomNumber);
}
