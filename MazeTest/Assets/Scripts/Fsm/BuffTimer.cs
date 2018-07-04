using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimer:Timer
{
    AttributeNode node;//节点参数
    BuffTimer timer;//计数器参数
    public event Action<BuffTimer> timerRemoveEvent;
    public event Action<AttributeNode> nodeRemoveEvent;

    public BuffTimer(float second):base(second)
    {
        _currentTime = 0;

        _endTime = second;
    }
    public override void UpdateTimer(float deltaTime)
    {
        if (_isTicking)
        {
            _currentTime += deltaTime;

            if (_currentTime > _endTime)
            {
                _isTicking = false;
                if (timerRemoveEvent != null && nodeRemoveEvent!=null)
                {
                    timerRemoveEvent(timer);
                    nodeRemoveEvent(node);
                }
            }

        }
    }
    public void SetParam(AttributeNode node,BuffTimer timer)
    {
        this.node = node;
        this.timer = timer;
    }
}
