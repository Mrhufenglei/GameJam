using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrameworkExpand
{
    #region Event
    public static void RegisterEvent(this EventSystemManager manager, LocalMessageName name, HandlerEvent handle)
    {
        manager.RegisterEvent((int)name, handle);
    }
    public static void UnRegisterEvent(this EventSystemManager manager, LocalMessageName name, HandlerEvent handle)
    {
        manager.UnRegisterEvent((int)name, handle);
    }
    public static void Dispatch(this EventSystemManager manager, object sender, LocalMessageName name, BaseEventArgs eventArgs)
    {
        manager.Dispatch(sender, (int)name, eventArgs);
    }
    public static void DispatchNow(this EventSystemManager manager, object sender, LocalMessageName name, BaseEventArgs eventArgs)
    {
        manager.DispatchNow(sender, (int)name, eventArgs);
    }
    #endregion

    #region Data
    public static T GetDataModule<T>(this DataModuleManager manager, DataName name) where T : IDataModule
    {
        return manager.GetDataModule<T>((int)name);
    }
    #endregion

    #region View
    /// <summary>
    /// 获得显示层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="manager"></param>
    /// <param name="name"></param> 
    /// <returns></returns>
    public static T GetViewModule<T>(this ViewModuleManager manager, ViewName name) where T : BaseViewModule
    {
        return manager.GetViewModule<T>((int)name);
    }
    public static void OpenView(this ViewModuleManager manager, ViewName name,
        object data = null,
        UILayers layer = UILayers.First,
        Action<GameObject> loadedCallBack = null,
        Action<GameObject> openedCallBack = null)
    {
        Debug.LogFormat("<color=red>[ViewModule]</color>OpenView( {0} )", name);
        manager.OpenView((int)name, data, layer, loadedCallBack, openedCallBack);
    }
    public static void CloseView(this ViewModuleManager manager, ViewName name)
    {
        Debug.LogFormat("<color=red>[ViewModule]</color>CloseView( {0} )", name);
        manager.CloseView((int)name);
    }
    public static bool IsOpened(this ViewModuleManager manager, ViewName name)
    {
        return manager.IsOpened((int)name);

    }
    public static bool IsLoading(this ViewModuleManager manager, ViewName name)
    {
        return manager.IsLoading((int)name);

    }
    #endregion

    #region State
    public static void ActiveState(this StateManager manager, StateName name)
    {
        Debug.LogFormat("<color=red>[State]</color>ActiveState( {0} )", name);
        manager.ActiveState((int)name);
    }
    public static T GetState<T>(this StateManager manager, StateName name) where T : State
    {
        return manager.GetState<T>((int)name);
    }
    #endregion

    #region Sound
    public static void PlaySound(this SoundManager manager, SoundName name, AudioClip clip)
    {
        manager.PlaySound((int)name, clip);
    }

    public static void StopSound(this SoundManager manager, SoundName name)
    {
        manager.StopSound((int)name);
    }

    public static SourceAgent GetSourceAgent(this SoundManager manager, SoundName name)
    {
        return manager.GetSourceAgent((int)name);
    }
    #endregion
}
