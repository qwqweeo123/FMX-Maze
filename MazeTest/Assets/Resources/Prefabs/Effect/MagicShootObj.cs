using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootObj : MonoBehaviour 
{
    //运动速度
    private float velocity;
    private GameObject target;
    private int damage;
    private Transform parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target != null)
        {
            transform.LookAt(target.transform.position + new Vector3(0, 1.3f, 0));
            transform.Translate(transform.forward*velocity*Time.deltaTime, Space.World);
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            ObjectPool.Instance.RecycleObj(this.gameObject);
            col.GetComponent<PlayerCtrl>().Hurt(damage);
        }
    }
    void OnDestroy()
    {
        ObjectPool.Instance.Remove(this.gameObject);
    }
    public void SetTarget(GameObject obj,Transform parent,float v)
    {
        this.target = obj;
        this.velocity = v;
        this.damage = parent.GetComponent<EnemyArcherCtrl>().m_Status.status.AtkCur;
    }
}
