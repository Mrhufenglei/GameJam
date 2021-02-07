//*************************************************************
//
//          Maggic@2018.3.2
//
//*************************************************************
//

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ViewModuleManager : MonoBehaviour
{
    private Dictionary<int, ViewModuleData> m_viewModuleDatas = new Dictionary<int, ViewModuleData>();

    [SerializeField] private UIPool m_uiPool;
    [SerializeField] private Camera m_uiCamera;

    [SerializeField] private EventSystemManager m_eventSystemManager;

    public UIPool Pool
    {
        get { return m_uiPool; }
    }

    public Camera UICamera
    {
        get { return m_uiCamera; }
    }

    public GameObject[] m_layerObjects;

    /// <summary>
    /// Registers the view module.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="viewLayer">View layer.</param>
    /// <param name="isMutex">互斥面板类型，默认不在任何互斥面板组里</param>
    public void RegisterViewModule(ViewModuleData viewModuleDatas)
    {
        if (viewModuleDatas != null && viewModuleDatas.m_viewModule != null)
        {
            m_viewModuleDatas[viewModuleDatas.m_viewModule.GetName()] = viewModuleDatas;
        }
    }

    public void UnRegisterViewModule(ViewModuleData viewModuleDatas)
    {
        if (viewModuleDatas != null && viewModuleDatas.m_viewModule != null)
        {
            m_viewModuleDatas.Remove(viewModuleDatas.m_viewModule.GetName());
        }
    }

    public T GetViewModule<T>(int viewName) where T : IViewModule
    {
        T _t = default(T);
        ViewModuleData _viewModuleData = null;
        if (m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData))
        {
            _t = (T) _viewModuleData.m_viewModule;
        }

        return _t;
    }

    public void OpenView(int viewName, object data = null, UILayers layer = UILayers.First)
    {
        ViewModuleData _viewModuleData = null;
        if (m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData))
        {
            if (_viewModuleData.m_gameObject == null)
            {
                _viewModuleData.m_gameObject = _viewModuleData.m_viewModule.OnCreate();
            }

            GameObject _prefab = GetGameObjectByUILayers(layer);
            if (_prefab != null)
            {
                RectTransform trans = (RectTransform) _viewModuleData.m_gameObject.transform;
                trans.SetParent(_prefab.transform);
                trans.sizeDelta = Vector3.zero;
                trans.localScale = Vector3.one;
                trans.localPosition = Vector3.zero;
                trans.localRotation = Quaternion.identity;

                _viewModuleData.m_gameObject.SetActive(true);
                trans.SetAsLastSibling();

                _viewModuleData.m_uiLayers = layer;
                _viewModuleData.m_viewModule.OnOpen(data);
                _viewModuleData.m_viewModule.RegisterEvents(m_eventSystemManager);
                _viewModuleData.m_isOpened = true;
            }
            else
            {
                Debug.LogError("layer gameObject is null");
            }
        }
    }

    public void CloseView(int viewName)
    {
        ViewModuleData _viewModuleData = null;
        if (m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData))
        {
            _viewModuleData.m_viewModule.UnRegisterEvents(m_eventSystemManager);
            _viewModuleData.m_viewModule.OnClose();
            _viewModuleData.m_gameObject.SetActive(false);
            _viewModuleData.m_isOpened = false;
        }
    }

    public GameObject GetGameObjectByUILayers(UILayers uilayer)
    {
        GameObject _object = null;
        if (Pool != null)
        {
            int _index = (int) uilayer;
            if (_index < m_layerObjects.Length)
            {
                _object = m_layerObjects[_index];
            }
        }

        return _object;
    }

    public void CloseAllView()
    {
        var _item = m_viewModuleDatas.GetEnumerator();
        while (_item.MoveNext())
        {
            CloseView(_item.Current.Key);
        }
    }

    public void CloseAllView(UILayers uiLayers)
    {
        var _item = m_viewModuleDatas.GetEnumerator();
        while (_item.MoveNext())
        {
            if (_item.Current.Value.m_isOpened)
            {
                if (_item.Current.Value.m_uiLayers == uiLayers)
                {
                    CloseView(_item.Current.Key);
                }
            }
        }
    }

    public bool IsOpened(int viewName)
    {
        bool _isOpened = false;
        ViewModuleData _viewModuleData = null;
        if (m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData))
        {
            _isOpened = _viewModuleData.m_isOpened;
        }

        return _isOpened;
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        foreach (var item in m_viewModuleDatas)
        {
            if (item.Value.m_viewModule != null && item.Value.m_isOpened && item.Value.m_gameObject != null)
            {
                item.Value.m_viewModule.OnUpdate(deltaTime, unscaledDeltaTime);
            }
        }
    }
}