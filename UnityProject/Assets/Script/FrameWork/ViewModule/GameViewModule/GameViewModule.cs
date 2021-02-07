//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:32:28
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameViewModule  : IViewModule
{
    #region IViewModule
    public int GetName()
    {
        return (int) ViewName.GameViewModule;
    }

    public GameObject OnCreate()
    {
        return GameApp.UI.Pool.m_gameUI;
    }

    public void OnOpen(object data)
    {
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public void OnClose()
    {
    }

    public void RegisterEvents(EventSystemManager manager)
    {
    }

    public void UnRegisterEvents(EventSystemManager manager)
    {
    }

    #endregion
}