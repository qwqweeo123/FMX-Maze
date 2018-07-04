using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour 
{
    private Transform SpawnPoint;
    private GameObject[] enemyObjs;
    void Awake()
    {
        SpawnPoint = transform.Find("SpawnPoint");
        enemyObjs= Resources.LoadAll<GameObject>("Prefabs/Enemy/Monster");
    }

    public void SpawnEnemy()
    {
        int rand = Random.Range(0, enemyObjs.Length - 1);
        GameObject obj = Instantiate(enemyObjs[rand], SpawnPoint.position, Quaternion.identity);
        obj.name = enemyObjs[rand].name;
    }

}
