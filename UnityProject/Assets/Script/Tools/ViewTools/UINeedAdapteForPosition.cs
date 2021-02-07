//======================================================================== 
//	
// 	 Maggic @ 2020/3/4 16:55:52　　　　　　　
// 	
//========================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINeedAdapteForPosition : MonoBehaviour
{
    public RectTransform m_target;
    public Vector2 m_addPosition = new Vector3(0, -50);

    private Vector2 m_defaultPosition;
    private void Start()
    {
        if (!Device.Instance.IsNeedSpecialAdapte()) return;
        Debug.Log("UINeedAdapteForPosition");
        if (m_target != null)
        {
            m_defaultPosition = m_target.anchoredPosition;
            m_target.anchoredPosition = m_defaultPosition + m_addPosition;
        }
    }
    private void OnDestroy()
    {
        if (!Device.Instance.IsNeedSpecialAdapte()) return;
        if (m_target != null)
        {
            m_target.anchoredPosition = m_defaultPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (m_target != null)
        {
            Vector2 pos = m_target.anchoredPosition + m_addPosition;
            Gizmos.DrawLine(m_target.anchoredPosition, pos);
        }
    }

}
