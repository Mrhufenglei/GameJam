using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModuleManager : MonoBehaviour
{
    private Dictionary<int, IDataModule> m_dataModules = new Dictionary<int, IDataModule>();
    [SerializeField]
    private EventSystemManager m_eventSystemManager;

    public void RegisterDataModule(IDataModule dataModule)
    {
        if (dataModule != null)
        {
            dataModule.RegisterEvents(m_eventSystemManager);
            m_dataModules[dataModule.GetName()] = dataModule;
        }
    }
    public void UnRegisterDataModule(IDataModule dataModule)
    {
        if (dataModule != null)
        {
            dataModule.UnRegisterEvents(m_eventSystemManager);
            m_dataModules.Remove(dataModule.GetName());
        }
    }
    public T GetDataModule<T>(int name) where T : IDataModule
    {
        T _t = default(T);
        IDataModule _module = null;
        if (m_dataModules.TryGetValue(name, out _module))
        {
            _t = (T)_module;
        }
        return _t;
    }
}
