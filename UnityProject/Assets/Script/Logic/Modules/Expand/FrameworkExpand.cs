using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrameworkExpand
{
    #region Event
    public static void RegisterEvent(this EventSystemManager manager, LocalMessageName name, HandleEvent handle)
    {
        manager.RegisterEvent((int)name, handle);
    }
    public static void UnRegisterEvent(this EventSystemManager manager, LocalMessageName name, HandleEvent handle)
    {
        manager.UnRegisterEvent((int)name, handle);
    }
    public static void Dispatch(this EventSystemManager manager, LocalMessageName name, object eventObject = null)
    {
        manager.Dispatch((int)name, eventObject);
    }
    public static void DispatchNow(this EventSystemManager manager, LocalMessageName name, object eventObject = null)
    {
        manager.DispatchNow((int)name, eventObject);
    }
    #endregion

    #region Data
    public static T GetDataModule<T>(this DataModuleManager manager, DataName name) where T : IDataModule
    {
        return manager.GetDataModule<T>((int)name);
    }
    #endregion

    #region View
    public static T GetViewModule<T>(this ViewModuleManager manager, ViewName name) where T : IViewModule
    {
        return manager.GetViewModule<T>((int)name);
    }
    public static void OpenView(this ViewModuleManager manager, ViewName name, object data = null, UILayers layer = UILayers.First)
    {
        manager.OpenView((int)name, data, layer);
    }
    public static void CloseView(this ViewModuleManager manager, ViewName name)
    {
        manager.CloseView((int)name);
    }
    public static bool IsOpened(this ViewModuleManager manager, ViewName name)
    {
        return manager.IsOpened((int)name);

    }
    #endregion

    #region State
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
