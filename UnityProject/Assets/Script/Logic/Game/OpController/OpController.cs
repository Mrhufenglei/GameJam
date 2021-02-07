//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 14:48:35
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class OpController : MonoBehaviour, IGameController
{
    #region IGameController

    public void OnInit()
    {
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (GameController.Builder.m_mapController.m_player == null) return;

        float horizontal = Input.GetAxis("Horizontal")*deltaTime;
        float vertical = Input.GetAxis("Vertical")*deltaTime;
        SetHorizontalAddVertical(horizontal,vertical);
    }

    public void SetHorizontalAddVertical(float h, float v)
    {
        GameController.Builder.m_mapController.m_player.Move(h,0,v);
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