using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    private static BuffManager _instance;

    public static BuffManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BuffManager();
            }
            return _instance;
        }
    }
    private List<BuffTimer> timerList;
    private List<AttributeNode> bufferList;

    private BuffManager()
    {
        bufferList = new List<AttributeNode>();
        timerList = new List<BuffTimer>();
    }
    public void AddBuff(AttributeRoot status,AttributeNode buffNode)
    {
        status.AddCurNode(buffNode);
    }
    public void AddBuff(AttributeRoot status,AttributeNode buffNode,float lastTime)
    {
        status.AddCurNode(buffNode);
        BuffTimer bTimer = new BuffTimer(lastTime);
        bTimer.StartTimer();
        bTimer.SetParam(buffNode, bTimer);
        bTimer.nodeRemoveEvent += status.RemoveCurNode;
        bTimer.timerRemoveEvent += RemoveTimer;

    }
    public void AddBuff(Attribute status, AttributeNode buffNode, float lastTime, float interval)
    {

    }
    //更新buff时间
    public void UpdateBuff(float deltatime)
    {
        for (int i = 0; i < timerList.Count; i++)
        {
            timerList[i].UpdateTimer(deltatime);
        }
    }
    public void RemoveTimer(BuffTimer timer)
    {
        timerList.Remove(timer);
    }
}
