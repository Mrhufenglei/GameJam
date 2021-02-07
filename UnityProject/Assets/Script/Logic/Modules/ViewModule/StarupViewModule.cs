//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 13:13:05
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class StarupViewModule : BaseViewModule
{
    #region BaseViewModule
  
    public override void OnOpen(object data)
    {
        Debug.Log("StarupViewModule.OnOpen");
    }
    public override void OnClose()
    {
        Debug.Log("StarupViewModule.OnClose");
    }
    public override void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public override void RegisterEvents(EventSystemManager manager)
    {
        Debug.Log("StarupViewModule.RegisterEvents");
    }

    public override void UnRegisterEvents(EventSystemManager manager)
    {
        Debug.Log("StarupViewModule.UnRegisterEvents");
    }
    #endregion

}
