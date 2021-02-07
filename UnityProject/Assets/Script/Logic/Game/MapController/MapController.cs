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
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_player != null) m_player.OnUpdate(deltaTime, unscaledDeltaTime);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnUpdate(deltaTime, unscaledDeltaTime);
        }
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
    }

    public void OnReset()
    {
        if (m_player != null) m_player.OnReset();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnReset();
        }
    }

    public void OnGameStart()
    {
        if (m_player != null) m_player.OnGameStart();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnGameStart();
        }
    }

    public void OnPause(bool pause)
    {
        if (m_player != null) m_player.OnPause(pause);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnPause(pause);
        }
    }

    public void OnGameOver(GameOverType gameOverType)
    {
        if (m_player != null) m_player.OnGameOver(gameOverType);
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnGameOver(gameOverType);
        }
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
        int count = Physics.OverlapSphereNonAlloc(pos, radius, hits, 1 << 16 & 17);
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

    #endregion
}