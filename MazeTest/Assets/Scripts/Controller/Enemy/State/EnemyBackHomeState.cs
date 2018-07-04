using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackHomeState :IState
{
    public EnemyCtrl CurrEnemy { get; private set; }
    private Vector3 dir;
    public EnemyBackHomeState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.backHome;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToRun", true);
        CurrEnemy.FindPath(CurrEnemy.originPosition);
        //Debug.Log("进入idle状态"); ;
    }

    public void OnLeave()
    {
        //Debug.Log("离开idle状态");
        CurrEnemy.EnemyAnimator.SetBool("ToRun", false);
        CurrEnemy.StopFindPath();
    }
    public void OnUpdate()
    {

        Vector3 curPos = CurrEnemy.transform.position;
        curPos.y = 0;
        if (Vector3.Distance(CurrEnemy.originPosition, curPos) < 1.0f) 
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.idle);
        }
        else if (CurrEnemy.IsInSight)
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
