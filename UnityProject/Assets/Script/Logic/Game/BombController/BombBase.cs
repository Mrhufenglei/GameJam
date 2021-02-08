using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Attributes")] public float m_radius = 2;
    public float m_attack = 30;

    [Header("Setting")] public Rigidbody m_rigidbody;
    [Label] public bool m_isGround = false;
    public State m_state = State.Wait;
    [Header("Time")] public float m_duration = 6;
    public float m_currentTime = 0;


    [Header("Bomb Prefab")] public GameObject m_bombPrefab;
    [Header("Destroy Time")] public float m_destroyDuration = 6;
    public float m_currentDestroyTime = 0;

    public enum State
    {
        Wait,
        Bomb,
    }

    public virtual void OnInit()
    {
        m_currentTime = 0;
        m_currentDestroyTime = 0;
        m_isGround = false;
    }

    public virtual void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        switch (m_state)
        {
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
                OnBombUpdate(deltaTime,unscaledDeltaTime);
                if (m_currentDestroyTime >= m_destroyDuration)
                {
                    GameController.Builder.m_mapController.m_bombController.DestroyBomb(this);
                    m_currentDestroyTime = 0;
                }
            }
                break;
        }
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
                member.OnHit(m_attack);
            }
        }
    }
}