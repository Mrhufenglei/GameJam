//----------------------------------------------------------------------
//
//              Maggic @  2020/8/14 19:08:01
//
//---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HandlerEvent(object sender, int type, BaseEventArgs eventArgs = null);
public class EventSystemManager : MonoBehaviour
{
    private Dictionary<int, List<HandlerEvent>> m_handles = new Dictionary<int, List<HandlerEvent>>();
    private List<DispatchData> m_dispatchDatas = new List<DispatchData>();

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="handle"></param>
    public void RegisterEvent(int type, HandlerEvent handle)
    {
        List<HandlerEvent> handles = null;
        bool isContains = m_handles.TryGetValue(type, out handles);
        if (isContains == true)
        {
            handles.Add(handle);
        }
        else
        {
            handles = new List<HandlerEvent>();
            handles.Add(handle);
            m_handles.Add(type, handles);
        }
    }
    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="handle"></param>
    public void UnRegisterEvent(int type, HandlerEvent handle)
    {
        List<HandlerEvent> handles = null;
        bool isContains = m_handles.TryGetValue(type, out handles);
        if (isContains == true)
        {
            if (handles.Contains(handle) == true)
            {
                handles.Remove(handle);
            }
        }
    }
    /// <summary>
    /// 立即发送事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="type"></param>
    /// <param name="eventArgs"></param>
    public void DispatchNow(object sender, int type, BaseEventArgs eventArgs = null)
    {
        List<HandlerEvent> handles = null;
        bool isContains = m_handles.TryGetValue(type, out handles);
        if (isContains == true)
        {
            for (int i = 0; i < handles.Count; i++)
            {
                if (handles[i] != null)
                {
                    handles[i](sender, type, eventArgs);
                }
            }
        }
    }
    /// <summary>
    /// 当Update是发送事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="type"></param>
    /// <param name="eventArgs"></param>
    public void Dispatch(object sender, int type, BaseEventArgs eventArgs = null)
    {
        m_dispatchDatas.Add(new DispatchData() { m_sender = sender, m_type = type, m_eventArgs = eventArgs });
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_dispatchDatas.Count > 0)
        {
            for (int i = 0; i < m_dispatchDatas.Count; i++)
            {
                DispatchNow(m_dispatchDatas[i].m_sender, m_dispatchDatas[i].m_type,m_dispatchDatas[i].m_eventArgs);
            }
            m_dispatchDatas.Clear();
        }

    }
}
