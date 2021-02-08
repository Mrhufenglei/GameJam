//-----------------------------------------------------------------
//
//              Maggic @  2021-02-08 14:13:50
//
//----------------------------------------------------------------

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
public class BaseMap : MonoBehaviour, IGameController
{
    public NavMeshSurface m_navMeshSource;

    #region IGameController

    public void OnInit()
    {
        BuildNavMeshs();
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
    
    private void BuildNavMeshs()
    {
        if (m_navMeshSource == null) return;
        m_navMeshSource.collectObjects = CollectObjects.Children;
        m_navMeshSource.BuildNavMesh();
    }
}