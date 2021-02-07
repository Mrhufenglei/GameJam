using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dxx.Util
{
    public class MathDxx
    {

        public static int CeilToInt(float value)
        {
            return Mathf.CeilToInt(value);
        }
        public static int FloorToInt(float value)
        {
            return Mathf.FloorToInt(value);
        }
        public static int CeilBig(float value)
        {
            return GetSymbol(value) * Mathf.CeilToInt(Mathf.Abs(value));
        }
        public static int GetSymbol(float value)
        {
            return value > 0 ? 1 : -1;
        }
        public static int GetSymbol(int value)
        {
            return value > 0 ? 1 : -1;
        }
        public static int GetSymbol(long value)
        {
            return value > 0 ? 1 : -1;
        }
        public static string GetSymbolString(long value)
        {
            return value >= 0 ? "+" : "-";
        }
        public static string GetSymbolString(int value)
        {
            return value >= 0 ? "+" : "-";
        }
        public static string GetSymbolString(float value)
        {
            return value >= 0 ? "+" : "-";
        }
        public static float Sin(float angle)
        {
            return Mathf.Sin(angle * Mathf.PI / 180f);
        }
        public static float Cos(float angle)
        {
            return Mathf.Cos(angle * Mathf.PI / 180f);
        }
        public static int Abs(int value)
        {
            return Mathf.Abs(value);
        }
        public static float Abs(float value)
        {
            return Mathf.Abs(value);
        }
        public static long Abs(long value)
        {
            if (value > 0)
            {
                return value;
            }
            else
            {
                return -value;
            }
        }
        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            return Mathf.MoveTowardsAngle(current, target, maxDelta);
        }
        public static float Clamp(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }
        public static int Clamp(int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }
        public static long Clamp(long value, long min, long max)
        {
            if (value < min) {
                value = min;
            } else if (value > max) {
                value = max;
            }
            return value;
        }
        public static Vector3 Clamp(Vector3 value, float min, float max)
        {
            value.x = Mathf.Clamp(value.x, min, max);
            value.y = Mathf.Clamp(value.y, min, max);
            value.z = Mathf.Clamp(value.z, min, max);
            return value;
        }
        public static float Clamp01(float value)
        {
            return Mathf.Clamp01(value);
        }
        public static float Pow(float f, float p) {
            return Mathf.Pow(f, p);
        }
        // public static int RandomSymbol()
        // {
        //     return GameLogic.Random(0, 2) == 0 ? 1 : -1;
        // }
        // public static bool RandomBool()
        // {
        //     return GameLogic.Random(0, 2) == 0 ? true : false;
        // }
        public static int RoundToInt(float value)
        {
            return Mathf.RoundToInt(value);
        }
        public static float DistanceScaleZ(Vector3 v1, Vector3 v2)
        {
            return Mathf.Sqrt(DistanceScaleZPow(v1, v2));
        }
        public static float DistanceScaleZPow(Vector3 v1, Vector3 v2)
        {
            float x = v1.x - v2.x;
            float y = (v1.z - v2.x);// / GameLogic.RoomScaleZ;
            return x * x + y * y;
        }
        public static bool IsZero(float value)
        {
            return Abs(value) < 0.00001f;
        }
    }

}
