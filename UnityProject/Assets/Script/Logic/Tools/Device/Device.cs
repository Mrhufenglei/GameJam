//======================================================================== 
//	
// 	 Maggic @ 2020/3/4 16:39:04　　　　　　　
// 	
//========================================================================

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Device : Singleton<Device>
{
    /// <summary>
    /// 需要不需要适配
    /// </summary>
    /// <returns></returns>
    public bool IsNeedSpecialAdapte()
    {
        bool flag;

#if UNITY_EDITOR
        flag = IsHasNotch();
#elif UNITY_IPHONE
        var generation = UnityEngine.iOS.Device.generation;

        switch(generation)
        {
            case UnityEngine.iOS.DeviceGeneration.iPhoneX:
            case UnityEngine.iOS.DeviceGeneration.iPhoneXS:
            case UnityEngine.iOS.DeviceGeneration.iPhoneXSMax:
            case UnityEngine.iOS.DeviceGeneration.iPhoneXR:
                flag = true;
                break;
            default:
                flag = false;
                break;
        }
#elif UNITY_ANDROID
        // 是不是刘海屏
        flag = IsHasNotch();
#else
        flag = IsHasNotch();
#endif
        return flag;
    }


#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")] private static extern void setVibratorIOS();
#endif
	/// <summary>
	/// 震动功能
	/// </summary>
	public void Vibrator()
	{
#if UNITY_IOS && !UNITY_EDITOR
		setVibratorIOS();
#else
		Debug.LogWarningFormat("The current platform({0}) has not implemented Vibrator.", Application.platform.ToString());
#endif
	}

    /// <summary>
    /// 是不是刘海屏
    /// </summary>
    /// <returns></returns>
    private bool IsHasNotch()
    {
        bool isHasNotch = false;
        float s = Screen.height / (float)Screen.width;
        if (s >= 2.0f)
        {
            isHasNotch = true;
        }
        return isHasNotch;
    }
}
