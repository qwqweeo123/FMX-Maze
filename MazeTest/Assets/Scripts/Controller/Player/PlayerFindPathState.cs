using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFindPathState : IState
{
     public PlayerCtrl CurrPlayer { get; private set; }
     public PlayerFindPathState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
    }   
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.findPath;
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.PlayerMeshAgent.enabled = true;
        CurrPlayer.PlayerMeshAgent.isStopped = false;
        if (CurrPlayer.hitPos != null)
        {
            CurrPlayer.PlayerMeshAgent.destination = CurrPlayer.hitPos;
        }
        CurrPlayer.PlayerAnimator.SetBool("ToRun", true);
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToRun", false);
        CurrPlayer.PlayerMeshAgent.isStopped = true;
        CurrPlayer.PlayerMeshAgent.enabled = false;
    }  
  
    public void OnUpdate()  
    {
        if (Input.GetMouseButtonDown(1))
        {
            CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.idle);
        }
    }  
  
    public void OnFixedUpdate()  
    {  
    }  
  
    public void OnLateUpdate()  
    {  
    } 
    #endregion
}
