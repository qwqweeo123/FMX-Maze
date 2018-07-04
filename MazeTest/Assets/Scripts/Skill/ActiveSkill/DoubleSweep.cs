using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSweep : ActiveSkill
{
    private static string skillName = "DoubleSweep";

    private PlayerCtrl pCtrl;
    private PlayerStatus pStatus;
    private Vector3 dir;
    private float range = 7.0f;
    private float angle = 270;

    public override void SkillCheck()
    {
        GameObject obj;
        for (int i = 0; i < pCtrl.EnemyPool.Count;i++ )
        {
            obj = pCtrl.EnemyPool[i];
            float distance = Vector3.Distance(obj.transform.position, Caster.transform.position);
            Vector3 v = obj.transform.position - Caster.transform.position;
            v.y = 0;
            float bAngle = Vector3.Angle(v, Caster.transform.forward);
            if (distance < range)
            {
                if (bAngle < angle * 0.5f)
                {
                    obj.GetComponent<EnemyCtrl>().GetHurt(this.SkiiStatus.Damage + pStatus.status.AtkCur);
                }
            }
        }
        pCtrl.PlayCurAudio(AudioManager.Instance.GetAudio("DoubleSweep"));
    }
    public override void SkillCast()
    {
        pCtrl = Caster.GetComponent<PlayerCtrl>();
        dir = pCtrl.hitPos - pCtrl.transform.position;
        pStatus = Caster.GetComponent<PlayerStatus>();
    }
    public override void SkillUpdate()
    {
        pCtrl.PlayerRotate(dir, 0.2f);
    }
    public override void SkillEnd()
    {
    }
}
