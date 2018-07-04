using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour 
{

    void Awake()
    {
    }
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(111);
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log(222);
    }
}
