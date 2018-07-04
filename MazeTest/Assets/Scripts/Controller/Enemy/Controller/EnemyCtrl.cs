 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class  EnemyCtrl : MonoBehaviour 
{
    public enum EnemyState 
    {
        idle,
        chasing,
        attack,
        smasher,
        hurt,
        backHome,
        dead
    }
    protected BoxCollider boxcol;
    protected SphereCollider TriggerRange;
    protected AudioSource CurAudio;
    protected AudioSource BottomAudio;
    protected GameObject hitEffect;
    [HideInInspector]
    public EnemyStatus m_Status;
    [HideInInspector]
    public GameObject CurrentRoom;
    public GameStateMachine CurrStateMachine = new GameStateMachine();

    public float Velocity = 0.5f; //角色移动速度

    public float RotateVelocity = 0.5f; //角色转身速度

    public float AttackRange = 2.0f; //角色攻击范围

    public float DetectRange = 10.0f; //角色觉察范围

    public float GravityPower = 20.0f;//模拟重力
    public float FallProbality = 0.5f;
    [HideInInspector]
    public GameObject ItemBox;//掉落宝箱预设

    public Animator EnemyAnimator { get; private set; }
    public CharacterController EnemyCharacterCtrl { get; private set; }
    public GameObject CurPlayer{get; private set;}
    public bool IsInSight{get; private set;}
    public Vector3 originPosition{get; protected set;}
    public NavMeshAgent EnemyAgent { get; private set; }
    protected void Awake() 
    {
        m_Status = GetComponent<EnemyStatus>();
        EnemyAgent =GetComponent<NavMeshAgent>();

        EnemyAnimator = GetComponent<Animator>();
        render = GetComponentInChildren<Renderer>();
        oriColor = render.material.color;
        EnemyCharacterCtrl = GetComponent<CharacterController>();
        boxcol = this.gameObject.AddComponent<BoxCollider>();
        boxcol.center = EnemyCharacterCtrl.center;
        boxcol.size = new Vector3(EnemyCharacterCtrl.radius * 2.2f, EnemyCharacterCtrl.height, EnemyCharacterCtrl.radius * 2.2f);
        boxcol.isTrigger = true;
        TriggerRange = transform.Find("Trigger").GetComponent<SphereCollider>();
        CurAudio = GetComponent<AudioSource>();
        BottomAudio=transform.Find("BottomAudio").GetComponent<AudioSource>();
        CurPlayer = GameObject.FindWithTag("Player");
        if (gameObject.tag == "EnemyBoss")
        {
            hitEffect = EffectManager.Instance.GetEffect("BossHurt");
        }
        else
        {
            hitEffect = EffectManager.Instance.GetEffect("MonsterHurt");
        }
 
        ItemBox=Resources.Load<GameObject>("Prefabs/Prop/BoxClosed");
    }
	protected void Start () 
    {
        CurrStateMachine.RegistState(new EnemyIdleState(this));
        CurrStateMachine.RegistState(new EnemyChaseState(this));
        CurrStateMachine.RegistState(new EnemyBackHomeState(this));
        CurrStateMachine.RegistState(new EnemyAttackState(this));
        CurrStateMachine.RegistState(new EnemyHurtState(this));
        CurrStateMachine.RegistState(new EnemyDieState(this));
        CurrStateMachine.RegistState(new EnemySmasherState(this));
        CurrStateMachine.SwitchState((int)EnemyState.idle);
        CurPlayer.GetComponent<PlayerCtrl>().EnemyPool.Add(this.gameObject);
        originPosition = new Vector3(transform.position.x, 0, transform.position.z);
        TriggerRange.radius = DetectRange;
	}
	
	protected void Update () 
    {
        if(CurPlayer.GetComponent<PlayerCtrl>().CurrStateMachine.IsInState((int)(PlayerCtrl.PlayerState.dead)))
        {
            CurrStateMachine.SwitchState((int)EnemyState.idle);
        }
        CurrStateMachine.OnUpdate();
	}
    private Renderer render;
    private Color oriColor;
    void OnMouseEnter()
    {
        render.material.color = Color.red;
    }
    void OnMouseExit()
    {
        render.material.color = oriColor;
    }
    //普攻动画事件回调
    void NormalAtk()
    {
        if (Vector3.Distance(transform.position, CurPlayer.transform.position) < AttackRange)
        {
            CurPlayer.GetComponent<PlayerCtrl>().Hurt(m_Status.status.AtkCur);
        }
    }
    void FootR()
    {
        BottomAudio.Play();
    }
    void FootL()
    {
        BottomAudio.Play();
    }

    public virtual void GetHurt(int damage)
    {
        SetInSight(true);
        GameObject effectObj = ObjectPool.Instance.GetObj(hitEffect);
        effectObj.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        EnemyStatus hStatus = GetComponent<EnemyStatus>();
        hStatus.status.HpCur -= damage;
        if (hStatus.status.HpCur <= 0)
        {
            CurrStateMachine.SwitchState((int)EnemyCtrl.EnemyState.dead);
            hStatus.status.HpCur = 0;
            hStatus.Hurt();
        }
        else
        {
            hStatus.Hurt();
        }
    }
    public void  EnemyRotate(Quaternion playerRotation, float RotateVelocity)
    {
        playerRotation = Quaternion.Euler(0f, playerRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation,playerRotation, Time.deltaTime * (5.0f / RotateVelocity));
    }
    public virtual void SetInSight(bool inSight)
    {
        IsInSight = inSight;
    }
    //开启寻路
    public void FindPath(Vector3 des)
    {
        EnemyAgent.isStopped = false;
        EnemyAgent.acceleration = 1000000f;
        EnemyAgent.destination = des;
        EnemyAgent.speed = Velocity;
    }
    public void StopFindPath()
    {
        EnemyAgent.isStopped = true;
    }
}
