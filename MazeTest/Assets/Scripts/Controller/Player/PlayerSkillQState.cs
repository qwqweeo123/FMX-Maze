using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillQState : IState
{
    private Animator curAnimator;
    private AnimatorStateInfo aInfo;
    private PlayerCtrl CurrPlayer;
    private PlayerStatus CurrPlayerStatus;
    private Skill curSkill;
    public PlayerSkillQState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
        CurrPlayer.CurSkill = this.curSkill;
        CurrPlayerStatus = currPlayer.GetComponent<PlayerStatus>();
        curAnimator = currPlayer.GetComponent<Animator>();
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.skillQ;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.Trail.Emit = true;
        if (CurrPlayerStatus.SkillQ != null)
        {
            curSkill = CurrPlayerStatus.SkillQ.CurSkill;
            curSkill.SetCaster(CurrPlayer.gameObject);
            CurrPlayer.PlayerAnimator.SetBool("ToSkillQ", true);
            curSkill.SkillCast();
        }
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToSkillQ", false);
        CurrPlayer.Trail.Emit = false;
    }  
  
    public void OnUpdate()  
    {
        if (curSkill != null)
        {
            curSkill.SkillUpdate();
        }
        aInfo = curAnimator.GetCurrentAnimatorStateInfo(0);
        if (aInfo.normalizedTime >= 1.0f && aInfo.IsName("SkillQ"))
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
