using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState :IState 
{
    private Vector3 dir;

    public AnimatorStateInfo CurStateInfo { get; private set; }
    public PlayerCtrl CurrPlayer { get; private set; }
   public PlayerAttackState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.attack;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToAttack", true);
        dir = (CurrPlayer.hitObj.transform.position - CurrPlayer.transform.position).normalized;
        dir = new Vector3(dir.x, -CurrPlayer.gravity, dir.z);
        CurrPlayer.Trail.Emit = true;
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToAttack", false);
        CurrPlayer.Trail.Emit = false;
    }  
  
    public void OnUpdate()  
    {

        CurrPlayer.PlayerRotate(dir, CurrPlayer.RotateVelocity);
        if (CurrPlayer.hitObj == null)
        {
            CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.idle);
        }
        else if (CurrPlayer.hitObj.layer == 10)
        {
            CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.run );
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
