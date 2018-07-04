using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KnapsackPanel :BasePanel
{
    //背包物品格列表
    private List<GridSlot> itemSlotList;
    //玩家属性类索引
    private PlayerStatus m_RoleStatus;
    //金币UI
    private Text GoldNumtext;
    void Start()
    {
    }
    public override void OnEnter()
    {
        SetDisplay(true);
        transform.SetAsLastSibling();
    }
    public override void OnPause()
    {
    }

    public override void OnResume()
    {
        SetDisplay(!IsDisplay);
        transform.SetAsLastSibling();
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
    public override void OnInit()
    {
        InitWiget();
    }
    public void OnClosePanel()
    {
        UIManager.Instance.ClosePanel(UIPanelType.KnapsackPanel);
    }
    private void InitWiget()
    {
        GameObject obj = transform.Find("Slots Grid").gameObject;
        GoldNumtext = transform.Find("Currencies/Gold").GetComponent<Text>();
        itemSlotList=new List<GridSlot>();
        for (int i = 0; i < obj.transform.childCount;i++)
        {
            itemSlotList.Add(obj.transform.GetChild(i).transform.Find("Item Slot").GetComponent<GridSlot>());
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_RoleStatus = player.GetComponent<PlayerStatus>();

        for (int i = 0; i < m_RoleStatus.knapItemList.Count; i++)
        {
            itemSlotList[i].SetItem(m_RoleStatus.knapItemList[i]);
        }

        m_RoleStatus.SlotChange += KnapItemChange;
        m_RoleStatus.GoldChange += GoldUIchange;
        GoldUIchange();
    }
    private void KnapItemChange()
    {
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (i < m_RoleStatus.knapItemList.Count)
            {
                itemSlotList[i].SetItem(m_RoleStatus.knapItemList[i]);
            }
            else
            {
                itemSlotList[i].SetItem(null);
            }
        }
    }
    //private void KnapsackStore(Item item)
    //{
    //    foreach (Slot slot in itemSlotList)
    //    {
    //        if (slot.CurItem!=null&&slot.CurItem.Id == item.Id && slot.CurItem.Count < slot.CurItem.Capacity)
    //        {
    //            int delta = slot.CurItem.Capacity - slot.CurItem.Count;
    //            Debug.Log(delta);
    //            Debug.Log(item.Count);
    //            if (item.Count <= delta)
    //            {
    //                slot.CurItem.SetCount(slot.CurItem.Count + item.Count);
    //                slot.AmountChange();
    //                m_RoleStatus.knapItemList.Remove(item);
    //                return;
    //            }
    //            else
    //            {
    //                item.SetCount(item.Count-delta);
    //                slot.CurItem.SetCount(slot.CurItem.Capacity);
    //                slot.AmountChange();
    //            }
    //        }
    //    }
    //    foreach (Slot slot in itemSlotList)
    //    {
    //        if (slot.CurItem == null)
    //        {
    //            slot.SetItem(item);
    //            return;
    //        }
    //    }
    //}
    private void GoldUIchange()
    {
        this.GoldNumtext.text = m_RoleStatus.GoldNumber.ToString();
    }
}
