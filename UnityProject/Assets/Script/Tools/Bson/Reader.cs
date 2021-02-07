using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Foundation
{
    public partial class Bson
    {
        public sealed class Reader : IDisposable
        {
            private readonly MemoryStream _stream;
            private readonly BinaryReader _reader;
            private bool _isDisposed;

            public Reader(byte[] bson) : this(new MemoryStream(bson))
            {
            }

            public Reader(MemoryStream stream)
            {
                _stream = stream ?? new MemoryStream();
                _reader = new BinaryReader(_stream);
            }

            public object Decode(Type type)
            {
                var valType = Nullable.GetUnderlyingType(type) ?? type;
                var code = (ValueType) _reader.ReadByte();

                switch (code)
                {
                    case ValueType.Bool:
                        if (valType != typeof(bool)) Debug.LogError($"Bson type is bool, but expect {valType}");
                        return _reader.ReadBoolean();
                    case ValueType.SByte:
                        if (valType != typeof(sbyte)) Debug.LogError($"Bson type is sbyte, but expect {valType}");
                        return _reader.ReadSByte();
                    case ValueType.Byte:
                        if (valType != typeof(byte)) Debug.LogError($"Bson type is byte, but expect {valType}");
                        return _reader.ReadByte();
                    case ValueType.Char:
                        if (valType != typeof(char)) Debug.LogError($"Bson type is char, but expect {valType}");
                        return _reader.ReadChar();
                    case ValueType.Short:
                        if (valType != typeof(short)) Debug.LogError($"Bson type is short, but expect {valType}");
                        return _reader.ReadInt16();
                    case ValueType.UShort:
                        if (valType != typeof(ushort)) Debug.LogError($"Bson type is ushort, but expect {valType}");
                        return _reader.ReadUInt16();
                    case ValueType.Int:
                        if (valType != typeof(int)) Debug.LogError($"Bson type is int, but expect {valType}");
                        return _reader.ReadInt32();
                    case ValueType.UInt:
                        if (valType != typeof(uint)) Debug.LogError($"Bson type is uint, but expect {valType}");
                        return _reader.ReadUInt32();
                    case ValueType.Int64:
                        if (valType != typeof(long)) Debug.LogError($"Bson type is long, but expect {valType}");
                        return _reader.ReadInt64();
                    case ValueType.UInt64:
                        if (valType != typeof(ulong)) Debug.LogError($"Bson type is ulong, but expect {valType}");
                        return _reader.ReadUInt64();
                    case ValueType.Decimal:
                        if (valType != typeof(decimal)) Debug.LogError($"Bson type is decimal, but expect {valType}");
                        return _reader.ReadDecimal();
                    case ValueType.DateTime:
                        if (valType != typeof(DateTime)) Debug.LogError($"Bson type is DateTime, but expect {valType}");
                        return ReadDateTime();
                    case ValueType.Enum:
                        if (!valType.IsEnum) Debug.LogError($"Bson type is Enum, but expect {valType}");
                        return ReadEnum(valType);
                    case ValueType.Float:
                        if (valType != typeof(float)) Debug.LogError($"Bson type is float, but expect {valType}");
                        return _reader.ReadSingle();
                    case ValueType.Double:
                        if (valType != typeof(double)) Debug.LogError($"Bson type is double, but expect {valType}");
                        return _reader.ReadDouble();
                    case ValueType.String:
                        if (valType != typeof(string)) Debug.LogError($"Bson type is string, but expect {valType}");
                        return _reader.ReadString();
                    case ValueType.Array:
                        if (!valType.IsArray && valType.GetInterface("System.Collections.IList") == null)
                            Debug.LogError($"Bson type is Array, but expect {valType}");
                        return ReadArray(valType);
                    case ValueType.Vector2:
                        if (valType != typeof(Vector2)) Debug.LogError($"Bson type is vector2, but expect {valType}");
                        return ReadVector2();
                    case ValueType.Vector3:
                        if (valType != typeof(Vector3)) Debug.LogError($"Bson type is vector3, but expect {valType}");
                        return ReadVector3();
                    case ValueType.Vector4:
                        if (valType != typeof(Vector4)) Debug.LogError($"Bson type is vector4, but expect {valType}");
                        return ReadVector4();
                    case ValueType.Color:
                        if (valType != typeof(Color)) Debug.LogError($"Bson type is color, but expect {valType}");
                        return ReadColor();
                    case ValueType.Color32:
                        if (valType != typeof(Color32)) Debug.LogError($"Bson type is color32, but expect {valType}");
                        return ReadColor32();
                    case ValueType.Quaternion:
                        if (valType != typeof(Quaternion)) Debug.LogError($"Bson type is quaternion, but expect {valType}");
                        return ReadQuaternion();
                    case ValueType.Bounds:
                        if (valType != typeof(Bounds)) Debug.LogError($"Bson type is bounds, but expect {valType}");
                        return ReadBounds();
                    case ValueType.Rect:
                        if (valType != typeof(Rect)) Debug.LogError($"Bson type is rect, but expect {valType}");
                        return ReadRect();
                    case ValueType.Matrix:
                        if (valType != typeof(Matrix4x4)) Debug.LogError($"Bson type is matrix4x4, but expect {valType}");
                        return ReadMatrix();
                    case ValueType.Object:
                        return ReadObject(valType);
                    default:
                        return null;
                }
            }

            private DateTime ReadDateTime()
            {
                var i = _reader.ReadInt64();
                return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(i * 10000); // 100ns
            }

            private object ReadEnum(Type type)
            {
                return Enum.ToObject(type, _reader.ReadInt32());
            }

            private object ReadArray(Type type)
            {
                var metadata = AddArrayMeta(type);

                object array;
                IList list;
                Type itemType;

                if (metadata.IsArray)
                {
                    list = new ArrayList();
                    itemType = type.GetElementType();
                }
                else
                {
                    list = (IList) Activator.CreateInstance(type);
                    itemType = metadata.ItemType;
                }

                var end = _reader.ReadInt32() + _reader.BaseStream.Position;
                while (_reader.BaseStream.Position < end)
                {
                    var item = Decode(itemType);
                    list.Add(item);
                }

                if (metadata.IsArray)
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    array = Array.CreateInstance(itemType, list.Count);

                    for (var i = 0; i < list.Count; i++)
                    {
                        ((Array) array).SetValue(list[i], i);
                    }
                }
                else
                {
                    array = list;
                }

                return array;
            }

            private Vector2 ReadVector2()
            {
                return new Vector2
                {
                    x = _reader.ReadSingle(),
                    y = _reader.ReadSingle(),
                };
            }

            private Vector3 ReadVector3()
            {
                return new Vector3
                {
                    x = _reader.ReadSingle(),
                    y = _reader.ReadSingle(),
                    z = _reader.ReadSingle()
                };
            }

            private Vector4 ReadVector4()
            {
                return new Vector4
                {
                    x = _reader.ReadSingle(),
                    y = _reader.ReadSingle(),
                    z = _reader.ReadSingle(),
                    w = _reader.ReadSingle()
                };
            }

            private Color ReadColor()
            {
                return new Color
                {
                    r = _reader.ReadSingle(),
                    g = _reader.ReadSingle(),
                    b = _reader.ReadSingle(),
                    a = _reader.ReadSingle()
                };
            }

            private Color32 ReadColor32()
            {
                return new Color32
                {
                    r = _reader.ReadByte(),
                    g = _reader.ReadByte(),
                    b = _reader.ReadByte(),
                    a = _reader.ReadByte()
                };
            }

            private Quaternion ReadQuaternion()
            {
                return new Quaternion
                {
                    x = _reader.ReadSingle(),
                    y = _reader.ReadSingle(),
                    z = _reader.ReadSingle(),
                    w = _reader.ReadSingle()
                };
            }

            private Bounds ReadBounds()
            {
                return new Bounds
                {
                    center = new Vector3
                    {
                        x = _reader.ReadSingle(),
                        y = _reader.ReadSingle(),
                        z = _reader.ReadSingle()
                    },
                    extents = new Vector3
                    {
                        x = _reader.ReadSingle(),
                        y = _reader.ReadSingle(),
                        z = _reader.ReadSingle()
                    }
                };
            }

            private Rect ReadRect()
            {
                return new Rect
                {
                    x = _reader.ReadSingle(),
                    y = _reader.ReadSingle(),
                    width = _reader.ReadSingle(),
                    height = _reader.ReadSingle()
                };
            }

            private Matrix4x4 ReadMatrix()
            {
                return new Matrix4x4
                {
                    m00 = _reader.ReadSingle(),
                    m10 = _reader.ReadSingle(),
                    m20 = _reader.ReadSingle(),
                    m30 = _reader.ReadSingle(),
                    m01 = _reader.ReadSingle(),
                    m11 = _reader.ReadSingle(),
                    m21 = _reader.ReadSingle(),
                    m31 = _reader.ReadSingle(),
                    m02 = _reader.ReadSingle(),
                    m12 = _reader.ReadSingle(),
                    m22 = _reader.ReadSingle(),
                    m32 = _reader.ReadSingle(),
                    m03 = _reader.ReadSingle(),
                    m13 = _reader.ReadSingle(),
                    m23 = _reader.ReadSingle(),
                    m33 = _reader.ReadSingle()
                };
            }

            private object ReadObject(Type type)
            {
                var metadata = AddObjectMeta(type);
                var obj = Activator.CreateInstance(type);

                var end = _reader.ReadInt32() + _reader.BaseStream.Position;
                while (_reader.BaseStream.Position < end)
                {
                    var propName = _reader.ReadString();

                    if (metadata.Properties.TryGetValue(propName, out var prop))
                    {
                        var val = Decode(prop.Type);

                        if (prop.IsField)
                        {
                            ((FieldInfo) prop.Info).SetValue(obj, val);
                        }
                        else
                        {
                            var info = (PropertyInfo) prop.Info;
                            if (info.CanWrite) info.SetValue(obj, val, null);
                        }
                    }
                    else
                    {
                        var val = Decode(metadata.ElemType);

                        if (metadata.IsDict)
                            ((IDictionary) obj).Add(propName, val);
                        else
                            Debug.LogError($"The type {type} doesn't have the property '{propName}'");
                    }
                }

                return obj;
            }

            public void Dispose()
            {
                if (_isDisposed) return;

                _reader?.Close();
                _stream?.Dispose();
                _isDisposed = true;
            }
        }
    }
}
