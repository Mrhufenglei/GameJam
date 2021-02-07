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
public class GameViewModule : IViewModule
{
    public GameObject m_gameObject;

    public UIHPScroll m_scroll;

    public ScrollCircle m_joy;

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
        var dic = ViewTools.CollectAllGameObjects(m_gameObject);
        m_scroll = dic["HpScroll"].GetComponent<UIHPScroll>();
        if (m_scroll != null) m_scroll.OnOpen(data);

        m_joy = dic["MoveJoy"].GetComponentInChildren<ScrollCircle>();
        if(m_joy!= null)
        {
            ScrollCircle.On_JoyTouchStart += OnMoveStart;
            ScrollCircle.On_JoyTouching += OnMoving;
            ScrollCircle.On_JoyTouchEnd += OnMoveEnd;
        }
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_scroll != null) m_scroll.OnUpdate(deltaTime, unscaledDeltaTime);
    }

    public void OnClose()
    {
        if (m_scroll != null) m_scroll.OnClose();

        if(m_joy!= null)
        {
            ScrollCircle.On_JoyTouchStart -= OnMoveStart;
            ScrollCircle.On_JoyTouching -= OnMoving;
            ScrollCircle.On_JoyTouchEnd -= OnMoveEnd;
        }
    }

    public void RegisterEvents(EventSystemManager manager)
    {
    }

    public void UnRegisterEvents(EventSystemManager manager)
    {
    }

    #endregion

    private bool isMoving = false;
    public void OnMoveStart(JoyData data)
    {
        isMoving = true;
    }

    public void OnMoveEnd(JoyData data)
    {
        isMoving = false;
        GameController.Builder.m_opController.SetHorizontalAddVertical(0, 0);
    }


    public void OnMoving(JoyData data)
    {

        GameController.Builder.m_opController.SetHorizontalAddVertical(data.direction.x, data.direction.z);
    }

    private void SetOpHorizontalAddVertical()
    {
        //
        float h = 1;
        float v = 1;
        GameController.Builder.m_opController.SetHorizontalAddVertical(h, v);
    }
}