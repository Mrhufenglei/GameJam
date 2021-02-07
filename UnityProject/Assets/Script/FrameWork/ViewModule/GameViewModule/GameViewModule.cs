//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:32:28
//
//----------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class GameViewModule  : IViewModule
{
    public GameObject m_gameObject;
    
    #region IViewModule
    public int GetName()
    {
        return (int) ViewName.GameViewModule;
    }

    public GameObject OnCreate()
    {
        m_gameObject = GameApp.UI.Pool.m_gameUI;
        return m_gameObject;
    }

    public void OnOpen(object data)
    {
       var dic =ViewTools.CollectAllGameObjects(m_gameObject);
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

    private void SetOpHorizontalAddVertical()
    {
        //
        float h = 1;
        float v = 1;
        GameController.Builder.m_opController.SetHorizontalAddVertical(h,v);
    }
}