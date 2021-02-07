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
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public void OnDeInit()
    {
    }

    public void OnReset()
    {
    }

    public void OnGameStart()
    {
    }

    public void OnPause(bool pause)
    {
    }

    public void OnGameOver(GameOverType gameOverType)
    {
    }

    #endregion
}