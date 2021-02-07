//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:13:46
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MainState : State
{
    #region State

    public override int GetName()
    {
        return (int) StateName.MainState;
    }

    public override void OnEnter()
    {
        if (GameApp.UI.IsOpened(ViewName.LoadingViewModule) == false)
        {
            GameApp.UI.OpenView(ViewName.LoadingViewModule, LoadingViewModule.LoadingType.Opened, UILayers.Second);
        }
        GameApp.Scene.LoadAsync("Main", UnityEngine.SceneManagement.LoadSceneMode.Single, OnSceneLoadFinished);
    }

    public override void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public override void OnExit()
    {
    }

    public override void RegisterEvents(EventSystemManager manager)
    {
    }

    public override void UnRegisterEvents(EventSystemManager manager)
    {
    }

    #endregion
    private void OnLoadingViewFinished()
    {
        Debug.Log("OnLoadingViewFinished");
        GameApp.UI.CloseView(ViewName.LoadingViewModule);
    }
    private void OnSceneLoadFinished(string sceneName)
    {
        GameApp.Resources.UnloadUnusedAssets();
        System.GC.Collect();

        GameApp.UI.CloseAllView(UILayers.First);
        Debug.Log("OnSceneLoadFinished");
        GameApp.UI.OpenView(ViewName.MainViewModule);
        LoadingViewModule loadingView = GameApp.UI.GetViewModule<LoadingViewModule>(ViewName.LoadingViewModule);
        loadingView.Play(LoadingViewModule.LoadingType.Opened, OnLoadingViewFinished, true);
    }
}