using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    Vector3 idleDir;
    public PlayerCtrl CurrPlayer { get; private set; }
    public PlayerIdleState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
        idleDir = new Vector3(0f, currPlayer.gravity, 0f);
    }  
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.idle;  
    }  
  
    public void OnEnter()  
    {
        
        CurrPlayer.PlayerAnimator.SetBool("ToIdle", true);
        idleDir.y = -CurrPlayer.gravity;
        //Debug.Log("进入idle状态"); ;
    }  
  
    public void OnLeave()  
    {
        //Debug.Log("离开idle状态");
        CurrPlayer.PlayerAnimator.SetBool("ToIdle", false);
    }  
  
    public void OnUpdate()  
    {
        if (CurrPlayer.hitObj != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (CurrPlayer.hitObj.layer==8 && Vector3.Distance(CurrPlayer.transform.position, CurrPlayer.hitObj.transform.position) < 2.0f)
                {
                    CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.attack);
                }
               
                else if (Vector3.Distance(CurrPlayer.hitPos, CurrPlayer.transform.position) > 0.5f)
                {
                    CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.run);
                }
            }
        }
        CurrPlayer.PlayerCharacterCtrl.Move(idleDir * Time.deltaTime);
    }  
  
    public void OnFixedUpdate()  
    {  
    }  
  
    public void OnLateUpdate()  
    {  
    }  
 
    #endregion 
}
