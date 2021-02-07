//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 13:15:40
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameState : State
{
    #region State

    public override int GetName()
    {
        return (int) StateName.GameState;
    }

    public override void OnEnter()
    {
        if (GameApp.UI.IsOpened(ViewName.LoadingViewModule) == false)
        {
            GameApp.UI.OpenView(ViewName.LoadingViewModule, LoadingViewModule.LoadingType.Closed, UILayers.Second);
            LoadingViewModule loadingView = GameApp.UI.GetViewModule<LoadingViewModule>(ViewName.LoadingViewModule);
            loadingView.Play(LoadingViewModule.LoadingType.Closed, OnLoadingCloseViewFinished, true);
        }
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
    
    private void OnLoadingCloseViewFinished()
    {
        Debug.Log("OnLoadingCloseViewFinished");
        GameApp.Scene.LoadAsync("Game", UnityEngine.SceneManagement.LoadSceneMode.Single, OnSceneLoadFinished);
    }
    private void OnLoadingOpenViewFinished()
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
        GameApp.UI.OpenView(ViewName.GameStartViewModule);
        LoadingViewModule loadingView = GameApp.UI.GetViewModule<LoadingViewModule>(ViewName.LoadingViewModule);
        loadingView.Play(LoadingViewModule.LoadingType.Opened, OnLoadingOpenViewFinished, true);
    }
    
}