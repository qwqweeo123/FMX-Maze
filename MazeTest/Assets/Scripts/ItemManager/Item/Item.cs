using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class Item:ICloneable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int Count { get; set; }
    public float DropOdds { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string SpritePath { get; set; }

    public Item(int id, string name, ItemType type, string description, int capacity,float dropOdds, int buyPrice, int sellPrice,string spritePath)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
        this.Description = description;
        this.Capacity = capacity;
        this.DropOdds = dropOdds;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.SpritePath = spritePath;
    }
    public void SetCount(int count)
    {
        if (count < 0)
        {
            return;
        }
        this.Count = count;
    }
    public object Clone()
    {
        return this.MemberwiseClone();
    }

   public enum ItemType
    {
        Consumable,
        Equipment,
        Skill
    }
}
