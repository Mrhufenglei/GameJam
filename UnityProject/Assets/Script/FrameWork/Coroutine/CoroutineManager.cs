using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public CoroutionAgent[] m_agents = new CoroutionAgent[3];

    public void AddTask(int type, IEnumerator routine)
    {
        m_agents[type].AddTask(routine);
    }

    public void RemoveTask(int type, IEnumerator routine)
    {
        m_agents[type].RemoveTask(routine);
    }

    public void RemoveAllTask(int type)
    {
        m_agents[type].RemoveAllTask();
    }

    public void RemoveAllTask()
    {
        for (int i = 0; i < m_agents.Length; i++)
        {
            m_agents[i].RemoveAllTask();
        }
    }
}
