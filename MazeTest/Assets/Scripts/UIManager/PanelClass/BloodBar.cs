using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour 
{
    private Image HPValue;
    private EnemyStatus CurStatus;
	// Use this for initialization
	void Start () 
    {
        InitWiget();
	}
	
	// Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
		
	}
    void InitWiget()
    {
        HPValue = transform.Find("BloodBar/Fill").GetComponent<Image>();
        CurStatus = GetComponentInParent<EnemyStatus>();
        CurStatus.StatusChange += HpChange;
    }
    void HpChange() 
    {
        if (CurStatus.status.HpCur <= 0)
        {
            this.gameObject.SetActive(false);
        }
        HPValue.fillAmount = (float)CurStatus.status.HpCur / (float)CurStatus.status.HpMax;
    }
}
