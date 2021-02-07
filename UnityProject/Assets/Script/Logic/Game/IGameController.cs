//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 14:41:16
//
//----------------------------------------------------------------

using UnityEditor;

/// <summary>
/// 
/// </summary>
public interface IGameController
{
    void OnInit();
    void OnUpdate(float deltaTime, float unscaledDeltaTime);
    void OnDeInit();
    void OnReset();
    void OnGameStart();
    void OnPause(bool pause);
    void OnGameOver(GameOverType gameOverType);
}