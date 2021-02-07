//======================================================================== 
//	
// 	 Maggic @ 2020/3/4 17:00:47　　　　　　　
// 	
//========================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINeedAdapteForActive : MonoBehaviour
{
    public GameObject[] m_opens = new GameObject[0];
    public GameObject[] m_closes = new GameObject[0];

    private void Start()
    {
        if (!Device.Instance.IsNeedSpecialAdapte()) return;
        Debug.Log("UINeedAdapteForActive");
        if (m_opens != null)
        {
            for (int i = 0; i < m_opens.Length; i++)
            {
                if (m_opens[i] != null) m_opens[i].SetActive(true);
            }
        }
        if (m_closes != null)
        {
            for (int i = 0; i < m_closes.Length; i++)
            {
                if (m_closes[i] != null) m_closes[i].SetActive(false);
            }
        }
    }
    private void OnDestroy()
    {

    }
}
