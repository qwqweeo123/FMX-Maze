using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : IState
{
    public PlayerCtrl CurrPlayer { get; private set; }
    public PlayerDieState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.dead;  
    }  
  
    public void OnEnter()  
    {
        
        CurrPlayer.PlayerAnimator.SetBool("ToDie", true);
        CurrPlayer.GetComponent<PlayerStatus>().SavePlayerInfo();
        UIManager.Instance.PushPanel(UIPanelType.FailPanel);
    }  
  
    public void OnLeave()  
    {
        //Debug.Log("离开idle状态");
        CurrPlayer.PlayerAnimator.SetBool("ToDie", false);
    }  
  
    public void OnUpdate()  
    {

    }  
  
    public void OnFixedUpdate()  
    {  
    }  
  
    public void OnLateUpdate()  
    {  
    }  
 
    #endregion 
}
