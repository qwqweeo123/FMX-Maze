using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EquipmenBar : MonoBehaviour,IDropHandler,IPointerDownHandler
{
    private EquipSlot ArmorSlot;
    private EquipSlot BootsSlot;
    private EquipSlot DecorSlot;
    private EquipSlot HelmetSlot;
    private EquipSlot WeaponSlot;

    private PlayerStatus m_RoleStatus;
    public void OnDrop(PointerEventData eventData)
    {
        var obj = eventData.pointerDrag;
        Slot slot = obj.GetComponent<Slot>();
        Item item = slot.CurItem;
        if (item == null || item.Type != Item.ItemType.Equipment)
        {
            return;
        }
        else
        {
            Equipment equip = item as Equipment;
            EquipSlot eSlot = GetEquipSlot(equip.EquipType);
            if (eSlot.CurItem != null)
            {
                m_RoleStatus.UnloadEquip((eSlot.CurItem as Equipment).EquipType);
            }

            m_RoleStatus.Equip(equip);
            m_RoleStatus.RemoveItem(slot); 
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            GameObject obj = eventData.pointerPressRaycast.gameObject;
            EquipSlot slot = obj.GetComponent<EquipSlot>();
            if (slot == null||slot.CurItem==null)
            {
                return;
            }
            m_RoleStatus.UnloadEquip((slot.CurItem as Equipment).EquipType);
        }
        else
        {
            return;
        }
    }
    public void EquipBarInit()
    {
        ArmorSlot = transform.Find("Equip Slot(Clothes)").GetComponent<EquipSlot>();
        BootsSlot = transform.Find("Equip Slot(Shoes)").GetComponent<EquipSlot>();
        DecorSlot = transform.Find("Equip Slot(Ornament)").GetComponent<EquipSlot>();
        HelmetSlot = transform.Find("Equip Slot(Head)").GetComponent<EquipSlot>();
        WeaponSlot = transform.Find("Equip Slot(Weapon)").GetComponent<EquipSlot>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_RoleStatus = player.GetComponent<PlayerStatus>();
        m_RoleStatus.EquipChange += EquipmentIconChange;
        EquipmentIconChange();

    }
    void EquipmentIconChange()
    {
        this.ArmorSlot.SetItem(m_RoleStatus.ArmorEquip);
        this.BootsSlot.SetItem(m_RoleStatus.BootsEquip);
        this.DecorSlot.SetItem(m_RoleStatus.DecorEquip);
        this.HelmetSlot.SetItem(m_RoleStatus.HelmetEquip);
        this.WeaponSlot.SetItem(m_RoleStatus.WeaponEquip);
    }
    EquipSlot GetEquipSlot(Equipment.EquipmentType eType)
    {
        switch (eType)
        {
            case Equipment.EquipmentType.Armor:
                return ArmorSlot;
            case Equipment.EquipmentType.Boots:
                return BootsSlot;
            case Equipment.EquipmentType.Decoration:
                return DecorSlot;
            case Equipment.EquipmentType.Helmet:
                return HelmetSlot;
            case Equipment.EquipmentType.Weapon:
                return WeaponSlot;
            default:
                return null;
        }
    }
}
