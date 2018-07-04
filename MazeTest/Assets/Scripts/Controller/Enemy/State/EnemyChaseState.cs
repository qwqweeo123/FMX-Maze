using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IState
 {
    public EnemyCtrl CurrEnemy { get; private set; }
    private Vector3 dir;
    public EnemyChaseState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.chasing;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToRun", true);
        //Debug.Log("进入idle状态"); ;
    }

    public void OnLeave()
    {
        //Debug.Log("离开idle状态");
        CurrEnemy.EnemyAnimator.SetBool("ToRun", false);
    }
    public void OnUpdate()
    {
        Quaternion rot = Quaternion.LookRotation(CurrEnemy.CurPlayer.transform.position - CurrEnemy.transform.position);
        if (rot.eulerAngles.y > 0.05f)
        {

            CurrEnemy.EnemyRotate(rot, CurrEnemy.RotateVelocity);
            dir = CurrEnemy.transform.forward * CurrEnemy.Velocity;
            dir.y = -CurrEnemy.GravityPower;
            CurrEnemy.EnemyCharacterCtrl.Move(dir * Time.deltaTime);
        }
        if (!CurrEnemy.IsInSight) 
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.backHome);
        }
        else if (Vector3.Distance(CurrEnemy.CurPlayer.transform.position, CurrEnemy.transform.position) < CurrEnemy.AttackRange) 
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
