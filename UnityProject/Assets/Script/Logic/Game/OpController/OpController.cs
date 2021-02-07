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
        GameController.Builder.m_mapController.m_player.Move(horizontal,0,vertical);
        // GameController.Builder.m_mapController.m_player.transform.position += new Vector3(horizontal, 0,vertical);
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