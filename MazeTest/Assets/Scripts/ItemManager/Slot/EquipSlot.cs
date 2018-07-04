using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipSlot : Slot
{
    public override SlotType Type
    {
        get
        {
            return SlotType.Equipment;
        }
    }
 
    public override void  OnBeginDrag(PointerEventData eventData)
    {

    }
    public override void OnDrag(PointerEventData eventData)
    {

    }
    public override void OnEndDrag(PointerEventData eventData)
    {

    }

}
