//======================================================================== 
//	
// 	 Maggic @ 2019/9/29 15:58:40 　　　　　　　　
// 	
//========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
//public class ViewModuleData
//{
//    public GameObject m_gameObject;
//    public IViewModule m_viewModule;
//    public bool m_isOpened = false;
//    public ViewModuleData(IViewModule viewModule, GameObject gameObject = null)
//    {
//        m_gameObject = gameObject;
//        m_viewModule = viewModule;
//    }
//}
public class ViewModuleData
{
    public int m_viewName;
    public string m_assetPath;
    public GameObject m_prefab;
    public GameObject m_gameObject;
    public BaseViewModule m_baseViewModule;
    public bool m_isCanDestory = false;
    public ViewState m_viewState;
    public ViewModuleData(int viewName, string assetPath, GameObject gameObject = null,bool isCanDestory = true)
    {
        m_viewName = viewName;
        m_gameObject = gameObject;
        m_assetPath = assetPath;
        m_gameObject = gameObject;
        m_isCanDestory = isCanDestory;
    }
}
public enum ViewState
{
    Null,
    Loading,
    Opened,
    Closed,
}
