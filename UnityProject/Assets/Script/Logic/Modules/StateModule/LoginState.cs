//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 15:02:55
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class LoginState : State
{
    #region State
    public override int GetName()
    {
        return (int)StateName.LoginState;
    }

    public override void OnEnter()
    {
        GameApp.UI.OpenView(ViewName.LoginViewModule, null, UILayers.First, null, (x) =>
        {
            GameApp.UI.CloseView(ViewName.StarupViewModule);

        });
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public override void RegisterEvents(EventSystemManager manager)
    {
    }

    public override void UnRegisterEvents(EventSystemManager manager)
    {
    }
    #endregion

}
