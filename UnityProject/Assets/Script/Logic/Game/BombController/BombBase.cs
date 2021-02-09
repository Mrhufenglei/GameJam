using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator m_animator;
    [Label] public Vector3 m_endPos = Vector3.zero;
    [Header("Fly Setting")] public float m_flySpeed = 10;

    [Header("Attributes")] public float m_radius = 2;
    public float m_attack = 30;

    [Header("Setting")] public Rigidbody m_rigidbody;
    public Collider m_collider;

    [Label] public bool m_isGround = false;
    public State m_state = State.Fly;
    [Header("Time")] public float m_duration = 6;
    public float m_currentTime = 0;


    [Header("Bomb Prefab")] public GameObject m_bombPrefab;
    [Header("Destroy Time")] public float m_destroyDuration = 6;
    public float m_currentDestroyTime = 0;

    public enum State
    {
        Fly,
        Wait,
        Bomb,
    }

    public virtual void OnInit()
    {
        m_currentTime = 0;
        m_currentDestroyTime = 0;
        m_isGround = false;
        if (m_rigidbody != null) m_rigidbody.isKinematic = true;
        if (m_collider != null) m_collider.enabled = false;
    }

    public virtual void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        switch (m_state)
        {
            case State.Fly:
            {
                transform.up = Vector3.Slerp(transform.up, m_endPos - gameObject.transform.position,
                    0.5f / Vector3.Distance(transform.position, m_endPos));
                transform.position += transform.up * m_flySpeed * deltaTime;

                var d1 = Vector3.Distance(transform.position, m_endPos);
                if (d1 <= 0.5f)
                {
                    if (m_rigidbody != null) m_rigidbody.isKinematic = false;
                    if (m_collider != null) m_collider.enabled = true;
                    m_state = State.Wait;
                }
            }
                break;
            case State.Wait:
            {
                m_currentTime += deltaTime;
                if (m_currentTime >= m_duration)
                {
                    m_currentTime = 0;
                    CreateBomb();
                    m_state = State.Bomb;
                }
            }
                break;
            case State.Bomb:
            {
                m_currentDestroyTime += deltaTime;
                OnBombUpdate(deltaTime, unscaledDeltaTime);
                if (m_currentDestroyTime >= m_destroyDuration)
                {
                    GameController.Builder.m_mapController.m_bombController.DestroyBomb(this);
                    m_currentDestroyTime = 0;
                }
            }
                break;
        }
    }

    public void SetEndPos(Vector3 endPos)
    {
        m_endPos = endPos;
    }

    private void CreateBomb()
    {
        var efx = GameObject.Instantiate(m_bombPrefab);
        efx.transform.position = this.transform.position;
        this.gameObject.SetActive(false);
        OnBombInit(efx);
        ToHit();
    }

    protected virtual void OnBombUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    protected virtual void OnBombInit(GameObject obj)
    {
    }


    public virtual void OnDeInit()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (m_isGround == true) return;
        if (m_rigidbody == null) return;
        if (other.gameObject.layer == 12)
        {
            m_rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            m_rigidbody.Sleep();
            m_state = State.Wait;
            if (m_animator != null) m_animator.SetTrigger("Bomb");
            m_isGround = true;
        }
    }

    private void ToHit()
    {
        if (GameController.Builder != null &&
            GameController.Builder.m_mapController != null)
        {
            var members = GameController.Builder.m_mapController.FindMembers(transform.position, m_radius);
            for (int i = 0; i < members.Count; i++)
            {
                var member = members[i];
                if (member == null) continue;
                member.ToHit(m_attack);
            }
        }
    }
}