//======================================================================== 
//	
// 	 Maggic @ 2020/4/16 14:25:06　　　　　　　
// 	
//========================================================================

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 图片置灰
/// </summary>
public class UIGray : MonoBehaviour
{
    [Header("Target Setting")]
    public Graphic m_target;

    [Header("Materail Setting")]
    public Material m_grayMat;
    public Material m_defalutMat;

    /// <summary>
    /// 创建置灰材质球
    /// </summary>
    /// <returns></returns>
    private Material GetGrayMat()
    {
        if (m_grayMat == null)
        {
            Shader shader = Shader.Find("Custom/UI-Gray");
            if (shader == null)
            {
                Debug.Log("null");
                return null;
            }
            Material mat = new Material(shader);
            m_grayMat = mat;
        }

        return m_grayMat;
    }

    /// <summary>
    /// 图片置灰
    /// </summary>
    /// <param name="img"></param>
    public void SetUIGray()
    {
        if (m_target == null) return;
        m_target.material = GetGrayMat();
        m_target.SetMaterialDirty();
    }

    /// <summary>
    /// 图片回复
    /// </summary>
    /// <param name="img"></param>
    public void Recovery()
    {
        if (m_target == null) return;
        m_target.material = m_defalutMat;
    }

}
