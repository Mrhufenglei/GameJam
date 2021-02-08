//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:41:18
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
public abstract class BaseMember : MonoBehaviour, IGameController
{
    public MemberType m_memberType = MemberType.Enemy;
    public MemberState m_memberState = MemberState.Show;

    [Header("Data Setting")] public MemberData m_memberData = new MemberData();

    [Header("Collider")] public CharacterController m_character;
    [SerializeField] [Label] protected Vector3 m_playerVelocity;

    [Header("Nav Agent")] public NavMeshAgent m_agent;
    [SerializeField] [Label] private Vector3 m_destination;

    [Header("Attributes")] [SerializeField] [Label]
    private float m_hp = 0;


    public float HP
    {
        get { return m_hp; }
    }

    public void Move(float montionX, float montionY, float montionZ)
    {
        if (m_memberData == null) return;
        if (m_character != null)
        {
            m_playerVelocity = new Vector3(montionX * m_memberData.m_speedX, montionY, montionZ * m_memberData.m_speedY);
        }
    }

    #region Controller

    public virtual void OnInit()
    {
        ResetAttributes();
        SwtichState(MemberState.Show);
    }

    public virtual void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        OnUpdateState(deltaTime, unscaledDeltaTime, m_memberState);
    }

    public virtual void OnDeInit()
    {
    }

    public virtual void OnReset()
    {
    }

    public virtual void OnGameStart()
    {
        SwtichState(MemberState.Idle);
        //创建HpUI
        GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_CREATEHP, this);
    }

    public virtual void OnPause(bool pause)
    {
    }

    public virtual void OnGameOver(GameOverType gameOverType)
    {
    }

    #endregion

    #region OnHit

    public void OnHit(float attack)
    {
        m_hp -= attack;
        if (m_hp <= 0)
        {
            m_hp = 0;
            //死亡            
            SwtichState(MemberState.Death);
            //检查是否结束
            GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_CHECKISOVERFORMEMBERS, null);
        }
    }

    #endregion

    private void ResetAttributes()
    {
        m_hp = m_memberData.m_hpMax;
    }

    public virtual void OnControllerColliderHit(ControllerColliderHit collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Debug.LogFormat("碰到墙了 {0}", collider.gameObject.name);
        }
    }

    public void SwtichState(MemberState state)
    {
        OnEnterState(state);
        m_memberState = state;
    }

    protected abstract void OnEnterState(MemberState state);
    protected abstract void OnUpdateState(float deltaTime, float unscaledDeltaTime, MemberState state);

    #region Agent

    /// <summary>
    /// 为代理赋予终点位置
    /// </summary>
    /// <param name="destination"></param>
    public void SetAgentDestination(Vector3 destination)
    {
        if (m_agent == null)
            return;
        try
        {
            m_agent.SetDestination(destination);
            m_destination = destination;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public List<Vector3> GetPath(Vector3 destination)
    {
        List<Vector3> path = new List<Vector3>();
        if (m_agent == null)
            return path;
        SetAgentDestination(destination);
        path = m_agent.path.corners.ToList();
        return path;
    }

    public Vector3 GetAgentDestination()
    {
        return m_destination;
    }

    /// <summary>
    /// 播放代理
    /// </summary>
    public void PlayAgent()
    {
        if (m_agent == null)
            return;
        if (m_agent.isOnNavMesh) m_agent.isStopped = false;
    }

    /// <summary>
    /// 停止代理
    /// </summary>
    public void StopAgent()
    {
        if (m_agent == null)
            return;
        if (m_agent.isOnNavMesh) m_agent.isStopped = true;
    }

    /// <summary>
    /// 代理移动速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetAgentSpeed(float speed)
    {
        if (m_agent == null)
            return;
        m_agent.speed = speed;
    }

    /// <summary>
    /// 代理区域遮罩赋值
    /// </summary>
    /// <param name="areaMask"></param>
    public void SetAreaMask(int areaMask)
    {
        if (m_agent == null) return;
        m_agent.areaMask = areaMask;
    }

    /// <summary>
    /// 代理开关
    /// </summary>
    /// <param name="enable"></param>
    public void SetAgentEnable(bool enable)
    {
        if (m_agent == null) return;
        m_agent.enabled = enable;
    }

    /// <summary>
    /// 设置代理的移动优先级
    /// </summary>
    /// <param name="agentPriority"></param>
    public void SetAgentPriority(int agentPriority)
    {
        if (m_agent == null) return;
        m_agent.avoidancePriority = agentPriority;
    }

    #endregion
}