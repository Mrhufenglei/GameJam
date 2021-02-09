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
    public Animator m_animator;
    
    #region IGameController

    public void OnInit()
    {
        if(m_animator!=null)m_animator.SetTrigger("Idle");
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_BombHit,OnEventBombHit);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public void OnDeInit()
    {
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_BombHit,OnEventBombHit);

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

    #region Event

    private void OnEventBombHit(int type, object obj)
    {
        if(m_animator!=null)m_animator.SetTrigger("Shake");
    }

    #endregion
}