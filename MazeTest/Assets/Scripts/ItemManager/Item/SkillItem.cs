using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : Item
{
    public string AnimationPath{get;private set;}
    public Skill CurSkill{get;private set;}
    public int Damage { get; set; }
    public int Magic { get; set; }
    public float CD { get; private set; }
    public float CurrentCD { get; private set; }
    public bool IsCooling { get; private set; }
    public SkillItem(int id, string name, ItemType type, string description, int capacity,float dropOdds, int buyPrice, int sellPrice, string spritePath,string animaPath,Skill curSkill,int damage,int magic,float cd)
        : base(id, name, type, description, capacity,dropOdds, buyPrice, sellPrice, spritePath)
    {
        this.AnimationPath = animaPath;
        this.CurSkill = curSkill;
        this.CurSkill.SetSkillStatus(this);
        this.Damage = damage;
        this.Magic = magic;
        this.CD = cd;
        this.CurrentCD = 0;
        this.IsCooling=false;
    }
    public void SkillStart()
    {
        this.IsCooling = true;
    }
    public void CoolFinish()
    {
        this.IsCooling=false;
        this.CurrentCD = 0;
    }
    public void UpdateCD(float deltatime)
    {
        this.CurrentCD +=deltatime;
    }
}
