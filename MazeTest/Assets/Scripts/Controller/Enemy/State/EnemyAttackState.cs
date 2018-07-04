using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState:IState
{
    public EnemyCtrl CurrEnemy { get; private set; }
    public EnemyAttackState(EnemyCtrl currEnemy)
    {
        CurrEnemy = currEnemy;
    }
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.attack;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToAttack", true);
    }

    public void OnLeave()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToAttack", false);
    }

    public void OnUpdate()
    {
        float distance = Vector3.Distance(CurrEnemy.transform.position, CurrEnemy.CurPlayer.transform.position);
        float y = Quaternion.LookRotation(CurrEnemy.CurPlayer.transform.position - CurrEnemy.transform.position).eulerAngles.y;
        if (y > 0.1f)
        {
            Quaternion rot = Quaternion.Euler(0f, y, 0f);
            CurrEnemy.EnemyRotate(rot, CurrEnemy.RotateVelocity);
        }
        AnimatorStateInfo CurStateInfo = CurrEnemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (distance > CurrEnemy.AttackRange && CurStateInfo.normalizedTime >= 0.3f && CurStateInfo.IsName("Attack"))
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.chasing);
        }
        if (CurrEnemy.CurPlayer.GetComponent<PlayerCtrl>().CurrStateMachine.IsInState((int)PlayerCtrl.PlayerState.dead))
        {
            CurrEnemy.CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.idle);
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
