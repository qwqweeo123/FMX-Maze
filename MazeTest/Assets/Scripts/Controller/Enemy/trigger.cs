using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour 
{
    EnemyCtrl enemyCtr;
	// Use this for initialization
	void Start () {
        enemyCtr = GetComponentInParent<EnemyCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemyCtr.SetInSight(true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemyCtr.SetInSight(false);
        }
        
    }
}
