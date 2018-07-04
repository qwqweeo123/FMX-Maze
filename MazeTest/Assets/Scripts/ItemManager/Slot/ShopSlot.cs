using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : Slot,IPointerDownHandler
{
    public override Slot.SlotType Type
    {
        get
        {
            return Slot.SlotType.Shop;
        }
    }

    private int totality=1;
    //是否被选择
    private bool isSelected=false;
    //价格text组件
    private Text priceText;
    //数量面板区域
    private GameObject numberView;
    //商店UI面板索引
    private ShopPanel parentPanel;
    //点击次数
    private int clickCount=0;
   void Awake()
    {
        curImage = transform.Find("Slot/Icon").GetComponent<Image>();
        amountText = transform.Find("NumberView/NumberText").GetComponent<Text>();
        numberView = transform.Find("NumberView").gameObject;
        numberView.SetActive(false);
        enterUI = transform.Find("Selected").gameObject;
        enterUI.SetActive(false);
        priceText = transform.Find("GoldText").GetComponent<Text>();
        parentPanel=transform.root.GetComponent<ShopPanel>();
        this.gameObject.SetActive(false);
    }
    public void AddAmount()
    {
        int i = CurItem.Count;
        if (i <=99)
        {
            i++;
            CurItem.SetCount(i);
            AmountChange();
            parentPanel.GoldAmountChange();
        }
    }
    public void SubAmount()
    {
        int i = CurItem.Count;
        if (i > 1)
        {
            i--;
            CurItem.SetCount(i);
            AmountChange();
            parentPanel.GoldAmountChange();
        }
    }
    public void ChangeAmount(int amount)
    {
        if (amount >= 1)
        {
            this.totality = amount;
            this.CurItem.SetCount(totality);
            AmountChange();
            parentPanel.GoldAmountChange();
        }
    }
    public override void SetItem(Item item)
    {
        curItem = item;
        if (curItem == null)
        {
            curImage.sprite = null;
            curImage.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            curImage.sprite = ItemManager.Instance.GetSpriteById(curItem.Id);
            curImage.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
            this.priceText.text = curItem.BuyPrice.ToString();
        }
        AmountChange();
    }
    public override void AmountChange()
    {
        if (CurItem == null)
        {
            amountText.gameObject.SetActive(false);
            return;
        }
        else if (CurItem.Count >=1)
        {
            amountText.text = CurItem.Count.ToString();
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            enterUI.SetActive(true);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            enterUI.SetActive(false);
        }
        UIManager.Instance.RemovePanel(UIPanelType.ItemInfoPanel);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clickCount++;
        if (eventData.pointerId == -1)
        {
            if (CurItem != null && eventData.pointerCurrentRaycast.gameObject.name == "Slot")
            {
                UIManager.Instance.PushPanel(UIPanelType.ItemInfoPanel, CurItem);
            }
            this.parentPanel.SetBuyItem(this);
            this.numberView.SetActive(true);
            enterUI.SetActive(true);
            AmountChange();
            isSelected = true;
            if (clickCount == 2)
            {
                parentPanel.ShowConfirmPanel();
                clickCount = 1;
            }
        }
    }
    public void ExitSelect()
    {
        this.isSelected = false;
        this.enterUI.SetActive(false);
        this.CurItem.SetCount(1);
        this.numberView.SetActive(false);
        this.clickCount = 0;
    }
}
