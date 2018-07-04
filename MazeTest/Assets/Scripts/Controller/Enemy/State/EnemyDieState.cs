using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IState 
{
    private List<Item> conItems;
    public AnimatorStateInfo CurStateInfo { get; private set; }
    public EnemyCtrl CurrEnemy { get; private set; }
    public EnemyDieState(EnemyCtrl currEnemy)  
    {
        CurrEnemy = currEnemy;
        conItems = new List<Item>();
    } 
    #region IState 成员
    public int GetStateID()
    {
        return (int)EnemyCtrl.EnemyState.dead;
    }

    public void OnEnter()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToDie", true);
        if (CurrEnemy.CurrentRoom != null)
        {
            CurrEnemy.CurrentRoom.GetComponent<RoomEnemySpawn>().Monsters.Remove(CurrEnemy.gameObject);
        }
        CurrEnemy.CurPlayer.GetComponent<PlayerCtrl>().GetExp(CurrEnemy.GetComponent<EnemyStatus>().CtExp);
        CurrEnemy.CurPlayer.GetComponent<PlayerCtrl>().hitObj = null;
        CurrEnemy.CurPlayer.GetComponent<PlayerCtrl>().EnemyPool.Remove(CurrEnemy.gameObject);
        CurrEnemy.gameObject.layer=2;
        FallItem();
        if (CurrEnemy.tag == "EnemyBoss")
        {
            EnemyBossCtrl boss = CurrEnemy as EnemyBossCtrl;
            if (boss.EndPoint != null)
            {
                Vector3 pos = new Vector3(CurrEnemy.transform.position.x, CurrEnemy.transform.position.y, CurrEnemy.transform.position.z + 5.0f);
                GameObject.Instantiate(boss.EndPoint, pos, Quaternion.identity);
            }
            if (boss.MysticMan != null)
            {
                Vector3 pos = new Vector3(CurrEnemy.transform.position.x + 5.0f, CurrEnemy.transform.position.y, CurrEnemy.transform.position.z);
                GameObject.Instantiate(boss.MysticMan, pos, Quaternion.identity);
            }
        }
    }

    public void OnLeave()
    {
        CurrEnemy.EnemyAnimator.SetBool("ToDie", false);
    }

    public void OnUpdate()
    {
        //死亡动画播放完清除删除角色
        AnimatorStateInfo CurStateInfo = CurrEnemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (CurStateInfo.normalizedTime >= 0.9f && CurStateInfo.IsName("Die"))
        {
            GameObject.DestroyObject(CurrEnemy.gameObject);
        }
    }

    public void OnFixedUpdate()
    {
    }

    public void OnLateUpdate()
    {
    }

    #endregion 
    //物品掉落算法
    private void FallItem()
    {
        foreach(int i in CurrEnemy.m_Status.ItemsID)
        {
            Item item = ItemManager.Instance.GetItemById(i);
            if (IsFall(item.DropOdds))
            {
                int r1=Random.Range(1,3);
                if (item.Capacity > 1)
                {
                    if(r1>2)
                    {
                        item.SetCount(Random.Range( 1,item.Capacity));
                    }
                    else if (r1 <= 2)
                    {
                        item.SetCount(Random.Range(1, item.Capacity / 2));
                    }
                }
                else
                {
                    item.SetCount(1);
                }
                conItems.Add(item);
            }
        }
        if (conItems .Count> 0)
        {
            GameObject obj= GameObject.Instantiate(CurrEnemy.ItemBox);
            obj.transform.position = CurrEnemy.transform.position;
            obj.GetComponent<ItemBox>().SetItems(conItems);
        }
    }
    private bool IsFall(float p)
    {
        float rand = Random.Range(0f, 1.0f);
        if (rand <= p)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
