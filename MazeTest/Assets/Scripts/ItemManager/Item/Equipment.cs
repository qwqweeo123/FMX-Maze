using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Equipment : Item
{
    public int Damege { get; set; }
    public int Defence { get; set; }
    public int Health { get; set; }
    public int Magic { get; set; }
    public int Speed { get; set; }
    public EquipmentType EquipType { get; set; }
    public AttributeNode StatusNode { get; set; }
    public Equipment(int id, string name, ItemType type, string description, int capacity, float dropOdds, int buyPrice, int sellPrice, string spritePath, int damage, int defence, int health, int magic, int speed, EquipmentType eType)
        : base(id, name, type, description, capacity, dropOdds, buyPrice, sellPrice,spritePath)
    {
        this.Damege = damage;
        this.Defence = defence;
        this.Health = health;
        this.Magic = magic;
        this.Speed = speed;
        this.EquipType = eType;
        StatusNode = new AttributeNode();
        this.StatusNode.Hp = health;
        this.StatusNode.Mp = magic;
        this.StatusNode.Atk = damage;
        this.StatusNode.Def = defence;
        this.StatusNode.Spd = speed;
        this.StatusNode.Calculate();
    }
    public enum EquipmentType
    {
        Weapon,
        Helmet,
        Armor,
        Boots,
        Decoration
    }
}
