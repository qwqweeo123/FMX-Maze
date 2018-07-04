using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPanel : BasePanel
{
    private PlayerStatus m_RoleStatus;
    private List<Item> itemList;
    private List<BoxSlot> slotList;
    private List<BoxSlot> selectSlotList;

    void Awake()
    {
        base.Awake();
        
    }
    public override void OnEnter(object param)
    {
        SetDisplay(true);
        itemList = param as List<Item>;
        for (int i = 0; i < slotList.Count;i++)
        {
            if (itemList.Count>i)
            {
                slotList[i].SetItem(itemList[i]);
            }
            else
            {
                slotList[i].SetItem(null);
            }
        }
        FreshPanel();
    }
    public override void OnResume()
    {
        UIManager.Instance.RemovePanel(UIPanelType.BoxPanel);
    }
    public override void OnExit()
    {
        FreshPanel();
        SetDisplay(false);
    }
    public override void OnInit()
    {
        InitWiget();
    }

    void InitWiget()
    {
        slotList = new List<BoxSlot>();
        selectSlotList = new List<BoxSlot>();
        GameObject obj = transform.Find("Item Grid").gameObject;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            slotList.Add(obj.transform.GetChild(i).GetComponent<BoxSlot>());
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_RoleStatus = player.GetComponent<PlayerStatus>();
    }
    void FreshPanel()
    {
        foreach (BoxSlot slot in slotList)
        {
            slot.Fresh();
        }
        selectSlotList.Clear();
    }
    public void SelectSlot(BoxSlot slot)
    {
        selectSlotList.Add(slot);
    }
    public void RemoveSlot(BoxSlot slot)
    {
        selectSlotList.Remove(slot);
    }
    public void AddItemsToKnap()
    {
        if (selectSlotList.Count <1)
        {
            return;
        }
        foreach (BoxSlot slot in selectSlotList)
        {
            m_RoleStatus.PickItem(slot.CurItem);
            itemList.Remove(slot.CurItem);
            slot.SetItem(null);
        }
        FreshPanel();
    }
    public void ClosePanel()
    {
        UIManager.Instance.RemovePanel(UIPanelType.BoxPanel);
    }
}
