//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 15:24:51
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class SelectLevelState : State
{
    #region State
    public override int GetName()
    {
        return (int)StateName.SelectLevelState;
    }

    public override void OnEnter()
    {
        GameApp.UI.OpenView(ViewName.SelectLevelViewModule, null, UILayers.First, null, (x) =>
        {
            GameApp.UI.CloseView(ViewName.LoadingViewModule);

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
