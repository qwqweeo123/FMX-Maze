using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCtrl : MonoBehaviour
{
    private string npcName = "神秘商人";
    public string Name { get{return npcName;} }
    public List<Item> ShopList;
    private GameObject player;
    private Color oriColor;
    private Renderer npcRender;
	void Start () 
    {
        npcRender = GetComponentInChildren<Renderer>();
        oriColor = npcRender.material.color;
        ShopList = new List<Item>();
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/NpcInfoJson");
        JSONObject j = new JSONObject(ta.text);
        foreach (JSONObject temp in j.list)
        {
            if (temp["name"].str == this.Name)
            {
                foreach (JSONObject tempi in temp["shopList"].list)
                {
                    Item item = ItemManager.Instance.GetItemById((int)tempi.n);
                    item.SetCount(1);
                    this.ShopList.Add(item);
                }
            }
        }
        player=GameObject.FindWithTag("Player");
	}
    public void OpenShopPanel()
    {
        UIManager.Instance.PushPanel(UIPanelType.ShopPanel, this.ShopList);
        UIManager.Instance.RemovePanel(UIPanelType.NpcSessionPanel);
    }
    void OnMouseEnter()
    {
        npcRender.material.color= Color.red;
    }
    void OnMouseExit()
    {
        npcRender.material.color = oriColor;
    }
    void OnMouseDown()
    {
        if (player.tag == "Player" && Vector3.Distance(transform.position, player.transform.position) < 5.0f)
        {
            UIManager.Instance.PushPanel(UIPanelType.NpcSessionPanel,this);
        }
    }

}
