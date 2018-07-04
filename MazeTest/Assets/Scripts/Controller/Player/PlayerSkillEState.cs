using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEState : IState
{
    private Animator curAnimator;
    public AnimatorStateInfo aInfo;
    public PlayerCtrl CurrPlayer;
    private PlayerStatus CurrPlayerStatus;
    private Skill curSkill;
     public PlayerSkillEState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
        this.CurrPlayerStatus = currPlayer.GetComponent<PlayerStatus>();
        curAnimator = currPlayer.GetComponent<Animator>();
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.skillE;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.Trail.Emit = true;
        if (CurrPlayerStatus.SkillE != null)
        {
            curSkill = CurrPlayerStatus.SkillE.CurSkill;
            curSkill.SetCaster(CurrPlayer.gameObject);
            CurrPlayer.PlayerAnimator.SetBool("ToSkille", true);
            curSkill.SkillCast();
        }
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToSkillE", false);
        CurrPlayer.Trail.Emit = false;
    }  
  
    public void OnUpdate()  
    {
        if (curSkill != null)
        {
            curSkill.SkillUpdate();
        }
        aInfo = curAnimator.GetCurrentAnimatorStateInfo(0);
        if (aInfo.normalizedTime >= 1.0f && aInfo.IsName("SkillE"))
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
