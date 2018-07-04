using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoom :Room 
{
    private bool _isMazeRoom;
    private bool _isBase;
    public bool isMazeRoom 
    {
        get 
        {
            return _isMazeRoom;
        }
        set 
        {
            _isMazeRoom = value;
        }
    }
    public bool isEndRoom
    {
        get
        {
            return _isBase;
        }
        set
        {
            _isBase = value;
        }
    }
    public BaseRoom(int x, int y): base(x, y)
    {
        isEndRoom = false;
    }
   
}
