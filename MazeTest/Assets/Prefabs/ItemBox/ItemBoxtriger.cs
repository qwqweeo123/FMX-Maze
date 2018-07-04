using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxtriger : MonoBehaviour 
{
    private ItemBox parent;
    protected void Awake()
    {
        parent = GetComponentInParent<ItemBox>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            parent.SetRole(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            parent.SetRole(null);
            UIManager.Instance.RemovePanel(UIPanelType.BoxPanel);
        }
    }
}
