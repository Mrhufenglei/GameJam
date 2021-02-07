//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 14:37:45
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class LoginViewModule : BaseViewModule
{
    public Button m_loginBt;
    #region BaseViewModule

    public override void OnOpen(object data)
    {
        Debug.Log("LoginViewModule.OnOpen");
        m_loginBt.onClick.AddListener(OnClickLoginBtHandler);
    }
    public override void OnClose()
    {
        Debug.Log("LoginViewModule.OnClose");
        m_loginBt.onClick.RemoveListener(OnClickLoginBtHandler);
    }
    public override void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public override void RegisterEvents(EventSystemManager manager)
    {
        Debug.Log("LoginViewModule.RegisterEvents");
    }

    public override void UnRegisterEvents(EventSystemManager manager)
    {
        Debug.Log("LoginViewModule.UnRegisterEvents");
    }
    #endregion

    private void OnClickLoginBtHandler()
    {
        Debug.Log("OnClickLoginBtHandler");
        GameApp.State.ActiveState(StateName.LoadingToSelectLevelState);
    }
}
