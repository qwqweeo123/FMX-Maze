using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    protected bool _isTicking;//是否在计时中

    protected float _currentTime;//当前时间

    protected float _endTime;//结束时间


    public event Action tickEvent;

    public float CurrentTime
    {
        get
        {
            return _currentTime;
        }
    } 

    public Timer(float second)
    {

        _currentTime = 0;

        _endTime = second;

    }

    /// <summary>

    /// 开始计时

    /// </summary>

    public virtual void StartTimer()
    {

        _isTicking = true;

    }

    /// <summary>

    /// 更新中

    /// </summary>

    public virtual void UpdateTimer(float deltaTime)
    {
        if (_isTicking)
        {
            _currentTime += deltaTime;

            if (_currentTime > _endTime)
            {

                _isTicking = false;
                if (tickEvent != null)
                {
                    tickEvent();
                }
            }

        }
    }

    public virtual void Reset()
    {
        _currentTime = 0;
        _isTicking = true;
    }
    public virtual void Reset(float newTime)
    {
        _currentTime = newTime;
        _isTicking = true;
    }
}
