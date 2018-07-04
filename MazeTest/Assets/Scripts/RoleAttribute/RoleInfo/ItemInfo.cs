using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo 
{
    public int ItemID{get;private set;}
    public int ItemCount{get;private set;}

    public ItemInfo(int id,int count)
    {
        this.ItemID=id;
        this.ItemCount=count;
    }
}
