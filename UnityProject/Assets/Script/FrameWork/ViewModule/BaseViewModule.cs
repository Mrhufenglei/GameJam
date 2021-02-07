//----------------------------------------------------------------------
//
//              Maggic @  2020/8/18 12:00:06
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public abstract class BaseViewModule : MonoBehaviour
{
    /// <summary>
    /// 打开
    /// </summary>
    public abstract void OnOpen(object data);
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="unscaledDeltaTime"></param>
    public abstract void OnUpdate(float deltaTime, float unscaledDeltaTime);
    /// <summary>
    /// 关闭
    /// </summary>
    public abstract void OnClose();

    public abstract void RegisterEvents(EventSystemManager manager);

    public abstract void UnRegisterEvents(EventSystemManager manager);
}
