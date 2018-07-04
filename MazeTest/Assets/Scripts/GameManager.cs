using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager :MonoSingleton<GameManager>
{
    private bool _isInit = false;//游戏是否初始化
    public bool isInit { get { return _isInit; } set { _isInit = value; } }
    private int m_currentLevel=1;//当前关卡层数
    public  int CurrentLevel{get{return m_currentLevel;}}

    private UserInfo _playerInfo=new UserInfo();//玩家信息
    public UserInfo PlayerInfo { get { return _playerInfo; } }

    private GameObject _currentplayer;
    public GameObject CurentPlayer
    {
        get
        {
            if (_currentplayer == null)
            {
                _currentplayer = GameObject.FindWithTag("Player");
            }
            return _currentplayer;
        }
    }
 
    public void StartGame(int i)
    {
        m_currentLevel = i;
        UIManager.Instance.PushPanel(UIPanelType.LoadingPanel);
        if (CurentPlayer != null)
        {
            CurentPlayer.GetComponent<PlayerStatus>().SavePlayerInfo();
        }
    }
    public void ReStartGame()
    {
        CurentPlayer.transform.position = new Vector3(16, 4, 18);
        CurentPlayer.GetComponent<PlayerStatus>().status.HpCur += 200;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    ////人物初始化
    //public void InitPlayer()
    //{
    //    TextAsset ta = Resources.Load<TextAsset>(@"JsonInfo/UserInfoJson");
    //    JSONObject j = new JSONObject(ta.text);
    //    _playerInfo.Lv = (int)j["Lv"].n;
    //    _playerInfo.Name = j["Name"].str;
    //    _playerInfo.Exp = (double)j["Exp"].n;
    //    _playerInfo.LvMax = (int)j["LvMax"].n;
    //    _playerInfo.GoldNumber = (int)j["GoldNumber"].n;
    //    _playerInfo.Status.Hp = (int)j["HP"].n;
    //    _playerInfo.Status.Mp = (int)j["MP"].n;
    //    _playerInfo.Status.Atk = (int)j["Atk"].n;
    //    _playerInfo.Status.Def = (int)j["Def"].n;
    //    _playerInfo.Status.Spd = (int)j["Spd"].n;
    //    _playerInfo.HpGrowth = (int)j["HpGrowth"].n;
    //    _playerInfo.MpGrowth = (int)j["MpGrowth"].n;
    //    _playerInfo.AtkGrowth = (int)j["AtkGrowth"].n;
    //    _playerInfo.DefGrowth = (int)j["DefGrowth"].n;
    //    _playerInfo.SpdGrowth = (int)j["SpdGrowth"].n;


    //    foreach (JSONObject temp in j["ItemList"].list)
    //    {
    //        string key = temp.keys[0];
    //        ItemInfo info = new ItemInfo(int.Parse(key), (int)temp[key].n);
    //        _playerInfo.ItemList.Add(info);
    //    }
    //    _isInit = true;
    //}
   
}
