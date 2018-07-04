using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour ,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{

    protected Item curItem;
    protected Image curImage;
    protected Text amountText;
    protected GameObject enterUI;
    public Item CurItem
    {
        get { return curItem; }
    }
    public virtual SlotType Type { get { return SlotType.Null; } }
    protected GameObject drag_icon;
    protected void Awake() 
    {
         curImage = transform.Find("Icon").GetComponent<Image>();
         amountText = transform.Find("NumberText").GetComponent<Text>();
         enterUI = transform.Find("Selected").gameObject;
         enterUI.SetActive(false);
    }
    void Start()
    {

        if (curItem == null)
        {
            SetItem(null);
        }
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (curItem == null)
        {
            return;
        }
        drag_icon = new GameObject("icon");
        drag_icon.transform.SetParent(GameObject.Find("Canvas").transform, false);
        drag_icon.AddComponent<RectTransform>();
        var img = drag_icon.AddComponent<Image>();
        img.sprite = curImage.sprite;
        //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效  
        CanvasGroup group = drag_icon.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        curImage.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (curItem == null)
        {
            return;
        }
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(drag_icon.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out pos))
        {
            drag_icon.transform.position = pos;
        }  
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //拖拽结束，销毁代替品  
        if (drag_icon)
        {
            Destroy(drag_icon);
        }
        if (CurItem != null)
        {
            curImage.gameObject.SetActive(true);
            AmountChange();
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        enterUI.SetActive(true);
        if (CurItem != null)
        {
            UIManager.Instance.PushPanel(UIPanelType.ItemInfoPanel, CurItem);
        }

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        enterUI.SetActive(false);
        UIManager.Instance.RemovePanel(UIPanelType.ItemInfoPanel);
    }
    public virtual void SwitchItem(Slot slot)
    {
        Item temp = CurItem;
        SetItem(slot.CurItem);

        slot.SetItem(temp);
    }
    public virtual void SetItem(Item item)
    {
        curItem = item;
        if (curItem == null)
        {
            curImage.sprite = null;
            curImage.gameObject.SetActive(false);
        }
        else
        {
            curImage.sprite = ItemManager.Instance.GetSpriteById(curItem.Id);
            curImage.gameObject.SetActive(true);
        }
        AmountChange();
    }
    public virtual void AmountChange()
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
        else if(CurItem.Count==1)
        {
            amountText.gameObject.SetActive(false);
        }
        else if (CurItem.Count == 0)
        {
            SetItem(null);
        }
    }

    public enum SlotType
    {
        Null,
        Grid,
        Equipment,
        Skill,
        Bos,
        Shop,
        Prop
    }
}
