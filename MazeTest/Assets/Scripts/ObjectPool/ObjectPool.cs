using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
     #region 单例
    private static ObjectPool instance;
    private ObjectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
    }
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    #endregion


    /// <summary>
    /// 对象池
    /// </summary>
    private Dictionary<string,List<GameObject>> pool;

    /// <summary>
    /// 预设体
    /// </summary>
    private Dictionary<string, GameObject> prefabs;

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public GameObject GetObj(GameObject obj)
    {
        //结果对象
        GameObject result=null;
        //判断是否有该名字的对象池
        if (pool.ContainsKey(obj.name))
        {
                //对象池里有对象
            if (pool[obj.name].Count > 0)
            {
                //获取结果
                result = pool[obj.name][0];
                //激活对象
                result.SetActive(true);
                //从池中移除该对象
                pool[obj.name].Remove(result);
                //返回结果
                return result;
            }
        }
        //如果没有该名字的对象池或者该名字对象池没有对象

        GameObject prefab = null;      
        //如果已经加载过该预设体
        if (prefabs.ContainsKey(obj.name))
        {
            prefab = prefabs[obj.name];
        }
        else     //如果没有加载过该预设体
        {
            //加载预设体
            prefab = obj;
            //更新字典
            prefabs.Add(obj.name, prefab);
        }

        //生成
        result = UnityEngine.Object.Instantiate(prefab);
        //改名（去除 Clone）
        result.name = obj.name;

        //返回
        return result;
    }

    /// <summary>
    /// 回收对象到对象池
    /// </summary>
    /// <param name="objName"></param>
    public void RecycleObj(GameObject obj)
    {
        //设置为非激活
        obj.SetActive(false);
        //判断是否有该对象的对象池
        if (pool.ContainsKey(obj.name))
        {       
            //放置到该对象池
            pool[obj.name].Add(obj);
        }
        else
        {
            //创建该类型的池子，并将对象放入
            pool.Add(obj.name, new List<GameObject>() { obj });
        }
    }
    public void Remove(GameObject obj)
    {
        prefabs.Remove(obj.name);
        pool.Remove(obj.name);
    }

}
