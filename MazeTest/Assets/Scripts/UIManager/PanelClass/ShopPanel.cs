using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{

    //商店物品格UI预设
    private GameObject shopSlotObj;
    //商店UI格子组
    private Transform itemGrids;
    //商店物品列表
    private List<Item> shopItems;
    //商店物品格子列表
    private List<ShopSlot> shopSlots;
    //购买物品
    private ShopSlot buyItem;
    //玩家索引
    private PlayerStatus playerStatus;
    //确认面板
    private GameObject confirmPanel;
    //商品金币总数
    private int totalPrice;
    //商品金币总数UIText
    private Text amountGoldUI;
    //角色拥有金钱UIText
    private Text ownGoldUI;
    //确认面板数量输入区域
    private InputField countInput;
    public override void OnEnter(object param)
    {
        SetDisplay(true);
        shopItems = param as List<Item>;
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopSlot slot = itemGrids.GetChild(i).GetComponent<ShopSlot>();
            slot.SetItem(shopItems[i]);
        }
        RectTransform rt=itemGrids.GetComponent<RectTransform>();
        float hight = 20f + ((shopItems.Count / 2 + 1)) * 137;
        rt.sizeDelta = new Vector2(587.5f, hight);
        ownGoldUI.text = playerStatus.GoldNumber.ToString();

    }
    public override void OnPause()
    {
    }

    public override void OnResume()
    {
        SetDisplay(!IsDisplay);
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
    public override void OnInit()
    {
        InitWiget();
    }

    public void SetBuyItem(ShopSlot slot)
    {
        if (buyItem != null && buyItem!=slot)
        {
            buyItem.ExitSelect();
        }
        this.buyItem = slot;
        GoldAmountChange();
    }
    public void ChangeBuyItemNum(string numberStr)
    {
        string str = countInput.text;
        int i;
        if (int.TryParse(str,out i))
        {
            i = Convert.ToInt32(countInput.text);
            if (i < 1)
            {
                i = 1;
                countInput.text = i.ToString();
            }
            else if(i>99)
            {
                i = 99;
                countInput.text = i.ToString();
            }
            buyItem.ChangeAmount(i);
            GoldAmountChange();
        }
    }
    public void ConfirmBuy()
    {
        if (buyItem != null)
        {
            if (playerStatus.BuyItem(this.buyItem))
            {
                OwnGoldUIChange();
            }
            else
            {

            }
        }
    }
    public void ConfirmNum()
    {
        confirmPanel.SetActive(false);
    }
    public void ShowConfirmPanel()
    {
        if (buyItem != null)
        {
            this.confirmPanel.SetActive(true);
            countInput.text = buyItem.CurItem.Count.ToString();
        }
    }
    private void InitWiget()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        itemGrids = transform.Find("Item View/Item Grids");
        confirmPanel = transform.Find("NumberInfo").gameObject;
        confirmPanel.SetActive(false);
        countInput = transform.Find("NumberInfo/NumberInputField").GetComponent<InputField>();
        shopSlotObj = transform.Find("Item View/Item Grids").gameObject;
        amountGoldUI = transform.Find("TotalMoneyView/TotalGoldText").GetComponent<Text>();
        amountGoldUI.text = "0";
        ownGoldUI = transform.Find("TotalMoneyView/OwnGoldText").GetComponent<Text>();
    }
    private void OwnGoldUIChange()
    {
        ownGoldUI.text = playerStatus.GoldNumber.ToString();
    }
    public void GoldAmountChange()
    {
        if (buyItem == null)
        {
            amountGoldUI.text = "0";
        }
        else
        {
            int total=buyItem.CurItem.Count * buyItem.CurItem.BuyPrice;
            amountGoldUI.text = total.ToString();
        }
    }
    public void ClosePanel()
    {
        UIManager.Instance.RemovePanel(UIPanelType.ShopPanel);
    }
}
