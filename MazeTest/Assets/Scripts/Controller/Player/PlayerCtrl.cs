using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerCtrl : MonoBehaviour 
{
    public GameStateMachine CurrStateMachine = new GameStateMachine();
    public enum PlayerState 
    {
        idle=0,
        run,
        attack,
        dead,
        findPath,
        skillQ,
        skillW,
        skillE,
        skillR
    }
    private PlayerStatus m_RoleStatus;
    private AudioSource curAudio;
    private AudioSource bottomAudio;
    private GameObject hitEffect;
    private GameObject drinkRedEffect;
    private GameObject drinkBlueEffect;
    private GameObject lvUpEfect;

    public List<GameObject> EnemyPool;//敌人池

    
    public float Velocity = 0.5f; //角色移动速度
    
    public float RotateVelocity = 0.5f; //角色转身速度
    public float gravity = 20f;//重力
    public MeleeWeaponTrail Trail;//刀光物体
    //动画组件
    public Animator PlayerAnimator { get; private set; }
    public CharacterController PlayerCharacterCtrl { get; private set; }
    public NavMeshAgent PlayerMeshAgent { get; private set; }
    public Vector3 hitPos{get;private set;}
    public GameObject hitObj { get; set; }
    public bool IsFindingPath{ get; private set;}
    public Skill CurSkill { get; set; }
    void Awake() 
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerMeshAgent = GetComponent<NavMeshAgent>();
        PlayerMeshAgent.enabled = false;
        m_RoleStatus = GetComponent<PlayerStatus>();
        PlayerCharacterCtrl = GetComponent<CharacterController>();
        curAudio = GetComponent<AudioSource>();
        bottomAudio = transform.Find("BottomAudio").GetComponent<AudioSource>();
        hitEffect = EffectManager.Instance.GetEffect("PlayerHurt");
        drinkRedEffect = EffectManager.Instance.GetEffect("RedPotion");
        drinkBlueEffect = EffectManager.Instance.GetEffect("BluePotion");
        lvUpEfect = EffectManager.Instance.GetEffect("LevelUp");
        EnemyPool = new List<GameObject>();
    }
    void Start() 
    {
        //注册状态
        CurrStateMachine.RegistState(new PlayerIdleState(this));
        CurrStateMachine.RegistState(new PlayerRunState(this));
        CurrStateMachine.RegistState(new PlayerAttackState(this));
        CurrStateMachine.RegistState(new PlayerDieState(this));
        CurrStateMachine.RegistState(new PlayerSkillQState(this));
        CurrStateMachine.RegistState(new PlayerSkillWState(this));
        CurrStateMachine.RegistState(new PlayerSkillEState(this));
        CurrStateMachine.RegistState(new PlayerSkillRState(this));
        CurrStateMachine.RegistState(new PlayerFindPathState(this));
        CurrStateMachine.SwitchState((int)PlayerState.idle);
        //关闭刀光
        Trail.Emit = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))
            {
                return;
            }
            LayerMask layerMask = 1 << 10 | 1 << 8;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mhit;
            if (Physics.Raycast(ray, out mhit, 1000f, layerMask.value))
            {
                hitObj = mhit.collider.gameObject;
                hitPos=new Vector3(mhit.point.x,transform.position.y,mhit.point.z);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_RoleStatus.SkillQ != null && !m_RoleStatus.SkillQ.IsCooling && m_RoleStatus.status.MpCur >= m_RoleStatus.SkillQ.Magic)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit mhit;
                if (Physics.Raycast(ray, out mhit, 1000f))
                {
                    hitObj = mhit.collider.gameObject;
                    hitPos = mhit.point;
                }
                m_RoleStatus.status.MpCur -= m_RoleStatus.SkillQ.Magic;
                m_RoleStatus.StatusChanged();
                m_RoleStatus.SkillQ.SkillStart();
                m_RoleStatus.SkillCooling();
                CurSkill = m_RoleStatus.SkillQ.CurSkill;
                CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.skillQ);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (m_RoleStatus.SkillW != null && !m_RoleStatus.SkillW.IsCooling && m_RoleStatus.status.MpCur >= m_RoleStatus.SkillW.Magic)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit mhit;
                if (Physics.Raycast(ray, out mhit, 1000f))
                {
                    hitObj = mhit.collider.gameObject;
                    hitPos = mhit.point;
                }
                m_RoleStatus.status.MpCur -= m_RoleStatus.SkillW.Magic;
                m_RoleStatus.StatusChanged();
                m_RoleStatus.SkillW.SkillStart();
                m_RoleStatus.SkillCooling();
                CurSkill = m_RoleStatus.SkillW.CurSkill;
                CurrStateMachine.SwitchState((int)PlayerCtrl.PlayerState.skillW);
            }
        }
        if (CurrStateMachine != null)
        {
            CurrStateMachine.OnUpdate();
        }
    }
    //普攻动画事件回调方法
    void NormalAtk()
    {
        if (hitObj != null)
        {
            GameObject enemy = hitObj;
            if (enemy.layer == 8)
            {
                enemy.GetComponent<EnemyCtrl>().GetHurt(m_RoleStatus.status.AtkCur);
            }
        }
    }
    //技能动画事件
    void SkillEvent()
    {
        CurSkill.SkillCheck();
    }
    void FootR()
    {
        bottomAudio.Play();
    }
    void FootL()
    {
        bottomAudio.Play();
    }
    //角色受伤方法
    public void Hurt(int damage)
    {
        GameObject effectObj = ObjectPool.Instance.GetObj(hitEffect);
        effectObj.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        m_RoleStatus.status.HpCur -= (damage - m_RoleStatus.status.DefCur);
        m_RoleStatus.StatusChanged();
        if (m_RoleStatus.status.HpCur <= 0)
        {
            CurrStateMachine.SwitchState((int)PlayerState.dead);
        }
    }
    //角色获得经验
    public void GetExp(double exp)
    {
        if (CurrStateMachine.IsInState((int)PlayerState.dead))
        {
            return;
        }
        m_RoleStatus.Exp += exp;

        if (m_RoleStatus.Exp > m_RoleStatus.ExpMax)
        {
            PlayCurAudio(AudioManager.Instance.GetAudio("LevelUp"));
            double deltaExp = m_RoleStatus.Exp - m_RoleStatus.ExpMax;
            m_RoleStatus.Exp = 0;
            m_RoleStatus.Lv++;
            m_RoleStatus.RecoverStatus();
            GameObject obj = ObjectPool.Instance.GetObj(lvUpEfect);
            obj.transform.SetParent(transform);
            obj.transform.position = new Vector3(transform.position.x, transform.position.y+1.8f, transform.position.z);
            GetExp(deltaExp);
        }
    }
    /// <summary>
    /// 角色转身方法
    /// </summary>
    /// <param name="hitPos">点击坐标</param>
    /// <param name="RotateVelocity">转身速度（秒）</param>
    public void PlayerRotate(Vector3 direction, float RotateVelocity)
    {
        Quaternion playerRotation = Quaternion.LookRotation(direction);
        float y = playerRotation.eulerAngles.y;
        playerRotation = Quaternion.Euler(0, y, 0);
        if (y > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * (5.0f / RotateVelocity));
    }
    //播放音频
    public void PlayCurAudio(AudioClip clip)
    {
        curAudio.clip = clip;
        curAudio.Play();
    }
    //开启寻路
    public void StartFindPath(Vector3 des)
    {
        hitPos = des;
        CurrStateMachine.SwitchState((int)PlayerState.findPath);
    }
    //喝药水
    public void DrinkPotion(Consumable item)
    {
        GameObject obj;
        if (item.StatusNode.HpMax > 0)
        {
            obj = ObjectPool.Instance.GetObj(drinkRedEffect);
            obj.transform.SetParent(transform);
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
        else
        {
            obj = ObjectPool.Instance.GetObj(drinkBlueEffect);
            obj.transform.SetParent(transform);
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
        m_RoleStatus.ConsumeItem(item);
        PlayCurAudio( AudioManager.Instance.GetAudio("DrinkPotion"));
    }
    public void ClearHitPos()
    {
        this.hitObj = null;
    }
   
}
