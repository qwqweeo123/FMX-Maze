using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSessionPanel : BasePanel
{
    private NpcCtrl npc;
    public override void OnEnter()
    {
        SetDisplay(true);
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
    public override void OnEnter(object param)
    {
        SetDisplay(true);
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        this.npc = param as NpcCtrl;
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
    public void OpenShopPanel()
    {
        UIManager.Instance.PushPanel(UIPanelType.ShopPanel, this.npc.ShopList);
        UIManager.Instance.RemovePanel(UIPanelType.NpcSessionPanel);
    }
    public void CloseSession()
    {
        UIManager.Instance.RemovePanel(UIPanelType.NpcSessionPanel);
    }
}
