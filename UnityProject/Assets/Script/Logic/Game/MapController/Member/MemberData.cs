//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 16:44:11
//
//----------------------------------------------------------------
using  System;
using UnityEngine;

[Serializable]
/// <summary>
/// 
/// </summary>
public class MemberData
{
    [Header("Physics Setting")]
    public float m_gravityValue = -9.81f;
    [Header("Attributes Setting")]
    public float m_hp = 100;
    [Header("Move Speed")]
    public float m_speedX = 10;
    public float m_speedY = 10;
}
