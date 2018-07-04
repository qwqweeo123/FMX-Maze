using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributePanel : BasePanel
{
    private Text HPText;
    private Text MPText;
    private Text AtkText;
    private Text DefText;
    private Text SpdText;


    private PlayerStatus m_RoleStatus;
    public override void OnEnter()
    {
        SetDisplay(true);
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
    public void OnClosePanel()
    {
        UIManager.Instance.ClosePanel(UIPanelType.AttributePanel);
    }
    private void InitWiget()
    {
        this.HPText = transform.Find("Content/Stats/Background/Stats Grid/Health Label/Stat Value").GetComponent<Text>();
        this.MPText = transform.Find("Content/Stats/Background/Stats Grid/Magic Label/Stat Value").GetComponent<Text>();
        this.AtkText = transform.Find("Content/Stats/Background/Stats Grid/Attack Label/Stat Value").GetComponent<Text>();
        this.DefText = transform.Find("Content/Stats/Background/Stats Grid/Defend Label/Stat Value").GetComponent<Text>();
        this.SpdText = transform.Find("Content/Stats/Background/Stats Grid/Speed Label/Stat Value").GetComponent<Text>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_RoleStatus = player.GetComponent<PlayerStatus>();
        m_RoleStatus.StatusChange += InitData;
        InitData();
        transform.Find("Content/Equip Slots").GetComponent<EquipmenBar>().EquipBarInit();
    }


	// Use this for initialization
    void InitData()
    {
        this.HPText.text = m_RoleStatus.status.HpMax.ToString();
        this.MPText.text = m_RoleStatus.status.MpMax.ToString();
        this.AtkText.text = m_RoleStatus.status.AtkMax.ToString();
        this.DefText.text = m_RoleStatus.status.DefMax.ToString();
        this.SpdText.text = m_RoleStatus.status.SpdMax.ToString();
    }
	
}
