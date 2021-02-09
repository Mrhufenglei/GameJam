//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 15:43:01
//
//----------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MemberPlayer : BaseMember
{
    public void OnControllerColliderHit(ControllerColliderHit collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Debug.LogFormat("碰到墙了 {0}", collider.gameObject.name);
        }

        if (collider.gameObject.layer == 10)
        {
            Debug.LogFormat("碰到人怪物了 {0}", collider.gameObject.name);
            // CharacterController rigidbody = collider.gameObject.GetComponent<CharacterController>();
            // if (rigidbody != null)
            // {
            //     Vector3 dir = transform.position - collider.transform.position;
            //     float force = 10;
            //     dir = dir.normalized * force;
            //     rigidbody.v
            //     rigidbody.AddForce(dir, ForceMode.Force);
            // }
            return;
        }

        if (collider.gameObject.layer == 11)
        {
            Debug.LogFormat("碰到炸弹了 {0}", collider.gameObject.name);
            Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 dir = collider.transform.position - transform.position;
                float force = 2f;
                dir = dir.normalized * force;
                rigidbody.AddForce(dir, ForceMode.Impulse);
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
                break;
            case MemberState.Run:
                break;
            case MemberState.Death:
                //关闭显示
                gameObject.SetActive(false);
                //创建一个特效
                GameObject obj = GameApp.Resources.Load<GameObject>("Prefab/Effect/Effect_Death");
                obj.gameObject.transform.position = transform.position;

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
            case MemberState.Run:
                var groundedPlayer = m_character.isGrounded;
                if (groundedPlayer && m_playerVelocity.y < 0)
                {
                    m_playerVelocity.y = 0f;
                }

                m_playerVelocity.y += m_memberData.m_gravityValue;
                var vaule = m_playerVelocity * deltaTime;
                m_character.Move(vaule);
                break;
            case MemberState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public override void OnHit(float attack)
    {
        base.OnHit(attack);
        GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_PlayerHit);
    }
}