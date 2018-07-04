using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeRoot : Attribute
{
    private int m_hpCur;
    public int HpCur { get { return m_hpCur; } set { if (value <= 0) m_hpCur = 0; else if (value >= HpMax) m_hpCur = HpMax; else m_hpCur = value; } }
    private int m_mpCur;
    public int MpCur { get { return m_mpCur; } set { if (value <= 0)m_mpCur = 0; else if (value >= MpMax) m_mpCur = MpMax; else m_mpCur = value; } }
    private int m_atkCur;
    public int AtkCur { get {  return m_atkCur; } set { if (value <= 0)m_atkCur = 0; else if (value >= AtkMax) m_atkCur = AtkMax; else m_atkCur = value; } }
    private int m_defCur;
    public int DefCur { get { return m_defCur; } set { if (value <= 0)m_defCur = 0; else if (value >= DefMax) m_defCur = DefMax; else m_defCur = value; } }
    private int m_spdCur;
    public int SpdCur { get { return m_spdCur; } set { if (value <= 0)m_spdCur = 0; else if (value >= SpdMax) m_spdCur = SpdMax; else m_spdCur = value; } }

    private List<Attribute> m_NodeList = new List<Attribute>();
    private List<Attribute> m_CurNodeList = new List<Attribute>();
    public override void Calculate()
    {
        this.HpMax = this.Hp;
        this.MpMax = this.Mp;
        this.AtkMax = this.Atk;
        this.DefMax = this.Def;
        this.SpdMax = this.Spd;

        foreach (AttributeNode node in m_NodeList)
        {

             node.Calculate();

            this.HpMax += node.HpMax;
            this.MpMax += node.MpMax;
            this.AtkMax += node.AtkMax;
            this.DefMax += node.DefMax;
            this.SpdMax += node.SpdMax;
        }
    }

    public void Calculate(bool isInit)
    {
        Calculate();
        if (isInit)
        {
            this.HpCur = this.HpMax;
            this.MpCur = this.MpMax;
            this.AtkCur = this.AtkMax;
            this.DefCur = this.DefMax;
            this.SpdCur = this.SpdMax;
        }
    }

    public int NodeCount()
    {
        return m_NodeList.Count;
    }
    public void AddNode(AttributeNode node)
    {
        m_NodeList.Add(node);
    }
    public void RemoveNode(AttributeNode node)
    {
        m_NodeList.Remove(node);
    }
    public void AddCurNode(AttributeNode node)
    {
        this.HpMax += node.HpMax;
        this.MpMax += node.MpMax;
        this.AtkMax += node.AtkMax;
        this.DefMax += node.DefMax;
        this.SpdMax += node.SpdMax;

        this.HpCur += node.HpMax;
        this.MpCur += node.MpMax;
        this.AtkCur += node.AtkMax;
        this.DefCur += node.DefMax;
        this.SpdCur += node.SpdMax;
    }
    public void RemoveCurNode(AttributeNode node)
    {
        this.HpMax -= node.HpMax;
        this.MpMax -= node.MpMax;
        this.AtkMax -= node.AtkMax;
        this.DefMax -= node.DefMax;
        this.SpdMax -= node.SpdMax;

        this.HpCur -= node.HpMax;
        this.MpCur -= node.MpMax;
        this.AtkCur -= node.AtkMax;
        this.DefCur -= node.DefMax;
        this.SpdCur -= node.SpdMax;
    }
    public void AddOnceNode(AttributeNode node)
    {
        this.HpCur += node.HpMax;
        this.MpCur += node.MpMax;
        this.AtkCur += node.AtkMax;
        this.DefCur += node.DefMax;
        this.SpdCur += node.SpdMax;
    }
}
