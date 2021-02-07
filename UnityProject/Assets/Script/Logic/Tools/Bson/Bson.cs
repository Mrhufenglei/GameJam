using System;
using System.Collections.Generic;
using System.Reflection;

namespace Foundation
{
    public sealed partial class Bson
    {
        private enum ValueType : byte
        {
            Bool = 0x01,
            SByte,
            Byte,
            Char,
            Short,
            UShort,
            Int,
            UInt,
            Int64,
            UInt64,
            Decimal,
            Float,
            Double,
            DateTime,
            Enum,
            String,
            Array,
            Vector2,
            Vector3,
            Vector4,
            Color,
            Color32,
            Quaternion,
            Bounds,
            Rect,
            Matrix,
            Object,
        };

        private struct PropertyMeta
        {
            public MemberInfo Info { get; set; }

            public bool IsField { get; set; }

            public Type Type { get; set; }
        }

        private struct ArrayMeta
        {
            public Type ItemType { get; set; }

            public bool IsArray { get; set; }

            public bool IsList { get; set; }
        }

        private struct ObjectMeta
        {
            public Type ElemType { get; set; }

            public bool IsDict { get; set; }

            public IDictionary<string, PropertyMeta> Properties { get; set; }
        }

        public static byte[] ToBson(object obj, bool cache = false)
        {
            if (obj == null) return null;

            using (var writer = new Writer())
            {
                writer.Encode(obj);
                return writer.Bytes;
            }
        }

        public static void ToBson(object obj, Writer writer)
        {
            if (obj == null || writer == null) return;

            writer.Encode(obj);
        }

        public static T ToObject<T>(byte[] bson)
        {
            if (bson == null || bson.Length == 0) return default(T);

            if (_bsonObjects.TryGetValue(bson, out var obj) && obj != null) return (T)obj;

            using (var reader = new Reader(bson))
            {
                obj = reader.Decode(typeof(T));
                return (T)obj;
            }
        }

        public static T ToObject<T>(Reader reader)
        {
            if (reader == null) return default(T);

            var obj = reader.Decode(typeof(T));
            return (T)obj;
        }

        public static void Clear()
        {
            _bsonObjects.Clear();
            _propertyMetas.Clear();
            _arrayMetas.Clear();
            _objectMetas.Clear();
        }

        private static readonly Dictionary<byte[], object> _bsonObjects = new Dictionary<byte[], object>(ByteArrayComparer.Default);
        private static readonly IDictionary<Type, IList<PropertyMeta>> _propertyMetas = new Dictionary<Type, IList<PropertyMeta>>();
        private static readonly IDictionary<Type, ArrayMeta> _arrayMetas = new Dictionary<Type, ArrayMeta>();
        private static readonly IDictionary<Type, ObjectMeta> _objectMetas = new Dictionary<Type, ObjectMeta>();

        private static IList<PropertyMeta> AddPropertyMetas(Type type)
        {
            if (_propertyMetas.TryGetValue(type, out var metas) && metas != null)
                return metas;

            metas = new List<PropertyMeta>();

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in properties)
            {
                if (info.Name == "Item") continue;

                var meta = new PropertyMeta { Info = info, IsField = false };
                metas.Add(meta);
            }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in fields)
            {
                var meta = new PropertyMeta { Info = info, IsField = true };
                metas.Add(meta);
            }

            _propertyMetas[type] = metas;
            return metas;
        }

        private static ArrayMeta AddArrayMeta(Type type)
        {
            if (_arrayMetas.TryGetValue(type, out var meta)) return meta;

            meta = new ArrayMeta() { IsArray = type.IsArray };

            if (type.GetInterface("System.Collections.IList") != null)
                meta.IsList = true;

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in properties)
            {
                if (info.Name != "Item") continue;

                var paramers = info.GetIndexParameters();
                if (paramers.Length != 1) continue;

                meta.ItemType = paramers[0].ParameterType == typeof(int) ? info.PropertyType : typeof(object);
            }

            _arrayMetas[type] = meta;
            return meta;
        }

        private static ObjectMeta AddObjectMeta(Type type)
        {
            if (_objectMetas.TryGetValue(type, out var meta)) return meta;

            meta = new ObjectMeta();

            if (type.GetInterface("System.Collections.IDictionary") != null)
                meta.IsDict = true;

            meta.Properties = new Dictionary<string, PropertyMeta>();

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in properties)
            {
                if (info.Name == "Item")
                {
                    var parameters = info.GetIndexParameters();
                    if (parameters.Length != 1) continue;

                    meta.ElemType = parameters[0].ParameterType == typeof(string) ? info.PropertyType : typeof(object);

                    continue;
                }

                var prop = new PropertyMeta { Info = info, Type = info.PropertyType };
                meta.Properties.Add(info.Name, prop);
            }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in fields)
            {
                var data = new PropertyMeta { Info = info, IsField = true, Type = info.FieldType };
                meta.Properties.Add(info.Name, data);
            }

            _objectMetas[type] = meta;
            return meta;
        }

        private sealed class ByteArrayComparer : IEqualityComparer<byte[]>
        {
            public static ByteArrayComparer Default => new ByteArrayComparer();

            public bool Equals(byte[] left, byte[] right)
            {
                if (left == null || right == null) return left == right;

                if (ReferenceEquals(left, right)) return true;

                if (left.Length != right.Length) return false;

                for (var i = 0; i < left.Length; ++i)
                {
                    if (left[i] != right[i]) return false;
                }

                return true;
            }

            public int GetHashCode(byte[] obj)
            {
                if (obj == null)
                    throw new ArgumentNullException("obj");

                var sum = 0;
                var sumOfSum = 0;

                foreach (var val in obj)
                {
                    sum += val; // by default, addition is unchecked. does not throw OverflowException.
                    sumOfSum += sum;
                }

                return sum ^ sumOfSum;
            }
        }
    }
}
