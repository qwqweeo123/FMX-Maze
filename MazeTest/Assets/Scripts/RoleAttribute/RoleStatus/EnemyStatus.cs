using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour 
{
    public string Name { get; set; }
    public double CtExp { get; set; }
    public List<int> ItemsID;

    public AttributeRoot status = new AttributeRoot();
    public event Action StatusChange;
    void Start()
    {
        Load();
    }

    private void Load()
    {
        ItemsID = new List<int>();
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/EnemyInfoJson");
        JSONObject j = new JSONObject(ta.text);
        foreach (JSONObject temp in j.list)
        {
            if (temp["Name"].str == name)
            {
                this.status.Hp = (int)temp["HP"].n; ;
                this.status.Mp = (int)temp["MP"].n;
                this.status.Atk = (int)temp["Atk"].n;
                this.status.Def = (int)temp["Def"].n;
                this.status.Spd = (int)temp["Spd"].n;
                this.CtExp = (double)temp["CtExp"].n;
                foreach(JSONObject tempi in temp["Items"].list)
                {
                    this.ItemsID.Add((int)tempi.n);
                }
            }
        }
        status.Calculate(true);
    }
    public void Hurt()
    {
        StatusChange();
    }

}
