using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    private Vector3 dir;

    public PlayerCtrl CurrPlayer { get; private set; }
    public PlayerRunState(PlayerCtrl currPlayer)  
    {
        CurrPlayer =currPlayer;
    }   
    #region IState 成员  
    public int GetStateID()  
    {
        return (int)PlayerCtrl.PlayerState.run;
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToRun", true);
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToRun", false);
    }  
  
    public void OnUpdate()  
    {
        dir = (CurrPlayer.hitPos - CurrPlayer.transform.position).normalized;
        dir.y = -CurrPlayer.gravity;
        CurrPlayer.PlayerRotate(dir, CurrPlayer.RotateVelocity);
        CurrPlayer.PlayerCharacterCtrl.Move(dir * CurrPlayer.Velocity*Time.deltaTime);
        //RaycastHit hit;
        //if(Physics.Raycast(CurrPlayer.transform.position,CurrPlayer.hitPos,out hit,Vector3.Distance(CurrPlayer.transform.position,CurrPlayer.hitPos)))
        //{
        //    if (hit.collider.gameObject.layer == 8 && CurrPlayer.hitObj.layer == 10)
        //    {
        //        CurrPlayer.StartFindPath(CurrPlayer.hitPos);
        //    }
        //}
        if (CurrPlayer.hitObj != null && CurrPlayer.hitObj.layer == 8)
        {

            if (Vector3.Distance(CurrPlayer.hitObj.transform.position, CurrPlayer.transform.position) <3.0f)
            {
                CurrPlayer.CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.attack);
            }
        }
        else if (Vector3.Distance(CurrPlayer.hitPos, CurrPlayer.transform.position) <=0.5f)
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
