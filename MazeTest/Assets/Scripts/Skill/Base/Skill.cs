using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    public virtual string SkillName { get { return null; } }

    public SkillItem SkiiStatus{get;private set;}
    public GameObject Caster { get; private set; }

    public virtual void SetSkillStatus(SkillItem status)
    {
        this.SkiiStatus = status;
    }
    public virtual void SetCaster(GameObject caster)
    {
        this.Caster = caster;
    }
    public virtual void SkillCheck()
    {

    }
    public virtual void SkillCast()
    {
    }
    public virtual void SkillUpdate()
    {

    }
    public virtual void SkillEnd()
    {

    }
}
