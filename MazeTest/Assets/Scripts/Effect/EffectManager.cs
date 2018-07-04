using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager 
{
    private static EffectManager _instance;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new  EffectManager();
            return _instance;
        }
    }
    private EffectManager()
    {
        ParseItemJson();
    }
    private Dictionary<string,GameObject> EffectDic;

    private void ParseItemJson()
    {

        if (EffectDic == null)
        {
            EffectDic = new Dictionary<string, GameObject>();
        }
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/EffectInfoJson");
        JSONObject j = new JSONObject(ta.text);
        foreach (JSONObject temp in j.list)
        {
            string sName =temp["skillName"].str;
            GameObject obj = Resources.Load<GameObject>(temp["effectPath"].str);
            if (obj!=null)
            {
                EffectDic.Add(sName,obj);
            }
            else
            {
                Debug.LogError("加载特效为空");
            }
        }
    }

    public GameObject GetEffect(string skillName)
    {
        GameObject obj;
        if (EffectDic != null&&EffectDic.TryGetValue(skillName,out obj))
        {
            return obj;
        }
        else
        {
            Debug.Log("特效字典为空或技能特效不存在: "+skillName);
            return null;
        }
    }
}
