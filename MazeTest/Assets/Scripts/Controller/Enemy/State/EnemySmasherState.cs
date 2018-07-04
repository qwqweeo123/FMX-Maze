using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmasherState : IState
{
   public EnemyCtrl CurrEnemy { get; private set; }
    public EnemySmasherState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.smasher;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToSmasher", true);
    }

    public void OnLeave()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToSmasher", false);
    }
    public void OnUpdate()
    {
        AnimatorStateInfo CurStateInfo = CurrEnemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (CurStateInfo.normalizedTime >= 1.0f && CurStateInfo.IsName("Smasher"))
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.attack);
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
