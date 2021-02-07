using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataModule
{
    /// <summary>
    /// 获得名称
    /// </summary>
    /// <returns></returns>
    int GetName();

    void RegisterEvents(EventSystemManager manager);

    void UnRegisterEvents(EventSystemManager manager);
}
