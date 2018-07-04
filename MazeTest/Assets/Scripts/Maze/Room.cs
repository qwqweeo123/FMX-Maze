using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private int _x, _y;
    public int x 
    {
        get 
        {
            return _x;
        }
    }
    public int y 
    {
        get 
        {
            return _y;
        }
    }
    public Room(int x, int y)
    {
        _x = x;
        _y = y;
    }
}
