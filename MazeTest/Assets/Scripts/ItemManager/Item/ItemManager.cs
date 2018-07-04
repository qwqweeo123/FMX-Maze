using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private static ItemManager _instance;
    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ItemManager();
            return _instance;
        }
    }
    private ItemManager()
    {
        ParseItemJson();
    }
    private Dictionary<int, Item> itemDict;
    private Dictionary<int, Sprite> spriteDict;
    //解析物品json文件
    private void ParseItemJson()
    {
        if (itemDict == null)
        {
            itemDict = new Dictionary<int, Item>();
        }
        if (spriteDict == null)
        {
            spriteDict = new Dictionary<int, Sprite>();
        }
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/ItemsInfoJson");
        JSONObject j = new JSONObject(ta.text);
        Sprite sprite;
        foreach (JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), temp["type"].str);
            string description = temp["description"].str;
            int capacity = (int)temp["capacity"].n;
            float dropOdds = temp["dropOdds"].n;
            int buyPrice = (int)temp["buyPrice"].n;
            int sellPrice = (int)temp["sellPrice"].n;
            string spritePath = temp["spritePath"].str;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = (int)temp["hp"].n;
                    int mp = (int)temp["hp"].n;
                    Consumable cons = new Consumable(id, name, type, description, capacity, dropOdds, buyPrice, sellPrice, spritePath, hp, mp);
                    itemDict.Add(cons.Id, cons);
                    sprite = Resources.Load<Sprite>(cons.SpritePath);
                    spriteDict.Add(cons.Id, sprite);
                    break;
                case Item.ItemType.Equipment:
                    int damage = (int)temp["damage"].n;
                    int defence = (int)temp["defence"].n;
                    int health = (int)temp["health"].n;
                    int magic = (int)temp["magic"].n;
                    int speed = (int)temp["speed"].n;
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].str);
                    Equipment equip = new Equipment(id, name, type, description, capacity, dropOdds, buyPrice, sellPrice, spritePath, damage, defence, health, magic, speed, equipType);
                    itemDict.Add(equip.Id, equip);
                    sprite = Resources.Load<Sprite>(equip.SpritePath);
                    spriteDict.Add(equip.Id, sprite);
                    break;
                case Item.ItemType.Skill:
                    string animPath = temp["animationPath"].str;
                    string skillName = temp["skillName"].str;
                    int sDamage = (int)temp["damage"].n;
                    int Smagic = (int)temp["damage"].n;
                    float cd = temp["cd"].n;
                    Type sType = Type.GetType(skillName);
                    Skill curSkill = sType.Assembly.CreateInstance(skillName) as Skill;
                    SkillItem sItem = new SkillItem(id, name, type, description, capacity,dropOdds, buyPrice, sellPrice, spritePath, animPath, curSkill,sDamage,Smagic,cd);
                    itemDict.Add(sItem.Id, sItem);
                    sprite = Resources.Load<Sprite>(sItem.SpritePath);
                    spriteDict.Add(sItem.Id, sprite);
                    break;
            }
        }
        
    }
    public Item GetItemById(int id)
    {
        Item item;
        if (itemDict.TryGetValue(id, out item))
        {
            return item.Clone() as Item;
        }
        else
        {
            Debug.LogError("Item "+id+ " is not exist!");
            return null;
        }
    }
    public Sprite GetSpriteById(int id)
    {
        Sprite sprite;
        if (spriteDict.TryGetValue(id,out sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogError(GetItemById(id).Name + " sprite is not exist!");
            return null;
        }

    }
}
