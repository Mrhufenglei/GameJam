//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:41:18
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BaseMember : MonoBehaviour
{
    public MemberType m_memberType = MemberType.Enemy;

    [Header("Collider")]
    public CharacterController m_collider;

    [Header("Move Speed")]
    public float m_speedX = 10;
    public float m_speedY = 10;

    public void Move(float montionX, float montionY, float montionZ)
    {
        if (m_collider != null)
        {
            m_collider.Move(new Vector3(montionX * m_speedX, montionY, montionZ * m_speedY));
        }
    }
}