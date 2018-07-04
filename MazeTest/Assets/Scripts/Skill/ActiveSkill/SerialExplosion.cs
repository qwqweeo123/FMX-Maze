using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialExplosion : ActiveSkill
{
    public int Damage = 80;

    private static string skillName="SerialExplosion";
    private GameObject skillEffect;
    private PlayerCtrl pCtrl;
    private Quaternion rot;
    private Vector3 pos;
    private Timer timer;
    int boomNumber = 1;
    public override string SkillName
    {
        get
        {
            return skillName;
        }
    }
    public override void SkillCast()
    {
        pCtrl = Caster.GetComponent<PlayerCtrl>();
        PlayerStatus pStatus = Caster.GetComponent<PlayerStatus>();
        skillEffect = EffectManager.Instance.GetEffect(skillName);
        rot = Quaternion.LookRotation(pCtrl.hitPos - pCtrl.transform.position);
        rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        pCtrl.transform.rotation = rot;
      
        if (timer == null)
        {
            timer = new Timer(0.3f);
            timer.StartTimer();
            timer.tickEvent += Boom;
        }
        else
        {
            timer.Reset();
            boomNumber = 1;
        }
    }
    public override void SkillUpdate()
    {
        timer.UpdateTimer(Time.deltaTime);
    }
    public override void SkillEnd()
    {
    }
    private void Boom()
    {
        if (boomNumber < 4)
        {
            timer.Reset();
            GameObject boomObj = ObjectPool.Instance.GetObj(skillEffect);
            boomObj.transform.rotation = rot;
            boomObj.transform.position = pCtrl.transform.position;
            boomObj.transform.Translate(boomObj.transform.forward * boomNumber*5.0f,Space.World);
            foreach (GameObject obj in pCtrl.EnemyPool)
            {
                float distance=Vector3.Distance(obj.transform.position,boomObj.transform.position);
                if (distance < 2.5f)
                {
                   obj.GetComponent<EnemyCtrl>().GetHurt(Damage);
                }
            }
            boomNumber++;
        }
    }

}
