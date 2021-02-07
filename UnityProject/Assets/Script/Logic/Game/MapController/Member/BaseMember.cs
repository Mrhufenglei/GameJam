//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:41:18
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class BaseMember : MonoBehaviour, IGameController
{
    public MemberType m_memberType = MemberType.Enemy;

    [Header("Data Setting")] public MemberData m_memberData = new MemberData();

    [Header("Collider")] public CharacterController m_collider;
    [SerializeField] [Label] private Vector3 m_playerVelocity;

    public void Move(float montionX, float montionY, float montionZ)
    {
        if (m_memberData == null) return;
        if (m_collider != null)
        {
            m_playerVelocity = new Vector3(montionX * m_memberData.m_speedX, montionY, montionZ * m_memberData.m_speedY);
            // m_collider.Move(new Vector3(montionX * m_memberData.m_speedX, montionY, montionZ * m_memberData.m_speedY));
        }
    }

    #region Controller

    public virtual void OnInit()
    {
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
    }

    public virtual void OnPause(bool pause)
    {
    }

    public virtual void OnGameOver(GameOverType gameOverType)
    {
    }

    #endregion


    public virtual void OnControllerColliderHit(ControllerColliderHit collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Debug.LogFormat("碰到墙了 {0}", collider.gameObject.name);
            return;
        }
    }
}