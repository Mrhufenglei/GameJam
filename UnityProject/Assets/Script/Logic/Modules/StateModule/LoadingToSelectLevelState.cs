//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 15:26:47
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class LoadingToSelectLevelState : State
{
    #region State
    public override int GetName()
    {
        return (int)StateName.LoadingToSelectLevelState;
    }

    public override void OnEnter()
    {
        GameApp.UI.OpenView(ViewName.LoadingViewModule, null, UILayers.First, null, (x) =>
        {
            GameApp.UI.CloseView(ViewName.LoginViewModule);
            //加载场景
            var handle = GameApp.Scene.LoadSceneAsync("Assets/_Resources/Scene/Other/SelectLevel.unity", UnityEngine.SceneManagement.LoadSceneMode.Single);
            handle.Completed += (s) =>
            {

                //转换状态
                GameApp.State.ActiveState(StateName.SelectLevelState);
            };
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
