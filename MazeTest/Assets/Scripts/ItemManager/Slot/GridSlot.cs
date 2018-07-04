using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSlot : Slot,IDropHandler,IPointerDownHandler
{
    public override SlotType Type
    {
        get
        {
            return SlotType.Grid;
        }
    }
    public  void OnDrop(PointerEventData eventData)
    {
        var obj = eventData.pointerDrag;
        Slot slot = obj.GetComponent<Slot>();
        if (slot.CurItem == null)
        {
            return;
        }
        else
        {
            if (CurItem != null)
            {
                if (slot.CurItem.Id == CurItem.Id && slot.gameObject != this.gameObject)
                {
                    if (CurItem.Count < CurItem.Capacity)
                    {
                        if (slot.CurItem.Count + CurItem.Count <= CurItem.Capacity)
                        {
                            CurItem.SetCount(CurItem.Count+slot.CurItem.Count);
                            AmountChange();
                            GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().RemoveItem(slot);
                        }
                        else
                        {
                            int delta = CurItem.Capacity - CurItem.Count;
                            CurItem.SetCount(CurItem.Capacity);
                            AmountChange();
                            slot.CurItem.SetCount(slot.CurItem.Count -  delta);
                            slot.AmountChange();
                        }
                    }
                }
                else
                {
                    SwitchItem(slot);
                }
            }
            else if(slot.Type==SlotType.Prop)
            {
                GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().PickItem(slot.CurItem);
                slot.SetItem(null);
            }
            else
            {
                SwitchItem(slot);
            }
        }
 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerId==-2&&CurItem!=null)
        {
            var player = GameObject.FindWithTag("Player");
            switch (CurItem.Type)
            {
                case Item.ItemType.Consumable:
                    Consumable con = CurItem as Consumable;
                    player.GetComponent<PlayerCtrl>().DrinkPotion(con);
                    AmountChange();
                    break;
                case Item.ItemType.Equipment:
                    Equipment equip = CurItem as Equipment;
                    player.GetComponent<PlayerStatus>().Equip(equip);
                    break;
            }
        }
    }
    public override void AmountChange()
    {
        if (CurItem == null)
        {
            amountText.gameObject.SetActive(false);
            return;
        }
        else if (CurItem.Count > 1)
        {
            amountText.text = CurItem.Count.ToString();
            amountText.gameObject.SetActive(true);

        }
        else if (CurItem.Count == 1)
        {
            amountText.gameObject.SetActive(false);
        }
        else if (CurItem.Count == 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().RemoveItem(this);
        }
    }
}
