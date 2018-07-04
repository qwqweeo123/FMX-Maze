using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void OnEnter()
    {
        SetDisplay(true);
    }
    public override void OnResume()
    {
        SetDisplay(!IsDisplay);
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
    public void OpenSettingPanel()
    {
        UIManager.Instance.PushPanel(UIPanelType.SettingPanel);
    }
    public void ReStartGame()
    {
        GameManager.Instance.StartGame(1);
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
