
using UnityEngine;

public interface IViewModule
{
    /// <summary>
    /// 获得名称
    /// </summary>
    /// <returns></returns>
    int GetName();
    /// <summary>
    /// 创建
    /// </summary>
    /// <returns></returns>
    GameObject OnCreate();
    /// <summary>
    /// 打开
    /// </summary>
    void OnOpen(object data);
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="unscaledDeltaTime"></param>
    void OnUpdate(float deltaTime, float unscaledDeltaTime);
    /// <summary>
    /// 关闭
    /// </summary>
    void OnClose();

    void RegisterEvents(EventSystemManager manager);

    void UnRegisterEvents(EventSystemManager manager);
}
