using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapPanel : BasePanel
{
    //当前小地图相机
    private Transform miniMapCamera;
    //小地图人物标记
    private Transform playerPin;
    //小地图当前关卡
    private Text curLevel;
    //当前人物
    private GameObject curPlayerTrans;
    public override void OnEnter()
    {
        SetDisplay(true);
        curLevel.text = string.Format("迷宫第{0}层", GameManager.Instance.CurrentLevel);    
    }
    public override void OnPause()
    {
    }

    public override void OnResume()
    {
        SetDisplay(!IsDisplay);
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
    public override void OnInit()
    {
        InitWiget();
    }
    private void InitWiget()
    {
        curPlayerTrans = GameObject.FindWithTag("Player");
        playerPin = transform.Find("Pins and Marks/Player Pin");
        curLevel = transform.Find("Zone Field/Title").GetComponent<Text>();
        miniMapCamera = transform.Find("MiniMapCamera");
        miniMapCamera.position = new Vector3(curPlayerTrans.transform.position.x, 100, curPlayerTrans.transform.position.z);
    }
    void LateUpdate()
    {
        miniMapCamera.position = new Vector3(curPlayerTrans.transform.position.x, 100, curPlayerTrans.transform.position.z);
        playerPin.rotation = Quaternion.Euler(new Vector3(0, 0, -curPlayerTrans.transform.rotation.eulerAngles.y));
    }

}
