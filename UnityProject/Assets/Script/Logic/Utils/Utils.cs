using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

using System.Reflection;
using System.IO;
using System.Globalization;
using UnityEngine.UI;

namespace Dxx.Util
{
    //权重随机类
    public class WeightRandomDataBase
    {
        public int id;
        public int weight;
        public WeightRandomDataBase(int id)
        {
            this.id = id;
        }
    }

    public class WeightRandomCountData : WeightRandomDataBase
    {
        /// <summary>
        /// 连续随到的次数
        /// </summary>
        public int randomcount;
        /// <summary>
        /// 上次随到的序列ID
        /// </summary>
        public int lastrandomindex;
        public WeightRandomCountData(int id) : base(id)
        {

        }
        public void RandomSelf(int randomindex)
        {
            randomcount++;
            lastrandomindex = randomindex;
        }
        public bool GetCanRandom(int randomindex, int maxcount)
        {
            //Debug.Log(Utils.FormatString("GetCanRandom[{0}] randomcount:{1}, maxcount:{2}", id, randomcount, maxcount));
            if (lastrandomindex == randomindex)
            {//相邻两次随到自己
                if (randomcount >= maxcount)
                {
                    return false;
                }
            }
            else
            {//上次随的不是自己，则清除连续计数
                randomcount = 0;
            }
            return true;
        }
    }


    /// <summary>
    /// 工具类
    /// </summary>
    public static partial class Utils
    {
        public static float getAngle(Vector2 dir)
        {
            return getAngle(dir.x, dir.y);
        }

        public static float getAngle(Vector3 dir)
        {
            return getAngle(dir.x, dir.z);
        }
        public static float getAnglexy(Vector3 dir)
        {
            return getAngle(dir.x, dir.y);
        }
        /// <summary>
        /// currentAngle向targetAngle角度旋转，计算这次应该旋转到多少度
        /// </summary>
        /// <param name="currentAngle">当前角度</param>
        /// <param name="targetAngle">目标角度</param>
        /// <param name="maxRotateSpeed">最大角速度</param>
        /// <returns></returns>
        public static float getRotateNextAngle(float currentAngle, float targetAngle, float maxRotateSpeed)
        {
            Quaternion q = Quaternion.RotateTowards(Quaternion.Euler(0, currentAngle, 0), Quaternion.Euler(0, targetAngle, 0), maxRotateSpeed);
            return q.eulerAngles.y;

        }
        // public static Vector3 World2Screen(Vector3 worldpos)
        // {
        //     Vector3 pos = GameNode.m_Camera.WorldToViewportPoint(worldpos);
        //     pos = new Vector3(GameLogic.Width * pos.x, GameLogic.Height * pos.y, 0);
        //     //pos *= GameLogic.ResolutionScale;
        //     return pos;
        // }
        private static Vector3 GetDirection_dir = new Vector3();
        public static Vector3 GetDirection(float angle)
        {
            GetDirection_dir.x = MathDxx.Sin(angle);
            GetDirection_dir.y = 0;
            GetDirection_dir.z = MathDxx.Cos(angle);
            return GetDirection_dir;
        }
        private static Vector3 GetDirectionxy_dir = new Vector3();
        public static Vector3 GetDirectionxy(float angle)
        {
            GetDirectionxy_dir.x = MathDxx.Sin(angle);
            GetDirectionxy_dir.y = MathDxx.Cos(angle);
            GetDirectionxy_dir.z = 0;
            return GetDirectionxy_dir;
        }
        public static float GetDistance(Vector3 start, Vector3 end)
        {
            start.y = 0;
            end.y = 0;
            return Vector3.Distance(start, end);
        }
        private static float getAngle_angle;
        public static float getAngle(float x, float y)
        {
            getAngle_angle = 90f - Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            getAngle_angle = (getAngle_angle + 360f) % 360f;
            return GetFloat2(getAngle_angle);
        }

        public static float GetFloat1(float f)
        {
            return (int)(f * 10) / 10f;
        }

        public static float GetFloat2(float f)
        {
            return (int)(f * 100) / 100f;
        }

        public static float GetFloat3(float f)
        {
            return (int)(f * 1000) / 1000f;
        }

        public static int Ceil(float value)
        {
            return (int)System.Math.Ceiling(value);
        }

        public static int Floor(float value)
        {
            return (int)System.Math.Floor(value);
        }

        private static StringBuilder mStringBudier = new StringBuilder();
        public static string GetString(params object[] args)
        {
            //mStringBudier.Remove(0, mStringBudier.Length);
            mStringBudier.Clear();
            for (int i = 0, imax = args.Length; i < imax; i++)
            {
                mStringBudier.Append(args[i]);
            }
            return mStringBudier.ToString();
        }

        public static string FormatNumber(long num)
        {
            if (num < 10000)
            {
                return num.ToString();
            }
            else
            {
                long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
                if (i != 0)
                {
                    num = num / i * i;
                }
                if (num >= 10000000000)
                {
                    return GetString((num / 1000000000D).ToString("0"), "B");
                }
                if (num >= 10000000)
                {
                    return GetString((num / 1000000D).ToString("0"), "M");
                }
                return GetString((num / 1000D).ToString("0"), "K");
            }
        }

        public static string FormatNumberPlayerAttribute(long num)
        {
            // Ensure number has max 3 significant digits (no rounding up can happen)
            long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;

            if (num >= 10000000000)
                return GetString((num / 1000000000D).ToString("0"), "B");
            if (num >= 10000000)
                return GetString((num / 1000000D).ToString("0"), "M");
            if (num >= 100000)
                return GetString((num / 1000D).ToString("0"), "K");
            return num.ToString("#,0");
        }

        private static string[] timeFormatTranslateString = new string[4] {
                "time_day",
                "time_hour",
                "time_minute",
                "time_second",
        };
        // public static string GetFormatTimeShort(float time)
        // {
        //     string str = "";
        //     TimeSpan t = TimeSpan.FromSeconds(time);
        //     if (time < 0)
        //     {
        //         t = TimeSpan.FromSeconds(0);
        //     }
        //     object[] resultarr = new object[4];
        //     int[] spanarr = new int[4] { t.Days, t.Hours, t.Minutes, t.Seconds };

        //     if (t.Days != 0)
        //     {
        //         str = GetString(str, GameLogic.Hold.Language.GetLanguageByTID("time_day", t.Days));
        //     }

        //     if (t.Hours != 0)
        //     {
        //         str = GetString(str, GameLogic.Hold.Language.GetLanguageByTID("time_hour", t.Hours));
        //     }

        //     if (t.Minutes != 0)
        //     {
        //         str = GetString(str, GameLogic.Hold.Language.GetLanguageByTID("time_minute", t.Minutes));
        //     }

        //     if (t.Seconds != 0)
        //     {
        //         str = GetString(str, GameLogic.Hold.Language.GetLanguageByTID("time_second", t.Seconds));
        //     }
        //     return str;
        // }

        private static StringBuilder mFormatStringBudier = new StringBuilder();
        private static object mFormatLock = new object();
        public static string FormatString(string format, params object[] args)
        {
            lock (mFormatLock)
            {
                try
                {
                    //mFormatStringBudier.Remove(0, mFormatStringBudier.Length);
                    mFormatStringBudier.Clear();
                    mFormatStringBudier.AppendFormat(format, args);
                    return mFormatStringBudier.ToString();
                }
                catch (Exception e)
                {
                    // SdkManager.Bugly_Report("Utils.FormatString", "mFormatStringBudier try failure!!! string :" + format, e.StackTrace);
                    return format;
                }
            }
        }
        private static StringBuilder mFormatStringBudierThread = new StringBuilder();
        private static object mFormatThreadLock = new object();
        public static string FormatStringThread(string format, params object[] args)
        {
            lock (mFormatThreadLock)
            {
                try
                {
                    //mFormatStringBudier.Remove(0, mFormatStringBudier.Length);
                    mFormatStringBudierThread.Clear();
                    mFormatStringBudierThread.AppendFormat(format, args);
                    return mFormatStringBudierThread.ToString();
                }
                catch (Exception e)
                {
                    // SdkManager.Bugly_Report("Utils.FormatStringThread", "mFormatStringBudierThread try failure!!! string :" + format, e.StackTrace);
                    return format;
                }
            }
        }

        

        /// <summary>
        private static float timestamp_offset;
        private static long timestamp_ret;
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            return GetLocalTime();
            //if (NetManager.IsLogin == false)
            //{
            //    return NetManager.LocalTime;
            //}
            //else
            //{
            //    timestamp_offset = Timer.GetRealTime() - NetManager.unitytime;
            //    timestamp_ret = NetManager.NetTime + (long)timestamp_offset;
            //    //Debug.Log("NetManager.NetTime " + NetManager.NetTime + " + " + Time.realtimeSinceStartup + " - " + NetManager.unitytime + " = " + ret + " off " + offset);
            //    return timestamp_ret;
            //}
        }
        public static long GetLocalTime()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            long ret = Convert.ToInt64(ts.TotalSeconds);
            return ret;
        }
        public static System.DateTime GetCurrentDataTime()
        {
            return ConvertIntDateTime(GetTimeStamp());
        }
        public static System.TimeSpan GetTimeGoDays(double d)
        {
            System.DateTime dTime = Utils.ConvertIntDateTime(d);
            System.DateTime currentTime = Utils.GetCurrentDataTime();
            System.TimeSpan timeGo = currentTime - dTime;
            return timeGo;
        }


        static DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
        public static string NormalizeTimpstamp0(long timpStamp)
        {
            long unixTime = timpStamp * 10000000L;
            TimeSpan toNow = new TimeSpan(unixTime);
            DateTime dt = dtStart.Add(toNow);
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 时钟式倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetSecond3String(long second)
        {
            stringGetSecond3String.Remove(0, stringGetSecond3String.Length);
            stringGetSecond3String.AppendFormat("{0:D2}:{1:D2}:{2:D2}", second / 3600, second % 3600 / 60, second % 60);
            return stringGetSecond3String.ToString();
        }
        private static StringBuilder stringGetSecond3String = new StringBuilder();
        /// <summary>
        /// 时钟式倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetSecond2String(int second)
        {
            stringGetSecond2String.Remove(0, stringGetSecond2String.Length);
            stringGetSecond2String.AppendFormat("{0:D2}:{1:D2}", second / 60, second % 60);
            return stringGetSecond2String.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="second">毫秒级</param>
        /// <returns></returns>
        public static TimeSpan GetTime(long second)
        {
            TimeSpan ts = new TimeSpan(second * 1000 * 10000);
            return ts;
        }
        private static StringBuilder stringGetSecond2String = new StringBuilder();

        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            //Debug.Log(startTime);
            time = startTime.AddSeconds(d);

            return time;
        }


        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }




        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime, bool milliseconds = true)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            if (milliseconds)
            {
                return Convert.ToInt64((dateTime - start).TotalMilliseconds);
            }
            else
            {
                return Convert.ToInt64((dateTime - start).TotalSeconds);
            }
        }


        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(DateTime target, long timestamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }

        public static string CutString(string str, int maxlength)
        {
            if (str.Length > maxlength)
            {
                return FormatString("{0}...", str.Substring(0, maxlength - 3));
            }
            return str;
        }
        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        public static void ClearEvents(this object ctrl)
        {
            if (ctrl == null) return;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static;
            EventInfo[] events = ctrl.GetType().GetEvents(bindingFlags);
            if (events == null || events.Length < 1) return;

            for (int i = 0; i < events.Length; i++)
            {
                try
                {
                    EventInfo ei = events[i];

                    /********************************************************
                     * class的每个event都对应了一个同名(变了，前面加了Event前缀)的private的delegate类
                     * 型成员变量（这点可以用Reflector证实）。因为private成
                     * 员变量无法在基类中进行修改，所以为了能够拿到base 
                     * class中声明的事件，要从EventInfo的DeclaringType来获取
                     * event对应的成员变量的FieldInfo并进行修改
                     ********************************************************/
                    FieldInfo fi = ei.DeclaringType.GetField("Event" + ei.Name, bindingFlags);
                    if (fi != null)
                    {
                        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
                        fi.SetValue(ctrl, null);
                    }
                }
                catch { }
            }
        }
       
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            if (bytes != null)

            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)

                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            }
            return hexString;

        }
        /// <summary>
        /// A2->结果->[16][2]    将16进制字符串转化为16进制数组
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        /// <summary>
        /// byte[]   [16][2] -> A2  将16进制数组转化为16进制字符串  
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder builder_string = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder_string.Append(bytes[i].ToString("X2"));
            }
            return builder_string.ToString();
        }
        
        public static bool TryParseFloat(string str, out float value)
        {
            return float.TryParse(str, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out value);
        }
        public static float ParseFloat(string str)
        {
            float value = 0f;
            TryParseFloat(str, out value);
            return value;
        }
        public static int ParseInt(string str)
        {
            int value = 0;
            TryParseInt(str, out value);
            return value;
        }
        public static bool TryParseInt(string str, out int value)
        {
            return int.TryParse(str, out value);
        }
        public static float Distance(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;
            return Vector3.Distance(from, to);
        }
        public static string ByteArray2String(byte[] data)
        {
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }
        public static byte[] String2ByteArray(string s)
        {
            System.Text.RegularExpressions.CaptureCollection cs =
                System.Text.RegularExpressions.Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return data;
            //return Encoding.Unicode.GetString(data, 0, data.Length);
        }
       
    }
}
static public class GameTools
{
    // static public void RandomSort<T>(this List<T> list)
    // {
    //     for (int i = 0, imax = list.Count; i < imax; i++)
    //     {
    //         T temp = list[i];
    //         list.RemoveAt(i);
    //         int ran = GameLogic.Random(0, list.Count);
    //         list.Insert(ran, temp);
    //     }
    // }
    static public void DestroyChildren(this Transform t)
    {
        bool isPlaying = Application.isPlaying;

        while (t.childCount != 0)
        {
            Transform child = t.GetChild(0);

            if (isPlaying)
            {
                child.SetParent(null);
                UnityEngine.Object.Destroy(child.gameObject);
            }
            else UnityEngine.Object.DestroyImmediate(child.gameObject);
        }
    }

    static public void DestroyGameObject(this Transform t)
    {
        bool isPlaying = Application.isPlaying;
        if (isPlaying)
        {
            t.SetParent(null);
            UnityEngine.Object.Destroy(t.gameObject);
        }
        else UnityEngine.Object.DestroyImmediate(t.gameObject);
    }

    static public List<T> GetComponentsInChildrens<T>(this GameObject t) where T : Component
    {
        List<T> result = new List<T>();
        int childCount = t.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = t.transform.GetChild(i);
            T temp = child.GetComponent<T>();
            if (temp != null)
            {
                result.Add(temp);
            }
            List<T> templist = child.gameObject.GetComponentsInChildrens<T>();
            for (int j = 0; j < templist.Count; j++)
            {
                result.Add(templist[j]);
            }
        }
        return result;
    }

    public static void SetParentNormal(this GameObject child, Transform parent)
    {
        SetParentNormalInternal(child.transform, parent);
    }

    public static void SetParentNormal(this Transform child, GameObject parent)
    {
        SetParentNormalInternal(child, parent.transform);
    }

    public static void SetParentNormal(this GameObject child, GameObject parent)
    {
        SetParentNormalInternal(child.transform, parent.transform);
    }

    public static void SetParentNormal(this Transform child, Transform parent)
    {
        SetParentNormalInternal(child, parent);
    }
    public static string DebugParent(this Transform child)
    {
        Transform parent = child.parent;
        string parentstring = "parent : " + child.name;
        while (parent != null)
        {
            parentstring += "->" + parent.name;
            parent = parent.parent;
        }
        return parentstring;
    }


    private static void SetParentNormalInternal(Transform child, Transform parent)
    {
        child.SetParent(parent, false);
        RectTransform t = child as RectTransform;

        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
        if (t)
        {
            t.anchoredPosition = Vector2.zero;
        }
    }
    public static Transform GetFirstParent(this Transform t)
    {

        Transform parent = t.parent;
        Transform temp = parent;
        while (temp != null)
        {
            temp = temp.parent;
            if (temp != null)
            {
                parent = temp;
            }
        }
        return parent;
    }
    public static void SetLeft(this Transform t)
    {
        RectTransform t1 = t as RectTransform;
        if (t1)
        {
            SetLeftInternal(t1);
        }
    }
    public static void SetLeft(this GameObject o)
    {
        RectTransform t = o.transform as RectTransform;
        if (t)
        {
            SetLeftInternal(t);
        }
    }
    public static void SetLeft(this RectTransform t)
    {
        SetLeftInternal(t);
    }
    public static void SetLeftInternal(RectTransform t)
    {
        t.anchorMin = Vector2.up;
        t.anchorMax = Vector2.up;
        t.pivot = new Vector2(0, 0.5f);
    }
    public static void SetLeftTop(this Transform t)
    {
        RectTransform t1 = t as RectTransform;
        if (t1)
        {
            SetLeftTopInternal(t1);
        }
    }
    public static void SetLeftTop(this GameObject o)
    {
        RectTransform t = o.transform as RectTransform;
        if (t)
        {
            SetLeftTopInternal(t);
        }
    }
    public static void SetLeftTop(this RectTransform t)
    {
        SetLeftTopInternal(t);
    }
    public static void SetLeftTopInternal(RectTransform t)
    {
        t.anchorMin = Vector2.up;
        t.anchorMax = Vector2.up;
        t.pivot = new Vector2(0, 1);
    }
    public static void SetTop(this RectTransform t)
    {
        if (t)
        {
            SetTopInternal(t);
        }
    }
    public static void SetBottom(this RectTransform t)
    {
        if (t)
        {
            SetBottomInternal(t);
        }
    }
    public static void SetTop(this Transform t)
    {
        if (t)
        {
            RectTransform tt = t as RectTransform;
            if (tt)
            {
                SetTopInternal(tt);
            }
        }
    }
    public static void SetTopInternal(RectTransform t)
    {
        t.anchorMin = new Vector2(0.5f, 1);
        t.anchorMax = new Vector2(0.5f, 1);
        t.pivot = new Vector2(0.5f, 1);
    }

    public static void SetBottomInternal(RectTransform t)
    {
        t.anchorMin = new Vector2(0.5f, 0);
        t.anchorMax = new Vector2(0.5f, 0);
        t.pivot = new Vector2(0.5f, 0);
    }

    public static bool TryParseToFloat(string source, out float val)
    {
        return float.TryParse(source, NumberStyles.Float, CultureInfo.InvariantCulture, out val);
    }

    public static bool TryParseToLong(string source, out long val)
    {
        return long.TryParse(source, NumberStyles.Integer, CultureInfo.InvariantCulture, out val);
    }

    public static float ParseToFloat(string source, float defaultValue = 0)
    {
        float outputValue;
        if (TryParseToFloat(source, out outputValue))
        {
            return outputValue;
        }
        else
        {
            return defaultValue;
        }
    }

    public static long ParseToLong(string source, long defaultValue = 0)
    {
        long outputValue;
        if (TryParseToLong(source, out outputValue))
        {
            return outputValue;
        }
        else
        {
            return defaultValue;
        }
    }

    public static bool TryParseToDouble(string source, out double val)
    {
        return double.TryParse(source, NumberStyles.Float, CultureInfo.InvariantCulture, out val);
    }

    public static double ParseToDouble(string source, double defaultValue = 0)
    {
        double outputValue;
        if (double.TryParse(source, NumberStyles.Float, CultureInfo.InvariantCulture, out outputValue))
        {
            return outputValue;
        }
        else
        {
            return defaultValue;
        }
    }

    public static bool TryParseToInt(string source, out int val)
    {
        return int.TryParse(source, NumberStyles.Integer, CultureInfo.InvariantCulture, out val);
    }

    public static int ParseToInt(string source, int defaultValue = 0)
    {
        int outputValue;
        if (int.TryParse(source, NumberStyles.Integer, CultureInfo.InvariantCulture, out outputValue))
        {
            return outputValue;
        }
        else
        {
            return defaultValue;
        }
    }
    public static string Debug<T>(this List<T> list)
    {
        string str = typeof(T).ToString() + " ";
        for (int i = 0, imax = list.Count; i < imax; i++)
        {
            str += list[i];
            if (i < imax - 1)
            {
                str += ",";
            }
        }
        return str;
    }
    public static string Debug<T>(this T[] list)
    {
        string str = typeof(T).ToString() + " ";
        for (int i = 0, imax = list.Length; i < imax; i++)
        {
            str += list[i];
            if (i < imax - 1)
            {
                str += ",";
            }
        }
        return str;
    }

    
    public static bool IsHave(this ulong value, int index)
    {
        ulong current = (ulong)(1L << index);
        bool result = (value & current) == current;
        return result;
    }
    public static void UpdateMeshLayer(this GameObject o, string LayerName, int order, bool changechild)
    {
        if (!changechild)
        {
            MeshRenderer mesh = o.GetComponent<MeshRenderer>();
            if (mesh)
            {
                mesh.sortingLayerName = LayerName;
                mesh.sortingOrder = order;
            }
        }
        else
        {
            MeshRenderer[] meshs = o.GetComponentsInChildren<MeshRenderer>(true);
            if (meshs != null)
            {
                for (int i = 0, imax = meshs.Length; i < imax; i++)
                {
                    if (meshs[i] != null)
                    {
                        meshs[i].sortingLayerName = LayerName;
                        meshs[i].sortingOrder = order;
                    }
                }
            }
        }
    }
    public static void UpdateParticlesLayer(this GameObject o, string LayerName, int order)
    {
        Renderer[] renders = o.GetComponentsInChildren<Renderer>(true);
        if (renders != null)
        {
            for (int i = 0, imax = renders.Length; i < imax; i++)
            {
                if (renders[i] != null)
                {
                    renders[i].sortingLayerName = LayerName;
                    renders[i].sortingOrder = order;
                }
            }
        }
    }

    public static void ForceRebuildLayout(this GameObject o)
    {
        o.transform.ForceRebuildLayout();
    }

    public static void ForceRebuildLayout(this Transform o)
    {
        var rect = o as RectTransform;
        if (rect != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
    /// <summary>
    /// 设置transform的四边的距离
    /// </summary>
    /// <param name="t"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="top"></param>
    /// <param name="bottom"></param>
    public static void SetStretch(this RectTransform t, float left, float right, float top, float bottom)
    {
        t.offsetMin = new Vector2(left, bottom);
        t.offsetMax = new Vector2(-right, -top);
    }
}
