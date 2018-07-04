using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : IState
{
    public EnemyCtrl CurrEnemy { get; private set; }
    public EnemyHurtState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.hurt;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToHurt", true);
        //Debug.Log("进入idle状态"); ;
    }

    public void OnLeave()
    {
        //Debug.Log("离开idle状态");
        CurrEnemy.EnemyAnimator.SetBool("ToHurt", false);
    }
    public void OnUpdate()
    {
        AnimatorStateInfo CurStateInfo = CurrEnemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (CurStateInfo.normalizedTime >= 1.0f && CurStateInfo.IsName("Hurt"))
        {
            CurrEnemy.CurrStateMachine.ReturnLastState();
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
