using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRState : IState
{
    private Animator curAnimator;
    public AnimatorStateInfo CurStateInfo;
    public PlayerCtrl CurrPlayer;
    private PlayerStatus CurrPlayerStatus;
    public PlayerSkillRState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
        this.CurrPlayerStatus = currPlayer.GetComponent<PlayerStatus>();
        curAnimator = currPlayer.GetComponent<Animator>();
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.skillR;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.Trail.Emit = true;
        CurrPlayer.PlayerAnimator.SetBool("ToSkillR", true);
        CurrPlayerStatus.SkillR.CurSkill.SkillCast();
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToSkillR", false);
        CurrPlayer.Trail.Emit = false;
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
