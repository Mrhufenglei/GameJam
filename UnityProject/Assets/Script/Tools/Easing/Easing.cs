//======================================================================== 
//	
// 	 Maggic @ 2019/2/14 11:40:18 　　　　　　　　
// 	
//========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Easing
{
    public static Vector2 EasingVector2(float t, Vector2 from, Vector2 to, float d, EaseType easeType = EaseType.Linear)
    {
        float x = EasingFloat(t, from.x, to.x, d, easeType);
        float y = EasingFloat(t, from.y, to.y, d, easeType);
        return new Vector2(x, y);
    }
    public static Color EasingColor(float t, Color from, Color to, float d, EaseType easeType = EaseType.Linear)
    {
        float progress = EasingFloat(t, 0, 1, d, easeType);
        return Color.Lerp(from, to, progress);
    }
    public static Vector3 EasingVector3(float t, Vector3 from, Vector3 to, float d, EaseType easeType = EaseType.Linear)
    {
        float progress = EasingFloat(t, 0, 1, d, easeType);
        return Vector3.Lerp(from, to, progress);
    }
    public static Quaternion EasingQuaternion(float t, Vector3 from, Vector3 to, float d, EaseType easeType = EaseType.Linear)
    {
        return EasingQuaternion(t, Quaternion.Euler(from), Quaternion.Euler(to), d, easeType);
    }
    public static Quaternion EasingQuaternion(float t, Quaternion from, Quaternion to, float d, EaseType easeType = EaseType.Linear)
    {
        float progress = EasingFloat(t, 0, 1, d, easeType);
        return Quaternion.Lerp(from, to, progress);
    }
    public static float EasingFloat(float t, float from, float to, float d, EaseType easeType = EaseType.Linear)
    {
        float ch = to - from;
        switch (easeType)
        {
            case EaseType.Linear:
                ch = Linear(t, from, ch, d);
                break;
            case EaseType.InOutCubic:
                ch = InOutCubic(t, from, ch, d);
                break;
            case EaseType.InOutQuintic:
                ch = InOutQuintic(t, from, ch, d);
                break;
            case EaseType.InQuintic:
                ch = InQuintic(t, from, ch, d);
                break;
            case EaseType.InQuartic:
                ch = InQuartic(t, from, ch, d);
                break;
            case EaseType.InCubic:
                ch = InCubic(t, from, ch, d);
                break;
            case EaseType.InQuadratic:
                ch = InQuadratic(t, from, ch, d);
                break;
            case EaseType.OutQuintic:
                ch = OutQuintic(t, from, ch, d);
                break;
            case EaseType.OutQuartic:
                ch = OutQuartic(t, from, ch, d);
                break;
            case EaseType.OutCubic:
                ch = OutCubic(t, from, ch, d);
                break;
            case EaseType.OutInCubic:
                ch = OutInCubic(t, from, ch, d);
                break;
            case EaseType.BackInCubic:
                ch = BackInCubic(t, from, ch, d);
                break;
            case EaseType.BackInQuartic:
                ch = BackInQuartic(t, from, ch, d);
                break;
            case EaseType.OutBackCubic:
                ch = OutBackCubic(t, from, ch, d);
                break;
            case EaseType.OutBackQuartic:
                ch = OutBackQuartic(t, from, ch, d);
                break;
            case EaseType.OutElasticSmall:
                ch = OutElasticSmall(t, from, ch, d);
                break;
            case EaseType.OutElasticBig:
                ch = OutElasticBig(t, from, ch, d);
                break;
            case EaseType.InElasticSmall:
                ch = InElasticSmall(t, from, ch, d);
                break;
            case EaseType.InElasticBig:
                ch = InElasticBig(t, from, ch, d);
                break;
            default:
                break;
        }
        return ch;
    }

    #region Easing
    private static float Linear(float time, float begion, float change, float duration)
    {
        time /= duration;
        return begion + change * (time);
    }
    private static float InOutCubic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (-2 * tc + 3 * ts);
    }

    private static float InOutQuintic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (6 * tc * ts + -15 * ts * ts + 10 * tc);
    }
    private static float InQuintic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (tc * ts);
    }
    private static float InQuartic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        //float tc = ts * time;
        return begion + change * (ts * ts);
    }
    private static float InCubic(float time, float begion, float change, float duration)
    {
        float tc = (time /= duration) * time * time;
        return begion + change * (tc);
    }
    private static float InQuadratic(float time, float begion, float change, float duration)
    {
        float ts = (time /= duration) * time;
        return begion + change * (ts);
    }
    private static float OutQuintic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (tc * ts + -5 * ts * ts + 10 * tc + -10 * ts + 5 * time);
    }
    private static float OutQuartic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (-1 * ts * ts + 4 * tc + -6 * ts + 4 * time);
    }
    private static float OutCubic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (tc + -3 * ts + 3 * time);
    }
    private static float OutInCubic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (4 * tc + -6 * ts + 3 * time);
    }
    private static float OutInQuartic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (6 * tc + -9 * ts + 4 * time);
    }
    private static float BackInCubic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (4 * tc + -3 * ts);
    }
    private static float BackInQuartic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (2 * ts * ts + 2 * tc + -3 * ts);
    }
    private static float OutBackCubic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (4 * tc + -9 * ts + 6 * time);
    }
    private static float OutBackQuartic(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (-2 * ts * ts + 10 * tc + -15 * ts + 8 * time);
    }
    private static float OutElasticSmall(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (33 * tc * ts + -106 * ts * ts + 126 * tc + -67 * ts + 15 * time);
    }
    private static float OutElasticBig(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (56 * tc * ts + -175 * ts * ts + 200 * tc + -100 * ts + 20 * time);
    }
    private static float InElasticSmall(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (33 * tc * ts + -59 * ts * ts + 32 * tc + -5 * ts);
    }
    private static float InElasticBig(float time, float begion, float change, float duration)
    {
        float ts = ((time /= duration) * time);
        float tc = ts * time;
        return begion + change * (56 * tc * ts + -105 * ts * ts + 60 * tc + -10 * ts);
    }

    #endregion
}
