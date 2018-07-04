using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameRoot : MonoBehaviour 
{

    private GameObject[] normalFloors;
    private GameObject EnemyFloor;
    private GameObject longWall;
    private GameObject roomobj;
    private GameObject doorwall;
    private GameObject wall;
    private GameObject way;
    private int randomNum;
    private GameObject player;
    private Vector3 BornPos;
    void Awake()
    {
        normalFloors = Resources.LoadAll<GameObject>("Prefabs/Terrain/Floors");
        EnemyFloor = Resources.Load<GameObject>("Prefabs/Terrain/EnemyFloors/BaseEnemy");
        longWall = Resources.Load<GameObject>("Prefabs/Terrain/Walls/LongWall");
        roomobj = Resources.Load<GameObject>("Prefabs/Terrain/EnemyFloors/BaseRoom");
        doorwall = Resources.Load<GameObject>("Prefabs/Terrain/Walls/DoorWall");
        wall = Resources.Load<GameObject>("Prefabs/Terrain/Walls/Wall");
        player = Resources.Load<GameObject>("Prefabs/Player");
    }
    void Start()
    {
        BornPos = new Vector3(16, 4, 18);
        if (GameManager.Instance.CurentPlayer != null)
        {
            GameManager.Instance.CurentPlayer.transform.position = BornPos;
            GameManager.Instance.CurentPlayer.GetComponent<PlayerCtrl>().CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.idle);
        }
        else
        {
            Instantiate(player, BornPos, Quaternion.identity);
        }
        int i = GameManager.Instance.CurrentLevel;
        int mazeWidth = 12 + i * 3;
        int mazeHight = 12 + i * 3;
        int roomNumber = 2 + i * 2;
        SFMaze sfmaze = new SFMaze();
        sfmaze.GenerateMaze(mazeWidth, mazeHight, new BaseRoom(1, 1), roomNumber);
        CreateMaze(sfmaze, mazeWidth, mazeHight);
    }
    //public void StartGame(int i)
    //{
    //    UIManager.Instance.PushPanel(UIPanelType.LoadingPanel);
    //    BornPos = new Vector3(18, 4, 18);
    //    Instantiate(player, BornPos, Quaternion.identity);
    //    m_currentLevel = i;
    //    int mazeWidth = 12 + i * 3;
    //    int mazeHight = 12 + i * 3;
    //    int roomNumber = 2 + i * 2;
    //    SFMaze sfmaze = new SFMaze();
    //    sfmaze.GenerateMaze(mazeWidth, mazeHight, new BaseRoom(1, 1), roomNumber);
    //    CreateMaze(sfmaze, mazeWidth, mazeHight);
    //}
    void CreateMaze(SFMaze sfm, int mazeWidth, int mazeHight)
    {
        //生成迷宫墙
        for (int i = 0; i <= mazeWidth; i++)
            for (int j = 0; j <= mazeHight; j++)
            {
                if (sfm.Map[i, j] == 1 && i - 1 >= 0 && i + 1 <= mazeWidth && j + 1 <= mazeHight && j - 1 >= 0)
                {
                    randomNum = Random.Range(1, 11);
                    //Debug.Log(randomNum);
                    if (randomNum <= 8)
                    {
                        way = Instantiate(normalFloors[Random.Range(0, normalFloors.Length)], new Vector3(18f * i, 0, 18f * j), Quaternion.Euler(0, 0, 0)) as GameObject;
                    }
                    else
                    {
                        way = Instantiate(EnemyFloor, new Vector3(18f * i, 0, 18f * j), Quaternion.Euler(0, 0, 0)) as GameObject;
                        way.GetComponent<EnemySpawn>().SpawnEnemy();
                    }
                    if (i == mazeWidth && j == mazeHight)
                    {
                        way.GetComponent<NavMeshSurface>().BuildNavMesh();
                    }
                    if (sfm.Map[i + 1, j] == 0)
                    {
                        Instantiate(wall, new Vector3(way.transform.position.x + 8.5f, way.transform.position.y, way.transform.position.z), Quaternion.Euler(0, 90f, 0), way.transform);
                    }
                    if (sfm.Map[i - 1, j] == 0)
                    {
                        Instantiate(wall, new Vector3(way.transform.position.x - 8.5f, way.transform.position.y, way.transform.position.z), Quaternion.Euler(0, 90f, 0), way.transform);
                    }
                    if (sfm.Map[i, j + 1] == 0)
                    {
                        Instantiate(wall, new Vector3(way.transform.position.x, way.transform.position.y, way.transform.position.z + 8.5f), Quaternion.Euler(0, 0, 0), way.transform);
                    }
                    if (sfm.Map[i, j - 1] == 0)
                    {
                        Instantiate(wall, new Vector3(way.transform.position.x, way.transform.position.y, way.transform.position.z - 8.5f), Quaternion.Euler(0, 0, 0), way.transform);
                    }
                }
            }
        //生成迷宫房间
        for (int i = 0; i < sfm.Rooms.Count; i++)
        {
            way = Instantiate(roomobj, new Vector3(sfm.Rooms[i].x * 18f, 0, sfm.Rooms[i].y * 18f), Quaternion.Euler(0, 0, 0));
            way.GetComponent<RoomEnemySpawn>().SpawnEnmey();
            int tx = sfm.Rooms[i].x;
            int ty = sfm.Rooms[i].y;

            //房间右边判断
            if (tx + 1 == mazeWidth || sfm.Map[tx + 1, ty] <= 1)
            {
                GameObject.Instantiate(longWall, new Vector3((tx + 1) * 18f + 8.5f, 0, (ty) * 18f), Quaternion.Euler(0, 90f, 0), way.transform);
            }
            else if (sfm.Map[tx + 1, ty] == 2)
            {
                GameObject.Instantiate(doorwall, new Vector3((tx + 1) * 18f + 8.5f, 0, (ty) * 18f), Quaternion.Euler(0, -90f, 0), way.transform);
            }
            //房间左边判断
            if (tx - 1 == 0 || sfm.Map[tx - 1, ty] <= 1)
            {
                GameObject.Instantiate(longWall, new Vector3((tx - 1) * 18f - 8.5f, 0, (ty) * 18f), Quaternion.Euler(0, 90f, 0), way.transform);
            }
            else if (sfm.Map[tx - 1, ty] == 2)
            {
                GameObject.Instantiate(doorwall, new Vector3((tx - 1) * 18f - 8.5f, 0, (ty) * 18f), Quaternion.Euler(0, 90f, 0), way.transform);
            }
            //房间上边判断
            if (ty + 1 == mazeHight || sfm.Map[tx, ty + 1] <= 1)
            {
                GameObject.Instantiate(longWall, new Vector3((tx) * 18f, 0, (ty + 1) * 18f + 8.5f), Quaternion.Euler(0, 0, 0), way.transform);
            }
            else if (sfm.Map[tx, ty + 1] == 2)
            {
                GameObject.Instantiate(doorwall, new Vector3((tx) * 18f, 0, (ty + 1) * 18f + 8.5f), Quaternion.Euler(0, 180, 0), way.transform);
            }
            //房间下边判断
            if (ty - 1 == 0 || sfm.Map[tx, ty - 1] <= 1)
            {
                GameObject.Instantiate(longWall, new Vector3((tx) * 18f, 0, (ty - 1) * 18f - 8.5f), Quaternion.Euler(0, 0, 0), way.transform);
            }
            else if (sfm.Map[tx, ty - 1] == 2)
            {
                GameObject.Instantiate(doorwall, new Vector3((tx) * 18f, 0, (ty - 1) * 18f - 8.5f), Quaternion.Euler(0, 0, 0), way.transform);
            }

            if (i == sfm.Rooms.Count - 1)
            {
                way.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
            if (sfm.Rooms[i].isEndRoom)
            {
                way.GetComponent<RoomEnemySpawn>().Boss.GetComponent<EnemyBossCtrl>().SetEndPoint();
                Debug.Log(way.GetComponent<RoomEnemySpawn>().Boss.transform.position);
            }

        }

    }
}
