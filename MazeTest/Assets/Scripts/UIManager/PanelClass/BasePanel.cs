using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour 
{
    protected bool IsDisplay;
    protected  CanvasGroup panelCanvasGroup;
    private bool _isInit=false;
    public bool isInit { get { return _isInit; } }
    protected void Awake()
    {
        if (panelCanvasGroup == null) { panelCanvasGroup = GetComponent<CanvasGroup>(); }
        DontDestroyOnLoad(this.gameObject);
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {

    }
    public virtual void OnEnter(object param)
    {

    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {

    }
    public virtual void OnInit()
    {

    }
    public virtual void SetDisplay(bool isDis)
    {
        this.IsDisplay = isDis;
        if (this.IsDisplay)
        {
            gameObject.SetActive(true);
            //panelCanvasGroup.alpha = 1;
            //panelCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            gameObject.SetActive(false);
            //panelCanvasGroup.alpha = 0;
            //panelCanvasGroup.blocksRaycasts = false;
        }
    }
    public void SetInit()
    {
        _isInit = true;
    }
    void OnDestroy()
    {
        UIManager.Instance.Remove(this);
    }
}
