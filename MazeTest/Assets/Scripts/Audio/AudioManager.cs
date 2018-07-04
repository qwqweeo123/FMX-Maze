using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AudioManager();
            return _instance;
        }
    }
    public AudioManager()
    {
        ParseItemJson();
    }
    private Dictionary<string, AudioClip> audioDic;

    private void ParseItemJson()
    {
        if (audioDic == null)
        {
            audioDic = new Dictionary<string, AudioClip>();
        }
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/AudioInfoJson");
        JSONObject j = new JSONObject(ta.text);
        foreach (JSONObject temp in j.list)
        {
            string sName = temp["audioName"].str;
            AudioClip obj = Resources.Load<AudioClip>(temp["audioPath"].str);
            if (obj != null && !audioDic.ContainsKey(sName))
            {
                audioDic.Add(sName, obj);
            }
            else
            {
                Debug.LogError("加载声音为空，或声音已存在");
            }
        }
    }
    public AudioClip GetAudio(string name)
    {
       AudioClip obj;
        if (audioDic != null && audioDic.TryGetValue(name, out obj))
        {
            return obj;
        }
        else
        {
            Debug.Log("音频文件为空或音频不存在: " + name);
            return null;
        }
    }
}
