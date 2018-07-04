using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager
{

    private Animator m_animator;
    private AnimatorController animatorController;
    private AnimatorStateMachine animStM;


    public AnimationManager(Animator animator)
    {
        this.m_animator = animator;
        this.animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        this.animStM = animatorController.layers[0].stateMachine;
    }
    /// <summary>
    /// 添加状态机状态
    /// </summary>
    /// <param name="actName">状态名</param>
    /// /// <param name="actName">animator组件</param>
    public void AddAnimState(string actName)
    {
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == actName)
            {
                return;
            }
        }
        AnimatorState animSt = animStM.AddState(actName);
    }
    /// <summary>
    /// 删除状态机状态
    /// </summary>
    /// <param name="actName">状态名</param>
    public void DeleteAnimState(string actName)
    {
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == actName)
            {
                animStM.RemoveState(chidstate.state);
                return;
            }
        }
        Debug.LogError("未找到状态:" + actName);
    }
    /// <summary>
    /// 添加或替换状态机状态的动画片段
    /// </summary>
    /// <param name="name">使用状态机角色名</param>
    /// <param name="stateName">状态名</param>
    public void AddAnimMotion(AnimatorState animState, string animClipPath)
    {
        if (animState == null)
        {
            return;
        }
        AnimationClip newClip;
        newClip = Resources.Load<AnimationClip>(animClipPath);
        if (newClip != null)
        {
            animState.motion = newClip;
        }
        else
        {
            Debug.Log("找不到动画片段: " + newClip);
        }
        return;
    }
    public void DeleteMotion(AnimatorState animState)
    {
        if (animState == null || animState.motion==null)
        {
            return;
        }
        else
        {
            animState.motion = null;
        }
    }
    /// <summary>
    /// 增加转换条件
    /// </summary>
    /// <param name="state">状态名</param>
    /// <param name="transformSate">转换状态名</param>
    /// <param name="param">条件参数名</param>
    public void AddTransition(string state,string desState,string param) 
    {
        AnimatorState animSt = GetState(state);
        if(animSt.transitions.Length!=0)
        foreach (AnimatorStateTransition animStTran in animSt.transitions)
        {
            if (animStTran.destinationState.name == GetState(desState).name)
            {
                Debug.LogError("此转换:" + state + " to " + desState + "已存在");
                return;
            }
        }
        AnimatorStateTransition trans = animSt.AddTransition(GetState(desState));
        animatorController.AddParameter(param, AnimatorControllerParameterType.Bool);
        trans.AddCondition(AnimatorConditionMode.If,0,param);
    }
    public void DeleteTransition(string state,string desState) 
    {
        AnimatorState animSt = GetState(state);
        foreach (AnimatorStateTransition animStTrans in animSt.transitions) 
        {
            if (animStTrans.destinationState.name == GetState(desState).name) 
            {
                animSt.RemoveTransition(animStTrans);
                return;
            }
        }
        Debug.LogError("未找到此转换"+state+" to "+desState);
    }
    public AnimatorState GetState(string state)
    {
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == state)
            {
                return chidstate.state;
            }
        }
        Debug.LogError("未找到状态:" + state);
        return null;
    }

}
