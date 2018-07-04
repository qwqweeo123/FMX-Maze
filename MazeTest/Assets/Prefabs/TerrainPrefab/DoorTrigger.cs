using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour 
{
    private Transform leftDoor;
    private Transform rightDoor;
    private BoxCollider col;
    private Animator anim;
    private Shader oriShader;
    private GameObject curPlayer;
    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = true;
        leftDoor = transform.Find("Door_Left");
        rightDoor = transform.Find("Door_Right");

        curPlayer = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        oriShader = leftDoor.GetComponent<Renderer>().material.shader;
    }

    void OnMouseEnter()
    {
        leftDoor.GetComponent<Renderer>().material.shader = Shader.Find("Custom/OcclusionStandard");
        rightDoor.GetComponent<Renderer>().material.shader = Shader.Find("Custom/OcclusionStandard");
    }
    void OnMouseExit()
    {
        leftDoor.GetComponent<Renderer>().material.shader = oriShader;
        rightDoor.GetComponent<Renderer>().material.shader = oriShader;
    }
    void OnMouseDown()
    {
        if (curPlayer.tag=="Player"&&Vector3.Distance(transform.position,curPlayer.transform.position)<10.0f)
        {
            col.enabled = false;
            anim.SetBool("IsOpen", true);
        }
    }
}
