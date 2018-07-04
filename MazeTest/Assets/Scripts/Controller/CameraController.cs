using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    //距离
    public float distance = 8;
    //最大距离
    public float maxDistance=15;
    //最小距离
    public float minDistance=3;
    //横向角度
    public float rot = 0;
    //纵向角度
    public float roll = 30f;
    //滚轮速度
    public float zoomSpeed = 0.2f;
    //目标物体
    public GameObject target;

    private Vector3 cameraPos;
    //目标位置
    private Vector3 targetPos;
    // 所有障碍物的Renderer数组  
    private List<Renderer> _ObstacleCollider;
    // 临时接收，用于存储  
    private Renderer _tempRenderer;  

    void Start()
    {
        target = GameManager.Instance.CurentPlayer;
        transform.Rotate(roll, 0, 0);
        _ObstacleCollider = new List<Renderer>(); 
    }
    void Update()
    {
        // 调试使用：红色射线，仅Scene场景可见     
#if UNITY_EDITOR
        Debug.DrawLine(target.transform.position, transform.position, Color.red);
#endif
        RaycastHit[] hits;
        int mask = 1 << 9;
        hits = Physics.RaycastAll(transform.position, transform.forward,100f,mask);
        //  如果碰撞信息数量大于0条  
        if (hits.Length > 0)
        {   // 设置障碍物透明度为0.5  
            for (int i = 0; i < hits.Length; i++)
            {
                _tempRenderer = hits[i].collider.gameObject.GetComponent<Renderer>();
                _ObstacleCollider.Add(_tempRenderer);
                SetMaterialsAlpha(_tempRenderer, 0.5f);
            }


        }// 恢复障碍物透明度为1  
        else
        {
            for (int i = 0; i < _ObstacleCollider.Count; i++)
            {
                _tempRenderer = _ObstacleCollider[i];
                SetMaterialsAlpha(_tempRenderer, 1f);
            }
        }

    }  
    void LateUpdate()
    {
        //一些判断
        if (target == null)
            return;
        if (Camera.main == null)
            return;
        //目标的坐标
        Vector3 targetPos = target.transform.position;
        //用三角函数计算相机位置
        Vector3 cameraPos;
        float d = distance * Mathf.Cos(roll * Mathf.PI * 2 / 360);
        float height = distance * Mathf.Sin(roll* Mathf.PI * 2 / 360);
        cameraPos.x = targetPos.x;
        cameraPos.z = targetPos.z - d ;
        cameraPos.y = targetPos.y + height;
        Camera.main.transform.position = cameraPos;
        //对准目标
        //Camera.main.transform.LookAt(target.transform);
        //调整距离
        Zoom();
    }


    //调整距离
    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distance > minDistance)
                distance -= zoomSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distance < maxDistance)
                distance += zoomSpeed;
        }
    }

    // 修改障碍物的透明度  
    private void SetMaterialsAlpha(Renderer _renderer, float Transpa)
    {
        // 一个游戏物体的某个部分都可以有多个材质球  
        int materialsCount = _renderer.materials.Length;
        for (int i = 0; i < materialsCount; i++)
        {

            // 获取当前材质球颜色  
            Color color = _renderer.materials[i].color;

            // 设置透明度（0--1）  
            color.a = Transpa;

            // 设置当前材质球颜色（游戏物体上右键SelectShader可以看见属性名字为_Color）  
            _renderer.materials[i].color=color;
        }

    }  
}