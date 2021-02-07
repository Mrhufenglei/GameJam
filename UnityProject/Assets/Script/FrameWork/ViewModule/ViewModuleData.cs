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
public class ViewModuleData
{
    public GameObject m_gameObject;
    public IViewModule m_viewModule;
    public bool m_isOpened = false;
    public UILayers m_uiLayers = UILayers.First;
    public ViewModuleData(IViewModule viewModule, GameObject gameObject = null)
    {
        m_gameObject = gameObject;
        m_viewModule = viewModule;
    }
}
