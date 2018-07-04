using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillWState : IState
{
    private Animator curAnimator;
    private AnimatorStateInfo aInfo;
    private PlayerCtrl CurrPlayer;
    private PlayerStatus CurrPlayerStatus;
    private Skill curSkill;
    public PlayerSkillWState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
        this.CurrPlayerStatus = currPlayer.GetComponent<PlayerStatus>();
        curAnimator = currPlayer.GetComponent<Animator>();
    }
    #region IState 成员
    public int GetStateID()
    {
        return (int)PlayerCtrl.PlayerState.skillW;
    }

    public void OnEnter()
    {
        CurrPlayer.Trail.Emit = true;
        if (CurrPlayerStatus.SkillW != null)
        {
            curSkill = CurrPlayerStatus.SkillW.CurSkill;
            curSkill.SetCaster(CurrPlayer.gameObject);
            CurrPlayer.PlayerAnimator.SetBool("ToSkillW", true);
            curSkill.SkillCast();
        }
    }

    public void OnLeave()
    {
        CurrPlayer.PlayerAnimator.SetBool("ToSkillW", false);
        CurrPlayer.Trail.Emit = false;
    }

    public void OnUpdate()
    {
        if (curSkill != null)
        {
            curSkill.SkillUpdate();
        }
        aInfo = curAnimator.GetCurrentAnimatorStateInfo(0);
        if (aInfo.normalizedTime >= 1.0f && aInfo.IsName("SkillW"))
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
