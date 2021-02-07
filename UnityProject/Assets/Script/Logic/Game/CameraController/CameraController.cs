//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 14:48:59
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CameraController : MonoBehaviour,IGameController
{
    public Camera m_camera;
    
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