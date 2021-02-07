using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ExplosionEfx;
    public Rigidbody m_rigidbody;

    public float ExplosionDelay = 3f;

    public bool m_isGround = false;
    private float mTimer = 0;

    public float m_radius = 2;
    public float m_attack = 30;

    bool isExplosion = false;

    void Start()
    {
        mTimer = ExplosionDelay;
        isExplosion = false;
        m_isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        mTimer -= Time.deltaTime;
        if (!isExplosion && mTimer <= 0)
        {
            isExplosion = true;
            var efx = GameObject.Instantiate(ExplosionEfx);
            efx.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
            ToHit();
        }
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