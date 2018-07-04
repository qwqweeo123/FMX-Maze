using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private UIManager()
    {
        ParseUIPanelTypeJson();
    }
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private static Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private static Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private List<BasePanel> panelStack;

    //public void InitPanel()
    //{
    //    foreach (UIPanelType type in Enum.GetValues(typeof(UIPanelType)))
    //    {
    //        BasePanel panel = GetPanel(type);
    //        panel.OnInit();
    //    }
    //}
    /// <summary>
    /// 把某个页面入队列，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new List<BasePanel>();
        BasePanel panel = GetPanel(panelType);
 
        //判断一下队列里面是否有页面
        if (panelStack.Contains(panel))
        {
            panel.OnResume();
        }
        else
        {
            panel.OnEnter();
            panelStack.Add(panel);
        }
    }
    public void PushPanel(UIPanelType panelType, object param)
    {
        if (panelStack == null)
            panelStack = new List<BasePanel>();
        BasePanel panel = GetPanel(panelType);
        //if (!panel.isInit)
        //{
        //    panel.OnInit();
        //    panel.SetInit();
        //}
        //判断一下队列里面是否有页面
        if (panelStack.Contains(panel))
        {
            panel.OnResume();
        }
        else
        {
            panel.OnEnter(param);
            panelStack.Add(panel);
        }

    }

    public void ClosePanel(UIPanelType panelType)
    {
        if (panelStack == null)
        {
            panelStack = new List<BasePanel>();
        }
        BasePanel panel = GetPanel(panelType);
        //判断一下队列里面是否有页面
        if (panelStack.Contains(panel))
        {
            panel.OnExit();
        }
        else
        {
            Debug.LogError("队列内不存在此页面" + panelType.ToString());
        }
    }
    public void RemovePanel(UIPanelType panelType)
    {
        if (panelStack == null)
        {
            panelStack = new List<BasePanel>();
        }
        BasePanel panel = GetPanel(panelType);
        //判断一下队列里面是否有页面
        if (panelStack.Contains(panel))
        {
            panel.OnExit();
            panelStack.Remove(panel);
        }
    }
    //获取UI面板BasePanel组件
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel;
        panelDict.TryGetValue(panelType, out panel);
        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            string path;
            panelPathDict.TryGetValue(panelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            instPanel.name = panelType.ToString();
            panel = instPanel.GetComponent<BasePanel>();
            panelDict.Add(panelType, panel);
            if (!panel.isInit)
            {
                panel.OnInit();
                panel.SetInit();
            }
            panel.SetDisplay(false);
            return panel;
        }
        else
        {
            return panel;
        }

    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
        TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/UIPanelInfoJson");
        List<UIPanelInfo> infoList = JsonConvert.DeserializeObject<List<UIPanelInfo>>(ta.text);

        foreach (UIPanelInfo info in infoList)
        {
            panelPathDict.Add(info.panelType, info.path);
            
        }
    }
    public void Remove(BasePanel panel)
    {
        KeyValuePair<UIPanelType, BasePanel> temp=new KeyValuePair<UIPanelType,BasePanel>();
        foreach (KeyValuePair<UIPanelType, BasePanel> kPair in panelDict)
        {
            if (kPair.Value == panel)
            {
                temp = kPair;
            }
        }
        panelDict.Remove(temp.Key);
    }
}
