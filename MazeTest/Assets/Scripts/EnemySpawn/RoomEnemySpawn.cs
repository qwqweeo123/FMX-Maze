using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemySpawn : MonoBehaviour 
{
    private GameObject BossPrefab;
    private GameObject[] MonsterPrefabs;
    [HideInInspector]
    public GameObject Boss;
    [HideInInspector]
    public List<GameObject> Monsters; 
    private List<Transform> MonsterSpawnPos;
    private Transform BossSpawnPos;

    void Awake()
    {
        BossPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Boss/EnemyBoss1");
        MonsterPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy/Monster");
        Monsters = new List<GameObject>();
        if (MonsterSpawnPos == null)
        {
            MonsterSpawnPos = new List<Transform>();
        }
        //foreach (Transform child in transform)
        //{
        //   Transform temp= child.Find("SpawnPoint");
        //   if (temp.parent.gameObject.name != "BossBase")
        //   {
        //       MonsterSpawnPos.Add(temp);
        //   }
        //   else
        //   {
        //       BossSpawnPos = temp;
        //   }
        //}
    }
    void Start()
    {
        //Boss= Instantiate(BossPrefab, BossSpawnPos.position, Quaternion.identity);
        //foreach (Transform trans in MonsterSpawnPos)
        //{
        //    Monsters.Add(Instantiate(MonsterPrefabs[Random.Range(0, MonsterPrefabs.Length - 1)], trans.position, Quaternion.identity));
        //}
        //Boss.GetComponent<EnemyBossCtrl>().RoomMonsters = Monsters;
    }
    public void SpawnEnmey()
    {
        foreach (Transform child in transform)
        {
            Transform temp = child.Find("SpawnPoint");
            if (temp.parent.gameObject.name != "BossBase")
            {
                MonsterSpawnPos.Add(temp);
            }
            else
            {
                BossSpawnPos = temp;
            }
        }
        Boss = Instantiate(BossPrefab, BossSpawnPos.position, Quaternion.identity);
        Boss.name = BossPrefab.name;
        Boss.GetComponent<EnemyBossCtrl>().CurrentRoom = this.gameObject;
        foreach (Transform trans in MonsterSpawnPos)
        {
            int rand = Random.Range(0, MonsterPrefabs.Length - 1);
            GameObject obj = Instantiate(MonsterPrefabs[rand], trans.position, Quaternion.identity);
            obj.name = MonsterPrefabs[rand].name;
            obj.GetComponent<EnemyCtrl>().CurrentRoom= this.gameObject;
            Monsters.Add(obj);
        }
    }
    public void StartAttack()
    {
        if (Monsters.Count > 0)
        {
            foreach (GameObject obj in Monsters)
            {
                obj.GetComponent<EnemyCtrl>().SetInSight(true);
            }
        }
    }
}
