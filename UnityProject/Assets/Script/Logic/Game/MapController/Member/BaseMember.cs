﻿//-----------------------------------------------------------------
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

    [Header("Data Setting")]
    public MemberData m_memberData = new MemberData();
    
    [Header("Collider")]
    public CharacterController m_collider;

    public void Move(float montionX, float montionY, float montionZ)
    {
        if (m_memberData == null) return;
        if (m_collider != null)
        {
            m_collider.Move(new Vector3(montionX * m_memberData.m_speedX, montionY, montionZ * m_memberData.m_speedY));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}