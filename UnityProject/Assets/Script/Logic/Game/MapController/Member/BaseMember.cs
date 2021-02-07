//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:41:18
//
//----------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class BaseMember : MonoBehaviour, IGameController
{
    public MemberType m_memberType = MemberType.Enemy;
    public MemberState m_memberState = MemberState.Idle;

    [Header("Data Setting")] public MemberData m_memberData = new MemberData();

    [Header("Collider")] public CharacterController m_collider;
    [SerializeField] [Label] private Vector3 m_playerVelocity;

    [Header("Attributes")] [SerializeField] [Label]
    private float m_hp = 0;

    public float HP
    {
        get { return m_hp; }
    }

    public void Move(float montionX, float montionY, float montionZ)
    {
        if (m_memberData == null) return;
        if (m_collider != null)
        {
            m_playerVelocity = new Vector3(montionX * m_memberData.m_speedX, montionY, montionZ * m_memberData.m_speedY);
        }
    }

    #region Controller

    public virtual void OnInit()
    {
        ResetAttributes();
        SwtichState(MemberState.Idle);
    }

    public virtual void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        var groundedPlayer = m_collider.isGrounded;
        if (groundedPlayer && m_playerVelocity.y < 0)
        {
            m_playerVelocity.y = 0f;
        }

        m_playerVelocity.y += m_memberData.m_gravityValue;
        var vaule = m_playerVelocity * deltaTime;
        m_collider.Move(vaule);
    }

    public virtual void OnDeInit()
    {
    }

    public virtual void OnReset()
    {
    }

    public virtual void OnGameStart()
    {
        SwtichState(MemberState.Run);
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
        switch (state)
        {
            case MemberState.Idle:
                break;
            case MemberState.Run:
                break;
            case MemberState.Death:
                break;
        }

        m_memberState = state;
    }
}