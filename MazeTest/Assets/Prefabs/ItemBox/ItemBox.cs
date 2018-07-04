using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour 
{
    private GameObject curRole;
    private Renderer rend;
    private Shader oriShader;
    private List<Item> ConItems;
	void Start () 
    {
        rend = GetComponent<Renderer>();
        oriShader = rend.material.shader;
        //Item item = ItemManager.Instance.GetItemById(1);
        //item.SetCount(5);
        //ConItems.Add(item);

	}
	
    void OnMouseEnter()
    {
        rend.material.shader = Shader.Find("Custom/Outline");
    }
    void OnMouseExit()
    {
        rend.material.shader = oriShader;
    }
    void OnMouseDown()
    {
        if (curRole != null)
        {
            UIManager.Instance.PushPanel(UIPanelType.BoxPanel, ConItems);
        }
    }
    public void SetItems(List<Item>  items)
    {
        if (ConItems == null)
        {
            ConItems = new List<Item>();
        }
        if (items!=null&& items.Count > 0)
        {
            foreach (Item item in items)
            {
                ConItems.Add(item);
            }
        }
    }
    public void SetRole(GameObject obj)
    {
        this.curRole = obj;
    }

}
