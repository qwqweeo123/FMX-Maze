using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PropSlot : Slot,IDropHandler
{
    public override SlotType Type
    {
        get
        {
            return SlotType.Grid;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var obj = eventData.pointerDrag;
        Slot slot = obj.GetComponent<Slot>();
        if (slot.CurItem == null||slot.CurItem.Type!=Item.ItemType.Consumable)
        {
            return;
        }
        else
        {
            int index = transform.GetSiblingIndex();
            GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().PropList[index] = slot.CurItem;
            SetItem(slot.CurItem);
            GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().RemoveItem(slot);
        }

    }
    public override void SetItem(Item item)
    {
        base.SetItem(item);
        int index = transform.GetSiblingIndex();
        GameManager.Instance.CurentPlayer.GetComponent<PlayerStatus>().PropList[index] =item;

    }
}
