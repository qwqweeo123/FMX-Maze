using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo :RoleInfo
{
    public int Lv { get; set; }
    public int LvMax { get; set; }
    public int GoldNumber { get; set; }
    public double Exp { get; set; }
    public int HpGrowth { get; set; }
    public int MpGrowth { get; set; }
    public int AtkGrowth { get; set; }
    public int DefGrowth { get; set; }
    public int SpdGrowth { get; set; }
    public Equipment HelmetEquip { get; set; }
    public Equipment ArmorEquip { get; set; }
    public Equipment WeaponEquip { get; set; }
    public Equipment BootsEquip { get; set; }
    public Equipment DecorEquip { get; set; }
    public SkillItem SkillQ { get; set; }
    public SkillItem SkillW { get; set; }
    public SkillItem SkillE { get; set; }
    public SkillItem SkillR { get; set; }
    public List<Item> ItemList{ get; private set; }
    public Item[] PropList { get; private set; }
    public UserInfo()
    {
        this.ItemList = new List<Item>();
        this.Status = new AttributeRoot();
        this.PropList = new Item[8];
    }

}
