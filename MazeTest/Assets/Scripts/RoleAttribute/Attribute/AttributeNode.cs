using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttributeNode : Attribute
{
    private List<Attribute> m_NodeList = new List<Attribute>();
    public override void Calculate()
    {
        this.HpMax = this.Hp;
        this.MpMax = this.Mp;
        this.AtkMax = this.Atk;
        this.DefMax = this.Def;
        this.SpdMax = this.Spd;


        foreach (AttributeNode node in m_NodeList)
        {
            if (node.NodeCount() > 0)
            {
                node.Calculate();
            }

            this.HpMax += node.HpMax;
            this.MpMax += node.MpMax;
            this.AtkMax += node.AtkMax;
            this.DefMax += node.DefMax;
            this.SpdMax += node.SpdMax;
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
        if (m_NodeList.Count == 0)
        {
            Calculate();
        }
    }
    public List<Attribute> GetNodelist()
    {
        return m_NodeList;
    }
}
