using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingPanel : BasePanel
{
    private Image loadingSlider;
    private Text loadingText;
    private Text levelText;
    private AsyncOperation operation;
    private float loadingSpeed = 1;
    private float targetValue;

    public override void OnEnter()
    {
        SetDisplay(true);
        StartCoroutine(AsyncLoading());
        levelText.text = string.Format("第{0}层迷宫生成中", GameManager.Instance.CurrentLevel);
    }
    public override void OnInit()
    {
        InitWiget();
    }
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.fillAmount)
        {
            //插值运算
            loadingSlider.fillAmount = Mathf.Lerp(loadingSlider.fillAmount, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.fillAmount - targetValue) < 0.01f)
            {
                loadingSlider.fillAmount = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.fillAmount * 100)).ToString() + "%";

        if ((int)(loadingSlider.fillAmount * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }

	}
    private void InitWiget()
    {
        loadingSlider = transform.Find("Loading Bar/Fill").GetComponent<Image>();
        loadingText = transform.Find("Loading Bar/LoadingText").GetComponent<Text>();
        levelText = transform.Find("Loading Bar/Text").GetComponent<Text>();
    }
    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync("MainScene");
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }


}
