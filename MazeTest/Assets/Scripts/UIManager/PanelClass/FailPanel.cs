using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPanel : BasePanel
{

	void Start () {
		
	}
	
	void Update () {
		
	}
    public override void OnEnter()
    {
        SetDisplay(true);
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
    public void ReStartGame()
    {
        GameManager.Instance.ReStartGame();
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
