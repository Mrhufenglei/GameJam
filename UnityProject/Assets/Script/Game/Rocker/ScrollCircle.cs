using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Dxx.Util;

public enum JoyNameType
{
    MoveJoy,
    AttackJoy,
}
public struct JoyData
{
    public string name;
    public Vector3 direction;
    //移动方向，可能跟摇杆方向不一致
    public Vector3 _moveDirection;
    public Vector3 MoveDirection
    {
        get
        {
            return _moveDirection == Vector3.zero ? direction : _moveDirection;
        }
    }

    public float angle;
    public float length;
    public int type;
    //播放的动作路径
    public string action;
    public void Revert()
    {
        direction *= -1;
        angle = (angle + 180f) % 360f;
    }
    public void UpdateDirectionByAngle(float angle)
    {
        this.angle = angle;
        direction.x = MathDxx.Sin(angle);
        direction.z = MathDxx.Cos(angle);
    }
}

public class ScrollCircle : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    #region delegate
    public delegate void JoyTouchStart(JoyData data);
    public delegate void JoyTouching(JoyData data);
    public delegate void JoyTouchEnd(JoyData data);
    #endregion
    #region events
    public static event JoyTouchStart On_JoyTouchStart = null;
    public static event JoyTouching On_JoyTouching = null;
    public static event JoyTouchEnd On_JoyTouchEnd = null;
    #endregion
    //玩家双击大招事件
    public static System.Action OnDoubleClick;
    [SerializeField]
    private JoyNameType JoyType;
    private Dictionary<JoyNameType, string> JoyDic = new Dictionary<JoyNameType, string>() {
        {JoyNameType.MoveJoy,  "MoveJoy" },
        {JoyNameType.AttackJoy, "AttackJoy" }
    };
    protected Vector2 Origin;
    protected float mRadius = 0f;
    protected float mRadiusSmall;//摇杆不出圈
    protected Transform child;
    protected Transform bgParent;
    protected Transform bgParengbgbg;
    protected Transform touch;
    protected Transform direction;

    private Vector3 StartPos;
    //是否显示箭头
    private bool bShowDirection = true;

    protected JoyData m_Data = new JoyData();

    private bool disable = false;
    private bool touchdown = false;
    private Vector3 touchdownpos;
    //摇杆按钮是否在圆底内
    private static bool TouchIn = true;
    private int mTouchID = -1;

    /// <summary>
    /// 双击相关
    /// </summary>
    private float fClickTime = 0f;
    private float ClickDelayTime = 0.2f;
    private Animator mAni_ScreenTouch;
    private bool bDrag = false;
    void OnEnable()
    {
        disable = false;
        touchdown = false;
        (transform.parent as RectTransform).SetAsFirstSibling();
    }
    void OnDisable()
    {
        on_point_up(null);
        bDrag = false;
        // if (GameLogic.Self && GameLogic.Self.m_MoveCtrl != null)
        // {
        //     GameLogic.Self.m_MoveCtrl.ResetRigidBody();
        // }
        mTouchID = -1;
        disable = true;
        touchdown = false;
    }

    void Awake()
    {
        ClickDelayTime = 0.3f;
        //mAni_ScreenTouch = transform.Find("panel/ScreenTouch").GetComponent<Animator>();
        child = transform.Find("panel/bg");
        bgParent = child.transform.Find("bgParent");
        bgParengbgbg = bgParent.Find("bg/bg");
        touch = child.transform.Find("touch");
        direction = bgParent.transform.Find("direction");
        StartPos = child.localPosition;
        // if (GameLogic.DebugMode)
        // {
        //     touch.localScale = Vector3.one * SettingDebugMediator.JoyScaleTouch / 100f;
        // }
        //计算摇杆块的半径
        mRadius = (child as RectTransform).sizeDelta.x * 0.5f;
        // if (GameLogic.DebugMode)
        // {
        //     mRadius *= SettingDebugMediator.JoyRadius / 100f;
        // }
        mRadiusSmall = mRadius;
        if (TouchIn)
        {

            // if (GameLogic.DebugMode)
            // {
            //     mRadiusSmall = mRadius - (touch as RectTransform).sizeDelta.x * 0.5f * SettingDebugMediator.JoyRadius / 100f * touch.localScale.x;
            // }
            // else
            {
                mRadiusSmall = mRadius - (touch as RectTransform).sizeDelta.x * 0.5f * touch.localScale.x;
            }

        }
        // if (GameLogic.DebugMode)
        // {
        //     mRadius *= SettingDebugMediator.JoyScaleBG / 100f;
        //     mRadiusSmall *= SettingDebugMediator.JoyScaleBG / 100f;
        // }
        //Debugger.Log("mRadius " + mRadius + " mRadiusSmall " + mRadiusSmall + " touch "+ ((touch as RectTransform).sizeDelta.x));
        m_Data.name = JoyDic[JoyType];
        // if (m_Data.name == ConstString.MoveJoy)
        // {
        //     m_Data.action = AnimationCtrlBase.Run;
        // }

        // if (GameLogic.DebugMode)
        // {
        //     bgParent.Find("bg/bg").localScale = Vector3.one * SettingDebugMediator.JoyScaleBG / 100f;
        // }
        //child.gameObject.SetActive(false);
        direction.gameObject.SetActive(bShowDirection);
        // SettingDebugMediator.OnValueChange = OnValueChange;
        m_Data.direction = new Vector3();
    }
    private void OnValueChange()
    {
        // if (GameLogic.DebugMode)
        // {
        //     mRadius = (child as RectTransform).sizeDelta.x * 0.5f * SettingDebugMediator.JoyRadius / 100f;
        // }
        // else
        {
            mRadius = (child as RectTransform).sizeDelta.x * 0.5f;
        }
        mRadiusSmall = mRadius;
        if (TouchIn)
        {
            // if (GameLogic.DebugMode)
            // {
            //     mRadiusSmall = mRadius - (touch as RectTransform).sizeDelta.x * 0.5f * SettingDebugMediator.JoyRadius / 100f * touch.localScale.x;
            // }
            // else
            {
                mRadiusSmall = mRadius - (touch as RectTransform).sizeDelta.x * 0.5f * touch.localScale.x;
            }
        }
        // if (GameLogic.DebugMode)
        // {
        //     mRadius *= SettingDebugMediator.JoyScaleBG / 100f;
        //     mRadiusSmall *= SettingDebugMediator.JoyScaleBG / 100f;
        //     bgParengbgbg.localScale = Vector3.one * SettingDebugMediator.JoyScaleBG / 100f;
        // }
        // else
        {
            bgParengbgbg.localScale = Vector3.one;
        }
    }


    Vector3 pos_v;
    float pos_w;
    Vector3 pos_2 = new Vector3(0.5f, 0.5f, 0);

    private const float DesignHeight = 1280;
    private const float DesignWidth = 720;
    private Vector3 GetPos(Vector3 pos)
    {
        var uicam = GameApp.UI.UICamera;
        pos_v = uicam.ScreenToViewportPoint(pos) - pos_2;
        pos_w = DesignHeight / (Screen.height + 0f) * Screen.width;
        // pos_w = /*GameLogic.DesignHeight / (Screen.height + 0f) **/  Screen.width;

        // Debug.Log("ScreenHW");
        // Debug.Log(Screen.width);
        // Debug.Log(Screen.height);

        return new Vector3(pos_w * pos_v.x, DesignHeight * pos_v.y, 0);
        // return new Vector3(pos_w * pos_v.x, Screen.height * pos_v.y, 0);

        //#endif
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mTouchID = eventData.pointerId;
        //Debug.Log("eventData.position " + eventData.position);
        OnPointerDown(GetPos(eventData.position));
    }
    private void OnPointerDown(Vector3 pos)
    {
        if (disable)
            return;
        bDrag = false;
        //touchdownpos = pos;
        //touchdown = true;
        // if (OnDoubleClick != null)
        // {
        //     GameObject o = GameLogic.EffectGet("Game/UI/ScreenTouch");
        //     o.transform.SetParent(GameNode.m_Joy);
        //     o.transform.position = pos;

        //     if (Updater.AliveTime - fClickTime < ClickDelayTime)
        //     {//距离上次摇杆抬起时间 足够短
        //         fClickTime = -1f;
        //         if (GameLogic.Self)
        //         {
        //             OnDoubleClick();
        //         }
        //     }
        //     else
        //     {
        //         fClickTime = Updater.AliveTime;
        //     }
        // }
        // else
        {
            //Debug.Log("point down pos " + pos);
            touchdownpos = pos;
            touchdown = true;

            child.localPosition = pos;
            child.gameObject.SetActive(true);
            Origin = pos;
            DealDrag(Origin);
            if (On_JoyTouchStart != null)
            {
                On_JoyTouchStart(m_Data);
            }
        }

    }
    public void OnDrag(PointerEventData eventData)
    {

        if (disable)
        {
            return;
        }
        if (mTouchID != eventData.pointerId)
        {
            return;
        }
        if (!touchdown)
        {
            child.gameObject.SetActive(true);
            Vector3 pos = GetPos(eventData.position);
            touchdownpos = pos;
            touchdown = true;

            child.localPosition = pos;
            Origin = pos;
            DealDrag(Origin);
            if (On_JoyTouchStart != null)
            {
                On_JoyTouchStart(m_Data);
            }
            //OnPointerDown(touchdownpos);
        }
        bDrag = true;
        DealDrag(GetPos(eventData.position));
        if (On_JoyTouching != null)
        {
            On_JoyTouching(m_Data);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        if (disable)
            return;
        if (mTouchID != eventData.pointerId)
            return;
        //child.gameObject.SetActive(false);
        touchdown = false;
        on_point_up(eventData);


    }
    private void on_point_up(PointerEventData eventData)
    {
        if (bDrag)
        {
            fClickTime = -1f;
        }

        if (On_JoyTouchEnd != null)
        {
            touch.localPosition = Vector2.zero;
            On_JoyTouchEnd(m_Data);
        }
        if (child)
        {
            child.localPosition = StartPos;
            direction.localRotation = Quaternion.identity;
            //child.gameObject.SetActive(false);
        }
    }
    private Vector2 DealDrag_touchpos;
    private Vector2 DealDrag_touchpos1;
    /// <summary>
    /// //摇杆中心跟随移动
    /// </summary>
    private const bool JoyFollowTouch = false;
    private void DealDrag(Vector2 pos, bool updateui = true)
    {
        DealDrag_touchpos = pos - Origin;
        float length = DealDrag_touchpos.magnitude;
        if (length > mRadius)
        {
            DealDrag_touchpos = DealDrag_touchpos.normalized * mRadius;
        }
        if (JoyFollowTouch && length > mRadiusSmall)
        {
            //摇杆中心跟随移动
            child.localPosition = Origin + DealDrag_touchpos.normalized * (length - mRadiusSmall);
            Origin = child.localPosition;
        }
        DealDrag_touchpos1 = DealDrag_touchpos;
        if (DealDrag_touchpos1.magnitude > mRadiusSmall)
        {
            DealDrag_touchpos1 = DealDrag_touchpos1.normalized * mRadiusSmall;
        }

        m_Data.length = DealDrag_touchpos.magnitude;
        m_Data.direction.x = DealDrag_touchpos.normalized.x;
        m_Data.direction.z = DealDrag_touchpos.normalized.y;// * GameLogic.RoomScaleZ;
        m_Data.angle = Utils.getAngle(m_Data.direction);
        if (updateui)
        {
            touch.localPosition = DealDrag_touchpos1;
            direction.localRotation = Quaternion.Euler(0, 0, -m_Data.angle);
        }

    }
}