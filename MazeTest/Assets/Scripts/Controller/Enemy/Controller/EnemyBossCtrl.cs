using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossCtrl : EnemyCtrl
{
    [HideInInspector]
    public GameObject EndPoint;
    [HideInInspector]
    public GameObject MysticMan;
    void Awake()
    {
        base.Awake();
    }
	void Start () 
    {
        base.Start();
	}
	
	void Update () 
    {
        base.Update();
	}
    //Boss重击事件回调
    void Smash()
    {
        
        if (Vector3.Distance(transform.position, CurPlayer.transform.position) < AttackRange)
        {
            CurPlayer.GetComponent<PlayerCtrl>().Hurt(m_Status.status.AtkCur * 2);
        }
    }
    //随机出重击事件回调
    void SmasherRandom()
    {
        int r = Random.Range(0, 5);
        if (r == 0)
        {
            CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.smasher);
        }
    }
    //设置进视野全部攻击
    public override void SetInSight(bool inSight)
    {
        base.SetInSight(inSight);
        if (inSight&&CurrentRoom!=null)
        {
            CurrentRoom.GetComponent<RoomEnemySpawn>().StartAttack();
        }
    }
    //设置为最终点
    //设置重点
    public virtual void SetEndPoint()
    {
        this.EndPoint = Resources.Load<GameObject>("Prefabs/Effect/EndPoint");
        this.MysticMan = Resources.Load<GameObject>("Prefabs/NPC/MysticBusinessMan");    
    }
}
