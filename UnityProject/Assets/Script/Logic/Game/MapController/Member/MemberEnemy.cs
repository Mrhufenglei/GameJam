//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:43:38
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 
/// </summary>
public class MemberEnemy : BaseMember
{
    public BombBase m_targetBomb;

    public override void OnControllerColliderHit(ControllerColliderHit collider)
    {
        base.OnControllerColliderHit(collider);

        if (collider.gameObject.layer == 9)
        {
            Debug.LogFormat("碰到玩家了 {0}", collider.gameObject.name);
            return;
        }

        if (collider.gameObject.layer == 10)
        {
            Debug.LogFormat("碰到人怪物了 {0}", collider.gameObject.name);
            return;
        }

        if (collider.gameObject.layer == 11)
        {
            Debug.LogFormat("碰到炸弹了 {0}", collider.gameObject.name);
            Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 dir = collider.transform.position - transform.position;
                float force = 10f;
                dir = dir.normalized * force;
                rigidbody.AddForce(dir, ForceMode.Impulse);
            }
            if (m_targetBomb != null && m_targetBomb.gameObject == collider.gameObject)
            {
                m_isWait = true;
                StopAgent();
            }

            return;
        }
    }

    protected override void OnEnterState(MemberState state)
    {
        switch (state)
        {
            case MemberState.Show:
                break;
            case MemberState.Idle:
                StopAgent();
                break;
            case MemberState.Run:
                m_time = 0;
                m_isWait = false;
                break;
            case MemberState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private bool m_isWait = false;
    private float m_time = 0;

    protected override void OnUpdateState(float deltaTime, float unscaledDeltaTime, MemberState state)
    {
        switch (state)
        {
            case MemberState.Show:
                break;
            case MemberState.Idle:
            {
                var bombs = GameController.Builder.m_mapController.FindBombs(this.transform.position, 2);
                if (bombs.Count > 0)
                {
                    // m_targetBomb = bombs[(int) Random.Range(0, bombs.Count)];

                    m_time = 0;
                    m_targetBomb = bombs[0];

                    if (m_targetBomb != null)
                    {
                        PlayAgent();
                        SetAgentDestination(m_targetBomb.transform.position);
                        SwtichState(MemberState.Run);
                    }
                }
            }
                break;
            case MemberState.Run:
            {
                if (m_isWait)
                {
                    m_time += deltaTime;
                    if (m_time >= 0.5f)
                    {
                        SwtichState(MemberState.Idle);
                        m_isWait = false;
                    }
                }
                else
                {
                    if (m_targetBomb != null)
                    {
                        if (Vector3.Distance(m_targetBomb.transform.position, GetAgentDestination()) >= 0.5f)
                        {
                            SetAgentDestination(m_targetBomb.transform.position);
                        }
                        if (Vector3.Distance(m_targetBomb.transform.position, transform.transform.position) <= 1f)
                        {
                            m_isWait = true;
                            StopAgent();
                        }
                    }
                    else
                    {
                        m_isWait = true;
                        StopAgent();
                    }
                }
            }
                break;
            case MemberState.Death:
                break;
        }
    }
}