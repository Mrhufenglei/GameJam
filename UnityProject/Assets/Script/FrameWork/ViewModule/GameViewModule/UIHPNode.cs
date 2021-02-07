//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 19:10:05
//
//----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class UIHPNode : MonoBehaviour
{
    public BaseMember m_target;

    [Header("Slider Setting")] public RectTransform m_sliderBg;
    public RectTransform m_sliderFg;

    private RectTransform m_rectTransform;

    public void SetMember(BaseMember member)
    {
        m_target = member;
    }

    public void OnInit()
    {
        m_rectTransform = transform as RectTransform;
    }

    public void OnDeInit()
    {
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        UpdatePosition();
        UpdateSliderValue();
    }

    private void SetSliderValue(float value)
    {
        if (m_sliderBg == null) return;
        if (m_sliderFg == null) return;
        m_sliderFg.sizeDelta = new Vector2(m_sliderBg.sizeDelta.x * value, m_sliderFg.sizeDelta.y);
    }

    private void UpdatePosition()
    {
        if (m_target == null) return;
        if (GameController.Builder.m_cameraController == null) return;
        if (GameController.Builder.m_cameraController.m_camera == null) return;
        if (GameApp.UI.UICamera == null) return;
        var point =
            GameController.Builder.m_cameraController.m_camera.WorldToScreenPoint(
                m_target.gameObject.transform.TransformPoint(new Vector3(0, 0, 0.5f)));
        Vector3 pos = GameApp.UI.UICamera.ScreenToWorldPoint(point);
        m_rectTransform.position = pos;
        m_rectTransform.anchoredPosition = new Vector3(m_rectTransform.anchoredPosition.x, m_rectTransform.anchoredPosition.y, 0);
    }

    private void UpdateSliderValue()
    {
        if (m_target == null) return;
        if (m_target.m_memberData == null) return;
        SetSliderValue(m_target.HP / m_target.m_memberData.m_hpMax);
    }
}