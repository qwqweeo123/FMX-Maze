using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : Slot,IDropHandler,IPointerDownHandler
{
    public override Slot.SlotType Type
    {
        get
        {
            return Slot.SlotType.Skill;
        }
    }

    private Image coolUI;
    private SkillItem skillItem;
    private Timer skillTimer;
    void Awake()
    {
        base.Awake();
        coolUI = transform.Find("Cooldown").GetComponent<Image>();
    }
    void Start()
    {
        //m_RoleStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        //m_RoleStatus.SkillCool += CoolUIChange;
    }
    void FixedUpdate()
    {
        if (skillItem != null && skillItem.IsCooling)
        {
            if (skillItem.CurrentCD >skillItem.CD)
            {
                skillItem.CoolFinish();
                CoolUIChange();
            }
            else
            {
                skillItem.UpdateCD(Time.fixedDeltaTime);
            }
            coolUI.fillAmount = 1-skillItem.CurrentCD / skillItem.CD;
        }
    }
   
    public override void OnBeginDrag(PointerEventData eventData)
    {

    }
    public override void OnDrag(PointerEventData eventData)
    {

    }
    public override void OnEndDrag(PointerEventData eventData)
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        PlayerStatus m_RoleStatus = GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>();
        var obj = eventData.pointerDrag;
        Slot slot = obj.GetComponent<Slot>();
        if (slot.CurItem == null || slot.CurItem.Type != Item.ItemType.Skill||!m_RoleStatus.GetComponent<PlayerCtrl>().CurrStateMachine.IsInState((int)PlayerCtrl.PlayerState.idle))
        {
            return;
        }
        string sName = this.name;
        SkillItem sItem = slot.CurItem as SkillItem;
        switch (sName)
        {
            case "SkillQSlot":
                if (slot.Type == SlotType.Grid)
                {
                    m_RoleStatus.RemoveItem(slot);
                    if (m_RoleStatus.SkillQ != null)
                    {
                        m_RoleStatus.PickItem(this.CurItem);
                    }
                    m_RoleStatus.SkillQ = sItem;
                }
                break;
            case "SkillWSlot":
                if (slot.Type == SlotType.Grid)
                {
                    m_RoleStatus.RemoveItem(slot);
                    if (m_RoleStatus.SkillW != null)
                    {
                        m_RoleStatus.PickItem(this.CurItem);
                    }
                    m_RoleStatus.SkillW = sItem;
                }
                break;
            case "SkillESlot":
                if (slot.Type == SlotType.Grid)
                {
                    m_RoleStatus.RemoveItem(slot);
                    if (m_RoleStatus.SkillE != null)
                    {
                        m_RoleStatus.PickItem(this.CurItem);
                    }
                    m_RoleStatus.SkillE = sItem;
                }
                break;
            case "SkillRSlot":
                if (slot.Type == SlotType.Grid)
                {
                    m_RoleStatus.RemoveItem(slot);
                    if (m_RoleStatus.SkillR != null)
                    {
                        m_RoleStatus.PickItem(this.CurItem);
                    }
                    m_RoleStatus.SkillR = sItem;
                }
                break;
        }
    }
  

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerStatus m_RoleStatus = GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>();
        if (eventData.pointerId == -2)
        {
            if (CurItem == null||!m_RoleStatus.GetComponent<PlayerCtrl>().CurrStateMachine.IsInState((int)PlayerCtrl.PlayerState.idle))
            {
                return;
            }
            string sName = this.name;
            switch (sName)
            {
                case "SkillQSlot":
                    m_RoleStatus.PickItem(this.CurItem);
                    m_RoleStatus.SkillQ =null;
                    break;
                case "SkillWSlot":
                    m_RoleStatus.PickItem(this.CurItem);
                    m_RoleStatus.SkillW = null;
                    break;
                case "SkillESlot":
                    m_RoleStatus.PickItem(this.CurItem);
                    m_RoleStatus.SkillE = null;
                    break;
                case "SkillRSlot":
                    m_RoleStatus.PickItem(this.CurItem);
                    m_RoleStatus.SkillR = null;
                    break;
            }
        }
    }
    public override void SetItem(Item item)
    {
        curItem = item;
        skillItem=item as SkillItem;
        if (curItem == null)
        {
            curImage.sprite = null;
            curImage.gameObject.SetActive(false);
            coolUI.gameObject.SetActive(false);
        }
        else
        {
            curImage.sprite = ItemManager.Instance.GetSpriteById(curItem.Id);
            curImage.gameObject.SetActive(true);
            CoolUIChange();
        }
        AmountChange();
    }
    public override void AmountChange()
    {
        amountText.gameObject.SetActive(true);
    }
    public SkillSlot Init()
    {
        GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().SkillCool += CoolUIChange;
        return this;
    }
    private void CoolUIChange()
    {
        if (skillItem == null || !skillItem.IsCooling)
        {
            this.coolUI.gameObject.SetActive(false);
        }
        else if (skillItem.IsCooling)
        {
            this.coolUI.gameObject.SetActive(true);
        }
    }

}
