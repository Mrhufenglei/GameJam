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

    #region IGameController

    public void OnInit()
    {
        if (m_player != null) m_player.OnInit();
        for (int i = 0; i < m_enemys.Count; i++)
        {
            var enemy = m_enemys[i];
            if (enemy != null) enemy.OnInit();
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
}