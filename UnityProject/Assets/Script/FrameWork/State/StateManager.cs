//*************************************************************
//
//          Maggic@2015.6.2
//
//*************************************************************
//
using UnityEngine;
using System.Collections.Generic;
public class StateManager : MonoBehaviour
{
    private State m_currentState = null;

    private Dictionary<int, State> m_states = new Dictionary<int, State>();

    [SerializeField]
    private EventSystemManager m_eventSystemManager = null;

    /// <summary>
    /// 激活状态
    /// </summary>
    /// <param name="stateName"></param>
    public void ActiveState(int stateName)
    {
        State _state = GetState<State>(stateName);
        Debug.LogFormat("<color=red>[State]</color>onEnter {0} ", _state.GetName());
        _state.OnEnter();
        _state.RegisterEvents(m_eventSystemManager);
        if (m_currentState != null)
        {
            Debug.LogFormat("<color=red>[State]</color>onExit {0} ", m_currentState.GetName());
            m_currentState.RegisterEvents(m_eventSystemManager);
            m_currentState.OnExit();
        }
        m_currentState = _state;
    }
    /// <summary>
    /// 获得当前状态的名称
    /// </summary>
    /// <returns></returns>
    public int GetCurrentStateName()
    {
        if (m_currentState == null) return -1;
        return m_currentState.GetName();
    }
    /// <summary>
    /// 获得状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stateName"></param>
    /// <returns></returns>
    public T GetState<T>(int stateName) where T : State
    {
        T _t = default(T);
        State _state = null;
        if (m_states.TryGetValue(stateName, out _state))
        {
            _t = _state as T;
        }
        return _t;
    }
    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="state"></param>
    public void RegisterState(State state)
    {
        if (state != null)
        {
            m_states[state.GetName()] = state;
        }
    }
    /// <summary>
    /// 取消注册
    /// </summary>
    /// <param name="state"></param>
    public void UnRegisterState(State state)
    {
        if (state != null)
        {
            m_states.Remove(state.GetName());
        }
    }
    /// <summary>
    /// 通过名字取消注册
    /// </summary>
    /// <param name="stateName"></param>
    public void UnRegisterStateByName(int stateName)
    {
        m_states.Remove(stateName);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        if (m_currentState != null) m_currentState.OnUpdate(deltaTime, unscaledDeltaTime);
    }
}
