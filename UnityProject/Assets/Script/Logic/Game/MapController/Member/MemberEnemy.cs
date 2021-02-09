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

    [SerializeField] [Label] private bool m_isWait = false;
    [SerializeField] [Label] private float m_time = 0;

    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Debug.LogFormat("碰到墙了 {0}", collider.gameObject.name);
        }

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
                Vector3 dir = new Vector3(collider.transform.position.x, 0, collider.transform.position.z) -
                              new Vector3(transform.position.x, 0, transform.position.z);
                float force = 10;
                dir = dir.normalized * force;
                rigidbody.AddForce(dir, ForceMode.Impulse);
                // rigidbody.AddExplosionForce(100,transform.position,2,3.0f);
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
                //关闭显示
                gameObject.SetActive(false);
                //创建一个特效
                GameObject prefab = GameApp.Resources.Load<GameObject>("Prefab/Effect/Effect_Death");
                var obj = GameObject.Instantiate<GameObject>(prefab);
                obj.gameObject.transform.position = transform.position;
                GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_DESTROYHP, this);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

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
                        Vector3 dir = m_targetBomb.transform.position - transform.position;
                        dir = new Vector3(dir.x, 0, dir.z);
                        dir = dir.normalized * 1;
                        Vector3 pos = new Vector3(m_targetBomb.transform.position.x, 0, m_targetBomb.transform.position.z);
                        SetAgentDestination(pos + dir);
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
                        // if (Vector3.Distance(m_targetBomb.transform.position, GetAgentDestination()) >= 0.5f)
                        // {
                        //     SetAgentDestination(m_targetBomb.transform.position);
                        // }
                        m_time += deltaTime;
                        if (m_time >= 1)
                        {
                            SwtichState(MemberState.Idle);
                            m_isWait = false;
                            return;
                        }

                        Vector3 trans = new Vector3(transform.transform.position.x, 0, transform.transform.position.z);
                        if (Vector3.Distance(GetAgentDestination(), trans) <= 0.5f)
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