using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;

public class PlayerStatus : MonoBehaviour 
{

    private AnimationManager animMgr;

    public int m_StartExp = 40;
    public string Name { get; set; }
    public int GoldNumber { get;private set; }
    private int m_lv;
    public int Lv { get { return m_lv; } set { SetLv(value); } }
    public int LvMax { get; set; }
    private double m_exp;
    public double Exp { get { return m_exp; } set { m_exp = value; if (ExpChange != null)ExpChange(); } }
    private double m_expMax;
    public double ExpMax { get { return m_expMax; } set { m_expMax = value; if (ExpChange!=null)ExpChange(); } }
 #region 装备属性
    public Equipment HelmetEquip { get; set; }
    public Equipment ArmorEquip { get; set; }
    public Equipment WeaponEquip { get; set; }
    public Equipment BootsEquip { get; set; }
    public Equipment DecorEquip { get; set; }
    #endregion
    #region 技能属性
    private SkillItem m_SkillQ;
    private SkillItem m_SkillW;
    private SkillItem m_SkillE;
    private SkillItem m_SkillR;
    public SkillItem SkillQ 
    {
        get
        {
            return m_SkillQ;
        }
        set
        {
            this.m_SkillQ = value;
            if (this.m_SkillQ != null)
            {
                this.animMgr.AddAnimMotion(this.animMgr.GetState("SkillQ"), m_SkillQ.AnimationPath);
            }
            else
            {
                this.animMgr.DeleteMotion(this.animMgr.GetState("SkillQ"));
            }
            if (BattleSlotChange != null)
            BattleSlotChange();
        } 
    }
    public SkillItem SkillW
    {
        get
        {
            return m_SkillW;
        }
        set
        {
            this.m_SkillW = value;
            if (this.m_SkillW != null)
            {
                this.animMgr.AddAnimMotion(this.animMgr.GetState("SkillW"), m_SkillW.AnimationPath);
            }
            else
            {
                this.animMgr.DeleteMotion(this.animMgr.GetState("SkillW"));
            }
            if (BattleSlotChange != null)
            BattleSlotChange();
        }
    }
    public SkillItem SkillE
    {
        get
        {
            return m_SkillE;
        }
        set
        {
            this.m_SkillE = value;
            if (this.m_SkillE != null)
            {
                this.animMgr.AddAnimMotion(this.animMgr.GetState("SkillE"), m_SkillE.AnimationPath);
            }
            else
            {
                this.animMgr.DeleteMotion(this.animMgr.GetState("SkillE"));
            }
            if(BattleSlotChange!=null)
            BattleSlotChange();
        }
    }
    public SkillItem SkillR
    {
        get
        {
            return m_SkillR;
        }
        set
        {
            this.m_SkillR = value;
            if (this.m_SkillR != null)
            {
                this.animMgr.AddAnimMotion(this.animMgr.GetState("SkillR"), m_SkillR.AnimationPath);
            }
            else
            {
                this.animMgr.DeleteMotion(this.animMgr.GetState("SkillR"));
            }
            if (BattleSlotChange != null)
            BattleSlotChange();
        }
    }
    #endregion
    //道具
    public Item[] PropList;

    public List<Item> knapItemList{ get;private set; }

    public double m_MultipleExp = 1.18;

    public AttributeNode statusGrowth = new AttributeNode();
    public AttributeNode statusEquip = new AttributeNode();
    public AttributeNode statusBuff = new AttributeNode();
    public AttributeRoot status;
    public event Action SkillCool;
    public event Action StatusChange;
    public event Action EquipChange;
    public event Action BattleSlotChange;
    public event Action GoldChange;
    public event Action LvChange;
    public event Action ExpChange;
    public event Action SlotChange;

    void Awake()
    {
        Load();
    }
    void Update()
    {
        BuffManager.Instance.UpdateBuff(Time.deltaTime);
    }
    private void InitAllStatus()
    {
        InitStatusGrowth();
        InitStatus();
    }

    private void InitStatusGrowth()
    {
        UserInfo user = GameManager.Instance.PlayerInfo;
        this.statusGrowth.Hp = this.Lv * user.HpGrowth;
        this.statusGrowth.Mp = this.Lv * user.MpGrowth;
        this.statusGrowth.Atk = this.Lv * user.AtkGrowth;
        this.statusGrowth.Def = this.Lv * user.DefGrowth;
        this.statusGrowth.Spd = this.Lv * user.SpdGrowth;

        this.statusGrowth.Calculate();
    }
    private void InitStatus()
    {
        UserInfo user = GameManager.Instance.PlayerInfo;
        this.status = user.Status;
        this.status.AddNode(this.statusGrowth);
        this.status.AddNode(this.statusEquip);
        this.status.AddNode(this.statusBuff);
        this.status.Calculate(true);
    }
    public void Load()
    {
        UserInfo user = GameManager.Instance.PlayerInfo;
        status=user.Status;
        this.animMgr = new AnimationManager(GetComponent<Animator>());

        if (!GameManager.Instance.isInit)
        {
            TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/UserInfoJson");
            JSONObject j = new JSONObject(ta.text);
            user.Lv = (int)j["Lv"].n;
            user.Name = j["Name"].str;
            user.Exp = (double)j["Exp"].n;
            user.LvMax = (int)j["LvMax"].n;
            user.GoldNumber = (int)j["GoldNumber"].n;
            user.Status.Hp = (int)j["HP"].n;
            user.Status.Mp = (int)j["MP"].n;
            user.Status.Atk = (int)j["Atk"].n;
            user.Status.Def = (int)j["Def"].n;
            user.Status.Spd = (int)j["Spd"].n;
            user.HpGrowth = (int)j["HpGrowth"].n;
            user.MpGrowth = (int)j["MpGrowth"].n;
            user.AtkGrowth = (int)j["AtkGrowth"].n;
            user.DefGrowth = (int)j["DefGrowth"].n;
            user.SpdGrowth = (int)j["SpdGrowth"].n;

            foreach (JSONObject temp in j["ItemList"].list)
            {
                string key = temp.keys[0];
                ItemInfo info = new ItemInfo(int.Parse(key), (int)temp[key].n);
                Item item = ItemManager.Instance.GetItemById(info.ItemID);
                item.SetCount(info.ItemCount);
                user.ItemList.Add(item);
            }
            knapItemList = user.ItemList;
            PropList = user.PropList;
            this.Lv = user.Lv;
            this.LvMax = user.LvMax;
            this.Name = user.Name;
            this.Exp = user.Exp;
            this.GoldNumber = user.GoldNumber;
            this.ExpMax = m_StartExp * m_MultipleExp * this.Lv;
            InitAllStatus();
            GameManager.Instance.isInit = true;
            InitUI();
        }
        else
        {
            knapItemList = user.ItemList;
            PropList = user.PropList;
            this.Lv = user.Lv;
            this.LvMax = user.LvMax;
            this.Name = user.Name;
            this.Exp = user.Exp;
            this.GoldNumber = user.GoldNumber;
            this.ExpMax = m_StartExp * m_MultipleExp * this.Lv;
            InitEquip();
            InitUI();
        }
    }
    public void Equip(Equipment equipItem)
    {
        switch (equipItem.EquipType)
        {
            case Equipment.EquipmentType.Armor:
                if (ArmorEquip != null)
                {
                    statusEquip.RemoveNode(ArmorEquip.StatusNode);
                    PickItem(ArmorEquip);
                }
                this.ArmorEquip = equipItem;
                statusEquip.AddNode(this.ArmorEquip.StatusNode);
                break;
            case Equipment.EquipmentType.Boots:
                if (this.BootsEquip != null)
                {
                    statusEquip.RemoveNode(BootsEquip.StatusNode);
                    PickItem(BootsEquip);
                }
                this.BootsEquip = equipItem;
                statusEquip.AddNode(this.BootsEquip.StatusNode);
                break;
            case Equipment.EquipmentType.Decoration:
                if (this.DecorEquip != null)
                {
                    statusEquip.RemoveNode(DecorEquip.StatusNode);
                    PickItem(DecorEquip);
                }
                this.DecorEquip = equipItem;
                statusEquip.AddNode(this.DecorEquip.StatusNode);
                break;
            case Equipment.EquipmentType.Helmet:
                if (this.HelmetEquip != null)
                {
                    statusEquip.RemoveNode(HelmetEquip.StatusNode);
                    PickItem(HelmetEquip);
                }
                this.HelmetEquip = equipItem;
                statusEquip.AddNode(this.HelmetEquip.StatusNode);
                break;
            case Equipment.EquipmentType.Weapon:
                if (this.WeaponEquip != null)
                {
                    statusEquip.RemoveNode(WeaponEquip.StatusNode);
                    PickItem(WeaponEquip);
                }
                this.WeaponEquip = equipItem;
                statusEquip.AddNode(this.WeaponEquip.StatusNode);
                break;
        }
        RemoveItem(equipItem);
        status.Calculate();
        EquipChange();
        StatusChange();
    }
    public void UnloadEquip(Equipment.EquipmentType eType)
    {
        switch (eType)
        {
            case Equipment.EquipmentType.Armor:
                if (ArmorEquip != null)
                {
                    statusEquip.RemoveNode(ArmorEquip.StatusNode);
                    PickItem(ArmorEquip);
                    ArmorEquip = null;
                }
                break;
            case Equipment.EquipmentType.Boots:
                if (BootsEquip != null)
                {
                    statusEquip.RemoveNode(BootsEquip.StatusNode);
                    PickItem(BootsEquip);
                    BootsEquip = null;
                }
                break;
            case Equipment.EquipmentType.Decoration:
                if (DecorEquip != null)
                {
                    statusEquip.RemoveNode(DecorEquip.StatusNode);
                    PickItem(DecorEquip);
                    DecorEquip = null;
                }
                break;
            case Equipment.EquipmentType.Helmet:
                if (HelmetEquip != null)
                {
                    statusEquip.RemoveNode(HelmetEquip.StatusNode);
                    PickItem(HelmetEquip);
                    HelmetEquip = null;
                }
                break;
            case Equipment.EquipmentType.Weapon:
                if (WeaponEquip != null)
                {
                    statusEquip.RemoveNode(WeaponEquip.StatusNode);
                    PickItem(WeaponEquip);
                    WeaponEquip = null;
                }
                break;
        }
        EquipChange();
        status.Calculate();
        StatusChange();
    }
    public bool PickItem(Item item)
    {
        int space=item.Count/item.Capacity+1;
        if (space > 56 - knapItemList.Count)
        {
            return false;
        }
        int itemCount = item.Count;
        Item temp;
        while (itemCount > item.Capacity)
        {
            temp = ItemManager.Instance.GetItemById(item.Id);
            temp.SetCount(item.Capacity);
            knapsackAddItem(temp);
            SlotChange();
            itemCount = itemCount - item.Capacity;
        }
        temp = ItemManager.Instance.GetItemById(item.Id);
        temp.SetCount(itemCount);
        knapsackAddItem(temp);
        SlotChange();
        return true;
    }
    public void RemoveItem(Item item)
    {
        knapItemList.Remove(item);
        SlotChange();
    }
    public void RemoveItem(Slot itemSlot)
    {
        knapItemList.Remove(itemSlot.CurItem);
        itemSlot.SetItem(null);
    }
    public bool BuyItem(ShopSlot slot)
    {
        Item item = slot.CurItem;
        int buyPrice = item.BuyPrice * item.Count;

        if (this.GoldNumber >= buyPrice&&knapItemList.Count<56)
        {
            this.GoldNumber -= buyPrice;
            PickItem(item);
            GoldChange();
            return true;
        }
        else
        {
            return false;
        }
       
    }
    public void StatusChanged()
    {
        StatusChange();
    }
    public void SkillCooling()
    {
        SkillCool();
    }
    //初始化敌人池
    private void InitEnemyPool()
    {
        PlayerCtrl ctrl = GetComponent<PlayerCtrl>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objs)
        {
            ctrl.EnemyPool.Add(obj);
        }
        GameObject[] bObjs = GameObject.FindGameObjectsWithTag("EnemyBoss");
        foreach (GameObject obj in objs)
        {
            ctrl.EnemyPool.Add(obj);
        }
    }
    //设置等级
    public void SetLv(int lv)
    {
        if (lv > LvMax&&LvMax>0)
        {
            return;
        }
        m_lv = lv;
        this.ExpMax = m_StartExp * m_MultipleExp * this.Lv;
        InitStatusGrowth();
        status.Calculate();
        if (LvChange != null && StatusChange != null)
        {
            LvChange();
            StatusChange();
        }
    }
    //回满状态
    public void RecoverStatus()
    {
        status.Calculate(true);
        StatusChange();
    }
    //使用消耗品
    public void ConsumeItem(Consumable con)
    {
        con.SetCount(con.Count - 1);
        status.AddOnceNode(con.StatusNode);
        StatusChange();
    }
 
    //初始化UI
    private void InitUI()
    {
        UIManager.Instance.PushPanel(UIPanelType.BattlePanel);
        UIManager.Instance.PushPanel(UIPanelType.KnapsackPanel);
        UIManager.Instance.PushPanel(UIPanelType.AttributePanel);
        UIManager.Instance.RemovePanel(UIPanelType.KnapsackPanel);
        UIManager.Instance.RemovePanel(UIPanelType.AttributePanel);
        UIManager.Instance.PushPanel(UIPanelType.MiniMapPanel);
    }
    //初始化装备
    private void InitEquip()
    {
        UserInfo user = GameManager.Instance.PlayerInfo;
        HelmetEquip = user.HelmetEquip;
        ArmorEquip = user.ArmorEquip;
        WeaponEquip = user.WeaponEquip;
        BootsEquip = user.BootsEquip;
        DecorEquip = user.DecorEquip;

        SkillQ = user.SkillQ;
        SkillW = user.SkillW;
        SkillE = user.SkillE;
        SkillR = user.SkillR;
    }
    //背包列表添加物品
    private void knapsackAddItem(Item item)
    {
        foreach (Item temp in knapItemList)
        {
            if (temp != null && temp.Id == item.Id && temp.Count < temp.Capacity)
            {
                int delta = temp.Capacity - temp.Count;
                if (item.Count <= delta)
                {
                    temp.SetCount(temp.Count + item.Count);
                    return;
                }
                else
                {
                    item.SetCount(item.Count - delta);
                    temp.SetCount(temp.Capacity);
                }
                Debug.Log(knapItemList.Count);
            }
        }
        knapItemList.Add(item);
    }
    //保存信息
    public void SavePlayerInfo()
    {
        UserInfo user = GameManager.Instance.PlayerInfo;
        user.Lv = this.Lv;
        user.LvMax = this.LvMax;
        user.Name = this.Name;
        user.Exp = this.Exp;

        user.GoldNumber=this.GoldNumber;

        user.HelmetEquip = HelmetEquip;
        user.ArmorEquip = ArmorEquip;
        user.WeaponEquip = WeaponEquip;
        user.BootsEquip = BootsEquip;
        user.DecorEquip = DecorEquip;

        user.SkillQ = SkillQ;
        user.SkillW = SkillW;
        user.SkillE = SkillE;
        user.SkillR = SkillR;
    }
}
