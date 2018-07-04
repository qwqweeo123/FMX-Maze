using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherCtrl : EnemyCtrl
{
    private GameObject shootObj;
    public Transform shootTrans;
    void Start()
    {
        base.Start();
        shootObj = Resources.Load<GameObject>("Prefabs/Effect/MagicShootObj");
    }
    void NormalAtk()
    {
        GameObject obj = ObjectPool.Instance.GetObj(Resources.Load<GameObject>("Prefabs/Effect/MagicShootObj"));
        obj.GetComponent<MagicShootObj>().SetTarget(CurPlayer,transform,10.0f);
        obj.transform.position = shootTrans.position;
    }
}
