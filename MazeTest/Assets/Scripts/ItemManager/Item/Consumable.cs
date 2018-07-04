using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Consumable : Item
{
    public AttributeNode StatusNode { get; set; }

    public Consumable(int id, string name, ItemType type, string description, int capacity, float dropOdds, int buyPrice, int sellPrice, string sprietPath, int hp, int mp)
        :base(id,name,type,description,capacity,dropOdds,buyPrice,sellPrice,sprietPath)
    {
        this.StatusNode = new AttributeNode();
        this.StatusNode.Hp = hp;
        this.StatusNode.Mp = mp;
        this.StatusNode.Calculate();
    }

}
