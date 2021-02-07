//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:22:11
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class LoadingViewModule: IViewModule
{
    private GameObject m_gameObject;

    public Image m_upObj;
    public Image m_downObj;
    public enum LoadingType
    {
        Null,
        Closed,
        Opened,
    }
    private Action m_openedAction;
    private Action m_closedAction;
    private bool m_isFading = false;
    private LoadingType m_loadingType = LoadingType.Null;
    private float m_time = 0;
    private float m_duration = 0.5f;

    private float m_moveY = 700;

    public LoadingType CurrentLoadingType
    {
        get
        {
            return m_loadingType;
        }
    }
    public int GetName()
    {
        return (int)ViewName.LoadingViewModule;
    }

    public GameObject OnCreate()
    {
        m_gameObject = GameApp.UI.Pool.m_loadingUI;
        Dictionary<string, GameObject> _dic = ViewTools.CollectAllGameObjects(m_gameObject);
        m_upObj = _dic["Up"].GetComponent<Image>();
        m_downObj = _dic["Down"].GetComponent<Image>();
        return m_gameObject;
    }
    public void OnOpen(object data)
    {
        m_loadingType = LoadingType.Null;
        LoadingType type = (LoadingType)data;
        switch (type)
        {
            case LoadingType.Closed:
                OnClosedStart();
                break;
            case LoadingType.Opened:
                OnOpendStart();
                break;
            default:
                break;
        }
        m_time = 0;
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        switch (m_loadingType)
        {
            case LoadingType.Closed:
                OnClosedUpdate(unscaledDeltaTime);
                break;
            case LoadingType.Opened:
                OnOpendUpdate(unscaledDeltaTime);
                break;
            default:
                break;
        }
    }
    public void OnClose()
    {
        m_loadingType = LoadingType.Null;
        GameApp.Event.DispatchNow(LocalMessageName.CC_UI_LOADINGVIEW_CLOSE, null);
    }

    public void RegisterEvents(EventSystemManager manager)
    {
    }

    public void UnRegisterEvents(EventSystemManager manager)
    {
    }


    public void Play(LoadingType type, Action finished, bool isFading)
    {
        m_isFading = isFading;
        switch (type)
        {
            case LoadingType.Closed:
                m_closedAction = finished;
                OnClosedTrigger();
                break;
            case LoadingType.Opened:
                m_openedAction = finished;
                OnOpenedTrigger();
                break;
            default:
                break;
        }
    }


    #region Opened

    void OnOpendStart()
    {
        m_upObj.transform.localPosition = Vector3.zero;
        m_downObj.transform.localPosition = Vector3.zero;
    }
    void OnOpenedTrigger()
    {
        m_time = -1;
        m_loadingType = LoadingType.Opened;
    }
    void OnOpendUpdate(float deltaTime)
    {
        m_time += deltaTime;
        m_upObj.transform.localPosition = Easing.EasingVector3(m_time, Vector3.zero, new Vector3(0, m_moveY, 0), m_duration);
        m_downObj.transform.localPosition = Easing.EasingVector3(m_time, Vector3.zero, new Vector3(0, -m_moveY, 0), m_duration);
        if (m_isFading)
        {
            float a = Easing.EasingFloat(m_time, 1, 0.5f, m_duration);
            m_upObj.color = new Color(m_upObj.color.r, m_upObj.color.g, m_upObj.color.b, a);
            m_downObj.color = new Color(m_upObj.color.r, m_upObj.color.g, m_upObj.color.b, a);
        }
        if (m_time >= m_duration)
        {
            if (m_openedAction != null)
            {
                m_openedAction.Invoke();
                m_openedAction = null;
            }
        }
    }

    #endregion

    #region Closed

    void OnClosedStart()
    {
        m_upObj.transform.localPosition = new Vector3(0, m_moveY, 0);
        m_downObj.transform.localPosition = new Vector3(0, -m_moveY, 0);
    }
    void OnClosedTrigger()
    {
        m_time = 0;
        m_loadingType = LoadingType.Closed;
    }
    void OnClosedUpdate(float deltaTime)
    {
        m_time += deltaTime;
        m_upObj.transform.localPosition = Easing.EasingVector3(m_time, new Vector3(0, m_moveY, 0), Vector3.zero, m_duration);
        m_downObj.transform.localPosition = Easing.EasingVector3(m_time, new Vector3(0, -m_moveY, 0), Vector3.zero, m_duration);
        if (m_isFading)
        {
            float a = Easing.EasingFloat(m_time, 0.5f, 1, m_duration);
            m_upObj.color = new Color(m_upObj.color.r, m_upObj.color.g, m_upObj.color.b, a);
            m_downObj.color = new Color(m_upObj.color.r, m_upObj.color.g, m_upObj.color.b, a);
        }
        if (m_time >= m_duration)
        {
            if (m_closedAction != null)
            {
                m_closedAction.Invoke();
                m_closedAction = null;
            }
        }
    }

    #endregion

}