//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 15:40:47
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class SelectLevelViewModule : BaseViewModule
{
    public Button m_loginBt;
    #region BaseViewModule

    public override void OnOpen(object data)
    {
        Debug.Log("SelectLevelViewModule.OnOpen");
        m_loginBt.onClick.AddListener(OnClickLoginBtHandler);
    }
    public override void OnClose()
    {
        Debug.Log("SelectLevelViewModule.OnClose");
        m_loginBt.onClick.RemoveListener(OnClickLoginBtHandler);
    }
    public override void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public override void RegisterEvents(EventSystemManager manager)
    {
        Debug.Log("SelectLevelViewModule.RegisterEvents");
    }

    public override void UnRegisterEvents(EventSystemManager manager)
    {
        Debug.Log("SelectLevelViewModule.UnRegisterEvents");
    }
    #endregion

    private void OnClickLoginBtHandler()
    {
        Debug.Log("SelectLevelViewModule");
    }
}
