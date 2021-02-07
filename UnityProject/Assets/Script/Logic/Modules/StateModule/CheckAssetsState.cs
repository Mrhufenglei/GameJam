//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 14:44:13
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class CheckAssetsState : State
{
    #region State
    public override int GetName()
    {
        return (int)StateName.CheckAssetsState;
    }

    public override void OnEnter()
    {
        //打开UI
        GameApp.UI.OpenView(ViewName.StarupViewModule, null, UILayers.First, null, OnOpenFinsishedForStarupUI);

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
    private void OnOpenFinsishedForStarupUI(GameObject obj)
    {
        //加载表格数据
        GameApp.Table.OnInitialize();
        GameApp.Table.Manager.LoadAll(OnForLoadAllLocalTableHandler);
    }

    private void OnForLoadAllLocalTableHandler()
    {
        //初始化数据层

        //检查更新 之后再写
        bool isNeedUp = false;
        if (isNeedUp == true)
        {
            //转换到更新状态
        }
        else
        {
            //进入登陆状态
            GameApp.State.ActiveState(StateName.LoginState);
        }
    }
}
