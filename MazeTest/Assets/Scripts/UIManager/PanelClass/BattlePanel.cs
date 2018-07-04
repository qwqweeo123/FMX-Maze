using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattlePanel :BasePanel,IPointerDownHandler
{
    private Text HPText;
    private Text MPText;
    private Text LvText;
    private Text ExpText;
    private Image HPValue;
    private Image MPValue;
    private Image ExpValue;
    private SkillSlot[] skillSlots;
    private SkillSlot SkillQSlot;
    private SkillSlot SkillWSlot;
    private SkillSlot SkillESlot;
    private SkillSlot SkillRSlot;
    private PropSlot[] propSlots;

    private PlayerStatus m_RoleStatus;

    void Update()
    {
        //Event e = Event.current;
        //if (e.isKey)
        //{
        //    int i = -(47 - (int)e.keyCode);
        //    if (i >= 1 || i <= 9)
        //    {
        //        GameManager.Instance.CurentPlayer.GetComponent<PlayerCtrl>().DrinkPotion(propSlots[i].CurItem as Consumable);
        //    }
        //}
        
    }
    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                int i = -(48 - (int)e.keyCode);
                if (i >= 1 && i <= 8)
                {
                    if (propSlots[i - 1].CurItem != null)
                    {
                        GameManager.Instance.CurentPlayer.GetComponent<PlayerCtrl>().DrinkPotion(propSlots[i - 1].CurItem as Consumable);
                        propSlots[i - 1].AmountChange();
                    }
                }
            }
        }
    }

    public void SwitchPanelState(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
  
    public override void OnEnter()
    {
        SetDisplay(true);
    }
    public override void OnInit()
    {
        InitWiget();
    }
    private void InitData()
    {
        this.HPText.text = String.Format("HP:{0}/{1}", m_RoleStatus.status.HpCur, m_RoleStatus.status.HpMax);
        this.MPText.text = String.Format("MP:{0}/{1}", m_RoleStatus.status.MpCur, m_RoleStatus.status.MpMax);
        this.HPValue.fillAmount = (float)m_RoleStatus.status.HpCur / m_RoleStatus.status.HpMax;
        this.MPValue.fillAmount = (float)m_RoleStatus.status.MpCur / m_RoleStatus.status.MpMax;
    }
    private void InitWiget()
    {
        this.HPText = transform.Find("Left Globe/HPText").GetComponent<Text>();
        this.MPText = transform.Find("Right Globe/MPText").GetComponent<Text>();
        this.LvText = transform.Find("Lv UI/LvText").GetComponent<Text>();
        this.ExpText = transform.Find("EXP Bar/ExpText").GetComponent<Text>();
        this.HPValue = transform.Find("Left Globe/Fill").GetComponent<Image>();
        this.MPValue = transform.Find("Right Globe/Fill").GetComponent<Image>();
        this.ExpValue = transform.Find("EXP Bar/Active").GetComponent<Image>();
        this.SkillQSlot = transform.Find("Slots Background/SkillQSlot").GetComponent<SkillSlot>().Init();
        this.SkillWSlot = transform.Find("Slots Background/SkillWSlot").GetComponent<SkillSlot>().Init();
        this.SkillESlot = transform.Find("Slots Background/SkillESlot").GetComponent<SkillSlot>().Init();
        this.SkillRSlot = transform.Find("Slots Background/SkillRSlot").GetComponent<SkillSlot>().Init();
        propSlots = new PropSlot[8];
        for (int i = 0; i < 8; i++)
        {
            propSlots[i]=transform.Find("Prop Slots").GetChild(i).GetComponent<PropSlot>();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_RoleStatus = player.GetComponent<PlayerStatus>();
        m_RoleStatus.StatusChange +=BattleStatusChange;
        m_RoleStatus.BattleSlotChange += SkillIconChange;
        m_RoleStatus.LvChange += lvChange;
        m_RoleStatus.ExpChange += expUIChange;
        BattleStatusChange();
        SkillIconChange();
        lvChange();
        expUIChange();
        PropIconChange();
    }
    private void BattleStatusChange()
    {
        InitData();
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    var obj = eventData.pointerDrag;
    //    Slot slot = obj.GetComponent<Slot>();
    //    Debug.Log(eventData.pointerCurrentRaycast.gameObject);
    //    Slot curSlot=eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();
    //    if (slot.CurItem == null||slot.CurItem.Type!=Item.ItemType.Skill||curSlot==null)
    //    {
    //        return;
    //    }
    //    string sName = curSlot.name;
    //    SkillItem sItem=slot.CurItem as SkillItem;
    //    switch (sName)
    //    {
    //        case "SkillQSlot":
    //            m_RoleStatus.RemoveItem(slot);
    //            if (m_RoleStatus.SkillQ != null)
    //            {
    //                m_RoleStatus.PickItem(curSlot);
    //            }
    //            m_RoleStatus.SkillQ = sItem;
    //            break;
    //        case "SkillWSlot":
    //            m_RoleStatus.RemoveItem(slot);
    //            if (m_RoleStatus.SkillW != null)
    //            {
    //                m_RoleStatus.PickItem(curSlot);
    //            }
    //            m_RoleStatus.SkillW = sItem;
    //            break;
    //        case "SkillESlot":
    //            m_RoleStatus.RemoveItem(slot);
    //            if (m_RoleStatus.SkillE != null)
    //            {
    //                m_RoleStatus.PickItem(curSlot);
    //            }
    //            m_RoleStatus.SkillE = sItem;
    //            break;
    //        case "SkillRSlot":
    //            m_RoleStatus.RemoveItem(slot);
    //            if (m_RoleStatus.SkillR != null)
    //            {
    //                m_RoleStatus.PickItem(curSlot);
    //            }
    //            m_RoleStatus.SkillR = sItem;
    //            break;
    //    }
    //}
    private void SkillIconChange()
    {
        
        this.SkillQSlot.SetItem(m_RoleStatus.SkillQ);
        this.SkillWSlot.SetItem(m_RoleStatus.SkillW);
        this.SkillESlot.SetItem(m_RoleStatus.SkillE);
        this.SkillRSlot.SetItem(m_RoleStatus.SkillR);
    }
    private void PropIconChange()
    {
        for (int i = 0; i < 8; i++)
        {
            propSlots[i].SetItem(GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().PropList[i]);
        }
    }
    private void lvChange()
    {
        this.LvText.text = m_RoleStatus.Lv.ToString();
    }
    private void expUIChange()
    {
        this.ExpText.text = String.Format("Exp:{0}/{1}", (int)m_RoleStatus.Exp,(int)m_RoleStatus.ExpMax);
        this.ExpValue.fillAmount = (float)m_RoleStatus.Exp / (float)m_RoleStatus.ExpMax;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerId);
    }
}
