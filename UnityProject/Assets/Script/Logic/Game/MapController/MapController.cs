//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 14:48:44
//
//----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MapController : MonoBehaviour, IGameController
{
    public BaseMember m_player;
    public List<BaseMember> m_enemys = new List<BaseMember>(10);
    public Dictionary<int, BaseMember> m_members = new Dictionary<int, BaseMember>();

    public BombController m_bombController;
    #region IGameController

    public void OnInit()
    {
        if (m_player != null)
        {
            m_members.Add(m_player.gameObject.GetInstanceID(), m_player);
            m_player.OnInit();
        }

        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null)
            {
                m_members.Add(enemy.gameObject.GetInstanceID(), enemy);
                enemy.OnInit();
            }
        }
        if(m_bombController!=null)m_bombController.OnInit();
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_player != null) m_player.OnUpdate(deltaTime, unscaledDeltaTime);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnUpdate(deltaTime, unscaledDeltaTime);
        }
        if(m_bombController!=null)m_bombController.OnUpdate(deltaTime, unscaledDeltaTime);

    }

    public void OnDeInit()
    {
        if (m_player != null) m_player.OnDeInit();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnDeInit();
        }

        m_members.Clear();
        
        if(m_bombController!=null)m_bombController.OnDeInit();

    }

    public void OnReset()
    {
        if (m_player != null) m_player.OnReset();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnReset();
        }
        if(m_bombController!=null)m_bombController.OnReset();

    }

    public void OnGameStart()
    {
        if (m_player != null) m_player.OnGameStart();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnGameStart();
        }
        if(m_bombController!=null)m_bombController.OnGameStart();

    }

    public void OnPause(bool pause)
    {
        if (m_player != null) m_player.OnPause(pause);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnPause(pause);
        }
        if(m_bombController!=null)m_bombController.OnPause(pause);

    }

    public void OnGameOver(GameOverType gameOverType)
    {
        if (m_player != null) m_player.OnGameOver(gameOverType);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnGameOver(gameOverType);
        }
        if(m_bombController!=null)m_bombController.OnGameOver(gameOverType);

    }

    #endregion

    #region Member

    /// <summary>
    /// 查找成员
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public List<BaseMember> FindMembers(Vector3 pos, float radius)
    {
        List<BaseMember> members = new List<BaseMember>();
        Collider[] hits = new Collider[100];
        int count = Physics.OverlapSphereNonAlloc(pos, radius, hits, 1 << LayerManager.Player | 1 << LayerManager.Enemy,
            QueryTriggerInteraction.Collide);

        if (count == 0) return members;
        for (int i = 0; i < count; i++)
        {
            var collider = hits[i];
            if (collider == null) continue;
            m_members.TryGetValue(collider.gameObject.GetInstanceID(), out var member);
            if (member == null) continue;
            members.Add(member);
        }

        return members;
    }


    /// <summary>
    /// 检查是否游戏可以结束
    /// </summary>
    public void CheckIsOverForMembers()
    {
        if (m_player != null && m_player.m_memberState == MemberState.Death)
        {
            GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_FAIL, null);
            return;
        }

        if (m_enemys != null)
        {
            int deathCount = 0;
            for (int i = 0; i < m_enemys.Count; i++)
            {
                var member = m_enemys[i];
                if (member == null) continue;
                if (member.m_memberState == MemberState.Death)
                {
                    deathCount++;
                }
            }

            if (deathCount == m_enemys.Count)
            {
                GameApp.Event.DispatchNow(LocalMessageName.CC_GAME_WIN, null);
            }
        }
    }

    #endregion

    #region

    /// <summary>
    /// 查找成员
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public List<BombBase> FindBombs(Vector3 pos, float radius)
    {
        List<BombBase> bombBases = new List<BombBase>();
        Collider[] hits = new Collider[100];
        int count = Physics.OverlapSphereNonAlloc(pos, radius, hits, 1 << LayerManager.Bomb,
            QueryTriggerInteraction.Collide);

        if (count == 0) return bombBases;
        for (int i = 0; i < count; i++)
        {
            var collider = hits[i];
            if (collider == null) continue;
            m_members.TryGetValue(collider.gameObject.GetInstanceID(), out var member);
            if (member == null) continue;
            // bombBases.Add(member);
        }

        return bombBases;
    }

    #endregion
}