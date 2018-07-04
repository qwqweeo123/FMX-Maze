using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState:IState
{
    public EnemyCtrl CurrEnemy { get; private set; }
    private Vector3 dir;
    public EnemyIdleState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.idle;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToIdle", true);
        //Debug.Log("进入idle状态"); ;
    }

    public void OnLeave()
    {
        //Debug.Log("离开idle状态");
        CurrEnemy.EnemyAnimator.SetBool("ToIdle", false);
    }

    public void OnUpdate()
    {
        dir = new Vector3(0, -CurrEnemy.GravityPower, 0);
        CurrEnemy.EnemyCharacterCtrl.Move(dir * Time.deltaTime);
        if (CurrEnemy.IsInSight && !CurrEnemy.CurPlayer.GetComponent<PlayerCtrl>().CurrStateMachine.IsInState((int)(PlayerCtrl.PlayerState.dead)))
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.chasing);
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
