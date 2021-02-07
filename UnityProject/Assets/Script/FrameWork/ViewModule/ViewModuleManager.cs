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

    [SerializeField]
    private UIPool m_uiPool = null;

    [SerializeField]
    private EventSystemManager m_eventSystemManager = null;

    [SerializeField]
    private ResourcesManager m_resourcesManager = null;

    public UIPool Pool
    {
        get { return m_uiPool; }
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
        if (viewModuleDatas != null)
        {
            m_viewModuleDatas[viewModuleDatas.m_viewName] = viewModuleDatas;
        }
    }
    public void UnRegisterViewModule(ViewModuleData viewModuleDatas)
    {
        if (viewModuleDatas != null)
        {
            m_viewModuleDatas.Remove(viewModuleDatas.m_viewName);
        }
    }

    public ViewModuleData GetViewModuleData(int viewName)
    {
        ViewModuleData _viewModuleData = null;
        m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData);
        return _viewModuleData;
    }
    public T GetViewModule<T>(int viewName) where T : BaseViewModule
    {
        T t = null;
        ViewModuleData _viewModuleData = GetViewModuleData(viewName);
        t = _viewModuleData.m_baseViewModule as T;
        return t;
    }

    public void OpenView(
        int viewName,
        object data = null,
        UILayers layer = UILayers.First,
        Action<GameObject> loadedCallBack = null,
        Action<GameObject> openedCallBack = null)
    {
        Debug.LogFormat("<color=red>[ViewModule]</color>OpenView( {0} )", viewName);
        ViewModuleData _viewModuleData = null;
        m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData);

        if (_viewModuleData == null)
        {
            Debug.LogErrorFormat("[ViewModule]viewModuleData is null , viewName = {0},UIlayers = {1}", viewName, layer);
            return;
        }

        if (_viewModuleData.m_gameObject == null)
        {
            if (_viewModuleData.m_prefab == null)
            {
                _viewModuleData.m_viewState = ViewState.Loading;

                //加载
                var _handler = m_resourcesManager.LoadAssetAsync<GameObject>(_viewModuleData.m_assetPath);
                _handler.Completed += (x) =>
                {
                    if (x.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                    {
                        Debug.LogErrorFormat("[ViewModule]viewModuleData loading Failed , viewName = {0},UIlayers = {1}", viewName, layer);
                        return;
                    }

                    if (_viewModuleData.m_viewState != ViewState.Loading)
                    {
                        Debug.LogErrorFormat("[ViewModule]viewModuleData not loading , viewName = {0},UIlayers = {1},state = {2}", viewName, layer, _viewModuleData.m_viewState);
                        return;
                    }

                    Debug.LogFormat("<color=red>[ViewModule]</color>OpenView.Load finided {0} ", viewName);

                    _viewModuleData.m_prefab = x.Result;

                    if (loadedCallBack != null) loadedCallBack.Invoke(_viewModuleData.m_prefab);

                    //创建UI 并调用UI函数
                    _viewModuleData.m_gameObject = InstantiateByPrefab(_viewModuleData.m_viewName, _viewModuleData.m_prefab, layer);
                    _viewModuleData.m_baseViewModule = OpenViewByGameObject(_viewModuleData.m_viewName, _viewModuleData.m_gameObject, data, openedCallBack);
                    _viewModuleData.m_viewState = ViewState.Opened;
                    m_viewModuleDatas[viewName] = _viewModuleData;
                };
                m_viewModuleDatas[viewName] = _viewModuleData;
                return;
            }
            else
            {
                //创建UI 并调用UI函数
                _viewModuleData.m_gameObject = InstantiateByPrefab(_viewModuleData.m_viewName, _viewModuleData.m_prefab, layer);
                _viewModuleData.m_baseViewModule = OpenViewByGameObject(_viewModuleData.m_viewName, _viewModuleData.m_gameObject, data, openedCallBack);
                _viewModuleData.m_viewState = ViewState.Opened;
                m_viewModuleDatas[viewName] = _viewModuleData;
            }
        }
        else
        {
            _viewModuleData.m_baseViewModule = OpenViewByGameObject(_viewModuleData.m_viewName, _viewModuleData.m_gameObject, data, openedCallBack);
            _viewModuleData.m_viewState = ViewState.Opened;
            m_viewModuleDatas[viewName] = _viewModuleData;
        }
    }

    private GameObject InstantiateByPrefab(int viewName, GameObject prefab, UILayers layer = UILayers.First)
    {
        GameObject _layerObj = GetGameObjectByUILayers(layer);
        if (_layerObj == null)
        {
            Debug.LogErrorFormat("[ViewModule]layer gameObject is null ,viewName = {0},UIlayers = {1}", viewName, layer);
            return null;
        }
        GameObject _obj = GameObject.Instantiate<GameObject>(prefab);
        RectTransform _trans = (RectTransform)_obj.transform;
        _trans.SetParent(_layerObj.transform);
        _trans.sizeDelta = Vector3.zero;
        _trans.localScale = Vector3.one;
        _trans.localPosition = Vector3.zero;
        _trans.localRotation = Quaternion.identity;

        _trans.SetAsLastSibling();

        return _obj;
    }

    private BaseViewModule OpenViewByGameObject(int viewName, GameObject target, object data, Action<GameObject> openedCallBack)
    {
        target.SetActive(true);
        BaseViewModule _baseViewModule = target.GetComponent<BaseViewModule>();

        if (_baseViewModule == null)
        {
            Debug.LogErrorFormat("[ViewModule]baseViewModule is null ,viewName = {0},UIlayers = {1}", viewName);
            return _baseViewModule;
        }
        _baseViewModule.OnOpen(data);
        _baseViewModule.RegisterEvents(m_eventSystemManager);
        if (openedCallBack != null) openedCallBack.Invoke(target);

        Debug.LogFormat("<color=red>[ViewModule]</color>OpenView.Opened finided {0} ", viewName);

        return _baseViewModule;
    }

    public void CloseView(int viewName)
    {
        Debug.LogFormat("<color=red>[ViewModule]</color>CloseView( {0} )", viewName);

        ViewModuleData _viewModuleData = null;
        m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData);
        if (_viewModuleData == null)
        {
            Debug.LogErrorFormat("[ViewModule]CloseView viewModuleData is null , viewName = {0}", viewName);
            return;
        }

        if (_viewModuleData.m_gameObject == null || _viewModuleData.m_baseViewModule == null)
        {
            Debug.LogErrorFormat("[ViewModule]CloseView viewModuleData gameObject is null , viewName = {0}", viewName);
            _viewModuleData.m_viewState = ViewState.Null;
            return;
        }

        _viewModuleData.m_baseViewModule.UnRegisterEvents(m_eventSystemManager);
        _viewModuleData.m_baseViewModule.OnClose();
        if (_viewModuleData.m_isCanDestory)
        {
            GameObject.Destroy(_viewModuleData.m_gameObject);
            _viewModuleData.m_baseViewModule = null;
            _viewModuleData.m_gameObject = null;

            m_resourcesManager.Release(_viewModuleData.m_prefab);
            _viewModuleData.m_prefab = null;
        }
        else
        {
            _viewModuleData.m_gameObject.SetActive(false);
        }
        _viewModuleData.m_viewState = ViewState.Closed;
        m_viewModuleDatas[viewName] = _viewModuleData;
    }

    public GameObject GetGameObjectByUILayers(UILayers uilayer)
    {
        GameObject _object = null;
        if (Pool != null)
        {
            int _index = (int)uilayer;
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
    public bool IsOpened(int viewName)
    {
        bool _isOpened = false;
        ViewModuleData _viewModuleData = null;
        m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData);

        if (_viewModuleData == null)
        {
            Debug.LogErrorFormat("[ViewModule]CloseView viewModuleData is null , viewName = {0}", viewName);
            return false;
        }

        _isOpened = _viewModuleData.m_viewState == ViewState.Opened;
        return _isOpened;
    }
    public bool IsLoading(int viewName)
    {
        bool _isOpened = false;
        ViewModuleData _viewModuleData = null;
        m_viewModuleDatas.TryGetValue(viewName, out _viewModuleData);

        if (_viewModuleData == null)
        {
            Debug.LogErrorFormat("[ViewModule]CloseView viewModuleData is null , viewName = {0}", viewName);
            return false;
        }

        _isOpened = _viewModuleData.m_viewState == ViewState.Loading;
        return _isOpened;
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        foreach (var item in m_viewModuleDatas)
        {
            if (item.Value.m_baseViewModule != null
                && item.Value.m_viewState == ViewState.Opened
                && item.Value.m_gameObject != null)
            {
                item.Value.m_baseViewModule.OnUpdate(deltaTime, unscaledDeltaTime);
            }
        }
    }
}

