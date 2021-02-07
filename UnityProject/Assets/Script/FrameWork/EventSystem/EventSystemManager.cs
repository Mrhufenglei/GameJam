using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HandleEvent(int type, object eventObject);
public class EventSystemManager : MonoBehaviour
{
    private Dictionary<int, List<HandleEvent>> m_handles = new Dictionary<int, List<HandleEvent>>();
    private List<DispatchData> m_dispatchDatas = new List<DispatchData>();

    public void RegisterEvent(int type, HandleEvent handle)
    {
        List<HandleEvent> _handles = null;
        bool _isContains = m_handles.TryGetValue(type, out _handles);
        if (_isContains == true)
        {
            _handles.Add(handle);
        }
        else
        {
            _handles = new List<HandleEvent>();
            _handles.Add(handle);
            m_handles.Add(type, _handles);
        }
    }
    public void UnRegisterEvent(int type, HandleEvent handle)
    {
        List<HandleEvent> _handles = null;
        bool _isContains = m_handles.TryGetValue(type, out _handles);
        if (_isContains == true)
        {
            if (_handles.Contains(handle) == true)
            {
                _handles.Remove(handle);
            }
        }
    }
    public void DispatchNow(int type, object eventObject = null)
    {
        List<HandleEvent> _handles = null;
        bool _isContains = m_handles.TryGetValue(type, out _handles);
        if (_isContains == true)
        {
            for (int i = 0; i < _handles.Count; i++)
            {
                if (_handles[i] != null)
                {
                    _handles[i](type, eventObject);
                }
            }
        }
    }
    public void Dispatch(int type, object eventObject = null)
    {
        m_dispatchDatas.Add(new DispatchData() { type = type, eventObject = eventObject });
    }
    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_dispatchDatas.Count > 0)
        {
            for (int i = 0; i < m_dispatchDatas.Count; i++)
            {
                DispatchNow(m_dispatchDatas[i].type, m_dispatchDatas[i].eventObject);
            }
            m_dispatchDatas.Clear();
        }
    }
}
