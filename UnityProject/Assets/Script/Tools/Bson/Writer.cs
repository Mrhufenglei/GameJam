using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Foundation
{
    public partial class Bson
    {
        public sealed class Writer : IDisposable
        {
            private readonly MemoryStream _stream;
            private readonly BinaryWriter _writer;
            private bool _isDisposed;

            public byte[] Bytes
            {
                get
                {
                    if (_stream == null) return null;

                    var bytes = new byte[_stream.Length];
                    Array.Copy(_stream.GetBuffer(), bytes, bytes.Length);
                    return bytes;
                }
            }

            public Writer(MemoryStream stream = null)
            {
                _stream = stream ?? new MemoryStream();
                _writer = new BinaryWriter(_stream);
            }

            public void Encode(object obj)
            {
                if (obj == null)
                {
                   Debug.LogError("Obj is null, encode return.");
                    return;
                }

                //Log.Debug($"BSON Encode: {obj.GetType()} - {obj}");

                switch (obj)
                {
                    case bool v:
                        Write(v);
                        return;
                    case sbyte v:
                        Write(v);
                        return;
                    case byte v:
                        Write(v);
                        return;
                    case char v:
                        Write(v);
                        return;
                    case short v:
                        Write(v);
                        return;
                    case ushort v:
                        Write(v);
                        return;
                    case int v:
                        Write(v);
                        return;
                    case uint v:
                        Write(v);
                        return;
                    case long v:
                        Write(v);
                        return;
                    case ulong v:
                        Write(v);
                        return;
                    case decimal v:
                        Write(v);
                        return;
                    case float v:
                        Write(v);
                        return;
                    case double v:
                        Write(v);
                        return;
                    case DateTime v:
                        Write(v);
                        return;
                    case Enum _:
                        WriteEnum(obj);
                        return;
                    case string v:
                        Write(v);
                        return;
                    case IList v:
                        Write(v);
                        return;
                    case IDictionary v:
                        Write(v);
                        return;
                    case Vector2 v:
                        Write(v);
                        return;
                    case Vector3 v:
                        Write(v);
                        return;
                    case Vector4 v:
                        Write(v);
                        return;
                    case Color v:
                        Write(v);
                        return;
                    case Color32 v:
                        Write(v);
                        return;
                    case Quaternion v:
                        Write(v);
                        return;
                    case Bounds v:
                        Write(v);
                        return;
                    case Rect v:
                        Write(v);
                        return;
                    case Matrix4x4 v:
                        Write(v);
                        return;
                    default: // it looks like the param object should be exported as an object.
                        Write(obj);
                        return;
                }
            }

            private void Write(bool value)
            {
                _writer.Write((byte) ValueType.Bool);
                _writer.Write(value);
            }

            private void Write(sbyte value)
            {
                _writer.Write((byte) ValueType.SByte);
                _writer.Write(value);
            }

            private void Write(byte value)
            {
                _writer.Write((byte) ValueType.Byte);
                _writer.Write(value);
            }

            private void Write(char value)
            {
                _writer.Write((byte) ValueType.Char);
                _writer.Write(value);
            }

            private void Write(short value)
            {
                _writer.Write((byte) ValueType.Short);
                _writer.Write(value);
            }

            private void Write(ushort value)
            {
                _writer.Write((byte) ValueType.UShort);
                _writer.Write(value);
            }

            private void Write(int value)
            {
                _writer.Write((byte) ValueType.Int);
                _writer.Write(value);
            }

            private void Write(uint value)
            {
                _writer.Write((byte) ValueType.UInt);
                _writer.Write(value);
            }

            private void Write(long value)
            {
                _writer.Write((byte) ValueType.Int64);
                _writer.Write(value);
            }

            private void Write(ulong value)
            {
                _writer.Write((byte) ValueType.UInt64);
                _writer.Write(value);
            }

            private void Write(decimal value)
            {
                _writer.Write((byte) ValueType.Decimal);
                _writer.Write(value);
            }

            private void Write(float value)
            {
                _writer.Write((byte) ValueType.Float);
                _writer.Write(value);
            }

            private void Write(double value)
            {
                _writer.Write((byte) ValueType.Double);
                _writer.Write(value);
            }

            private void Write(DateTime value)
            {
                var span = value.Kind == DateTimeKind.Local
                    ? value - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()
                    : value - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                _writer.Write((byte) ValueType.DateTime);
                _writer.Write((long) span.TotalSeconds * 1000); // ms
            }

            private void WriteEnum(object value)
            {
                _writer.Write((byte) ValueType.Enum);
                _writer.Write((int) value);
            }

            private void Write(string value)
            {
                _writer.Write((byte) ValueType.String);
                _writer.Write(value);
            }

            private void Write(IList value)
            {
                using (var aw = new Writer())
                {
                    foreach (var item in value)
                    {
                        if (item == null) continue;

                        aw.Encode(item);
                    }

                    var len = (int) aw._stream.Length;
                    _writer.Write((byte) ValueType.Array);
                    _writer.Write(len);
                    _writer.Write(aw._stream.GetBuffer(), 0, len);
                }
            }

            private void Write(IDictionary value)
            {
                using (var dw = new Writer())
                {
                    foreach (DictionaryEntry entry in value)
                    {
                        if (entry.Value == null) continue;

                        dw._writer.Write(entry.Key.ToString());
                        //dw.Encode(entry.Key);
                        dw.Encode(entry.Value);
                    }

                    var len = (int) dw._stream.Length;
                    _writer.Write((byte) ValueType.Object);
                    _writer.Write(len);
                    _writer.Write(dw._stream.GetBuffer(), 0, len);
                }
            }

            private void Write(Vector2 value)
            {
                _writer.Write((byte) ValueType.Vector2);
                _writer.Write(value.x);
                _writer.Write(value.y);
            }

            private void Write(Vector3 value)
            {
                _writer.Write((byte) ValueType.Vector3);
                _writer.Write(value.x);
                _writer.Write(value.y);
                _writer.Write(value.z);
            }

            private void Write(Vector4 value)
            {
                _writer.Write((byte) ValueType.Vector4);
                _writer.Write(value.x);
                _writer.Write(value.y);
                _writer.Write(value.z);
                _writer.Write(value.w);
            }

            private void Write(Color value)
            {
                _writer.Write((byte) ValueType.Color);
                _writer.Write(value.r);
                _writer.Write(value.g);
                _writer.Write(value.b);
                _writer.Write(value.a);
            }

            private void Write(Color32 value)
            {
                _writer.Write((byte) ValueType.Color32);
                _writer.Write(value.r);
                _writer.Write(value.g);
                _writer.Write(value.b);
                _writer.Write(value.a);
            }

            private void Write(Quaternion value)
            {
                _writer.Write((byte) ValueType.Quaternion);
                _writer.Write(value.x);
                _writer.Write(value.y);
                _writer.Write(value.z);
                _writer.Write(value.w);
            }

            private void Write(Bounds value)
            {
                _writer.Write((byte) ValueType.Bounds);
                _writer.Write(value.center.x);
                _writer.Write(value.center.y);
                _writer.Write(value.center.z);
                _writer.Write(value.extents.x);
                _writer.Write(value.extents.y);
                _writer.Write(value.extents.z);
            }

            private void Write(Rect value)
            {
                _writer.Write((byte) ValueType.Rect);
                _writer.Write(value.x);
                _writer.Write(value.y);
                _writer.Write(value.width);
                _writer.Write(value.height);
            }

            private void Write(Matrix4x4 value)
            {
                _writer.Write((byte) ValueType.Matrix);
                _writer.Write(value.m00);
                _writer.Write(value.m10);
                _writer.Write(value.m20);
                _writer.Write(value.m30);
                _writer.Write(value.m01);
                _writer.Write(value.m11);
                _writer.Write(value.m21);
                _writer.Write(value.m31);
                _writer.Write(value.m02);
                _writer.Write(value.m12);
                _writer.Write(value.m22);
                _writer.Write(value.m32);
                _writer.Write(value.m03);
                _writer.Write(value.m13);
                _writer.Write(value.m23);
                _writer.Write(value.m33);
            }

            private void Write(object value)
            {
                using (var ow = new Writer())
                {
                    var props = AddPropertyMetas(value.GetType());
                    foreach (var prop in props)
                    {
                        if (prop.IsField)
                        {
                            var v = ((FieldInfo) prop.Info).GetValue(value);
                            if (v == null) continue;

                            ow._writer.Write(prop.Info.Name);
                            ow.Encode(v);
                        }
                        else
                        {
                            var info = (PropertyInfo) prop.Info;
                            if (info.CanRead && info.CanWrite)
                            {
                                var v = info.GetValue(value, null);
                                if (v == null) continue;

                                ow._writer.Write(prop.Info.Name);
                                ow.Encode(v);
                            }
                        }
                    }

                    var len = (int) ow._stream.Length;
                    _writer.Write((byte) ValueType.Object);
                    _writer.Write(len);
                    _writer.Write(ow._stream.GetBuffer(), 0, len);
                }
            }

            public void Dispose()
            {
                if (_isDisposed) return;

                _writer?.Close();
                _stream?.Dispose();
                _isDisposed = true;
            }
        }
    }
}
