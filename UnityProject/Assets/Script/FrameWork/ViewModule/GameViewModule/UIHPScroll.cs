//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 19:10:48
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class UIHPScroll : MonoBehaviour
{
    public GameObject m_prefab;
    public RectTransform m_parent;

    public List<UIHPNode> m_nodes = new List<UIHPNode>();

    #region View

    public void OnOpen(object data)
    {
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_CREATEHP, OnEventCreateHP);
        GameApp.Event.RegisterEvent(LocalMessageName.CC_GAME_DESTROYHP, OnEventDestroyHP);
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        UpdateNode(deltaTime, unscaledDeltaTime);
    }

    public void OnClose()
    {
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_CREATEHP, OnEventCreateHP);
        GameApp.Event.UnRegisterEvent(LocalMessageName.CC_GAME_DESTROYHP, OnEventDestroyHP);
        DestroyAllNodes();
    }

    #endregion

    #region Node

    private void CreateNode(BaseMember member)
    {
        if (member == null) return;
        if (m_prefab == null) return;
        if (m_parent == null) return;
        var obj = GameObject.Instantiate<GameObject>(m_prefab);
        if (obj == null) return;
        obj.transform.SetParent(m_parent);
        obj.transform.localScale = Vector3.one;
        obj.SetActive(true);
        UIHPNode node = obj.GetComponent<UIHPNode>();
        if (node == null) return;
        node.SetMember(member);
        node.OnInit();
        m_nodes.Add(node);
    }

    private void UpdateNode(float deltaTime, float unscaledDeltaTime)
    {
        for (int i = 0; i < m_nodes.Count; i++)
        {
            var node = m_nodes[i];
            if (node == null) continue;
            node.OnUpdate(deltaTime, unscaledDeltaTime);
        }
    }

    private void DestroyAllNodes()
    {
        for (int i = 0; i < m_nodes.Count; i++)
        {
            var node = m_nodes[i];
            if (node == null) continue;
            node.OnDeInit();
            GameObject.Destroy(node.gameObject);
        }

        m_nodes.Clear();
    }

    #endregion

    #region Event

    private void OnEventCreateHP(int evenid, object eventObject)
    {
        if (eventObject == null) return;
        var member = eventObject as BaseMember;
        CreateNode(member);
    }

    private void OnEventDestroyHP(int evenid, object eventObject)
    {
        if (eventObject == null) return;
        var member = eventObject as BaseMember;

       var nodes = m_nodes.ToList();
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            if (node == null) continue;
            if (node.m_target == member)
            {
                node.OnDeInit();
                GameObject.Destroy(node.gameObject);
                m_nodes.Remove(node);
            }
        }
    }

    #endregion
}