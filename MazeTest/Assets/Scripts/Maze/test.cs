using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour 
{
    public Renderer rend;
	// Use this for initialization
	void Start () 
    {
        rend = GetComponent<Renderer>();
        Debug.Log(rend.bounds.size.x);
        Debug.Log(rend.bounds.size.y);
        Debug.Log(rend.bounds.size.z);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
