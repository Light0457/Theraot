﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Theraot.Collections.ThreadSafe;

namespace Theraot.Reflection
{
    public static partial class TypeExtraExtensions
    {
        private static readonly CacheDict<Type, bool> _binaryPortableCache = new CacheDict<Type, bool>(256);
        private static readonly CacheDict<Type, bool> _blittableCache = new CacheDict<Type, bool>(256);
        private static readonly CacheDict<Type, bool> _valueTypeRecursiveCache = new CacheDict<Type, bool>(256);

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool CanBeNull(this Type type)
        {
            var info = type.GetTypeInfo();
            return !info.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool DelegateEquals(this Delegate @delegate, MethodInfo method, object target)
        {
            return @delegate.GetMethodInfo().Equals(method) && @delegate.Target == target;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static TAttribute[] GetAttributes<TAttribute>(this Assembly item)
            where TAttribute : Attribute
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
#if TARGETS_NET
            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute), true);
#else
            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute));
#endif
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static TAttribute[] GetAttributes<TAttribute>(this MemberInfo item, bool inherit)
            where TAttribute : Attribute
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute), inherit);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static TAttribute[] GetAttributes<TAttribute>(this Module item)
            where TAttribute : Attribute
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
#if TARGETS_NET
            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute), true);
#else
            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute));
#endif
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static TAttribute[] GetAttributes<TAttribute>(this ParameterInfo item, bool inherit)
            where TAttribute : Attribute
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return (TAttribute[])item.GetCustomAttributes(typeof(TAttribute), inherit);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static TAttribute[] GetAttributes<TAttribute>(this Type type, bool inherit)
            where TAttribute : Attribute
        {
            var info = type.GetTypeInfo();
            return (TAttribute[])info.GetCustomAttributes(typeof(TAttribute), inherit);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetNonNullable(this Type type)
        {
            return type.IsNullable() ? Nullable.GetUnderlyingType(type) : type;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetNonRefType(this ParameterInfo parameterInfo)
        {
            if (parameterInfo == null)
            {
                throw new ArgumentNullException(nameof(parameterInfo));
            }

            var parameterType = parameterInfo.ParameterType;
            if (parameterType.IsByRef)
            {
                parameterType = parameterType.GetElementType();
            }

            return parameterType;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetNonRefType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetNonRefTypeInternal();
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetNotNullable(this Type type)
        {
            var underlying = Nullable.GetUnderlyingType(type);
            return underlying ?? type;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetNullable(this Type type)
        {
            var info = type.GetTypeInfo();
            if (info.IsValueType && !IsNullable(type))
            {
                return typeof(Nullable<>).MakeGenericType(type);
            }

            return type;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetReturnType(this MethodBase methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            return methodInfo.IsConstructor ? methodInfo.DeclaringType : ((MethodInfo)methodInfo).ReturnType;
        }

        public static MethodInfo GetStaticMethod(this Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return type.GetStaticMethodInternal(name);
        }

        public static MethodInfo[] GetStaticMethods(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetStaticMethodsInternal();
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this Assembly item)
            where TAttribute : Attribute
        {
            var attributes = item.GetAttributes<TAttribute>();
            if (attributes != null)
            {
                return attributes.Length > 0;
            }

            return false;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this MemberInfo item)
            where TAttribute : Attribute
        {
            var attributes = item.GetAttributes<TAttribute>(true);
            if (attributes != null)
            {
                return attributes.Length > 0;
            }

            return false;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this Module item)
            where TAttribute : Attribute
        {
            var attributes = item.GetAttributes<TAttribute>();
            if (attributes != null)
            {
                return attributes.Length > 0;
            }

            return false;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this ParameterInfo item)
            where TAttribute : Attribute
        {
            var attributes = item.GetAttributes<TAttribute>(true);
            if (attributes != null)
            {
                return attributes.Length > 0;
            }

            return false;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this Type item)
            where TAttribute : Attribute
        {
            var attributes = item.GetAttributes<TAttribute>(true);
            if (attributes != null)
            {
                return attributes.Length > 0;
            }

            return false;
        }

        public static bool HasIdentityPrimitiveOrNullableConversionTo(this Type source, Type target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return source.HasIdentityPrimitiveOrNullableConversionToInternal(target);
        }

        public static bool HasIdentityPrimitiveOrNullableConversionToInternal(this Type source, Type target)
        {
            // Identity conversion
            if (source == target)
            {
                return true;
            }

            // Nullable conversions
            if (source.IsNullable() && target == source.GetNonNullable())
            {
                return true;
            }

            if (IsNullable(target) && source == target.GetNonNullable())
            {
                return true;
            }

            // Primitive runtime conversions
            // All conversions amongst enum, bool, char, integer and float types
            // (and their corresponding nullable types) are legal except for
            // nonbool==>bool and nonbool==>bool? which are only legal from
            // bool-backed enums.
            return IsConvertible(source) && IsConvertible(target)
                                         && (target.GetNonNullable() != typeof(bool)
                                             || (source.GetTypeInfo().IsEnum && source.GetUnderlyingSystemType() == typeof(bool)));
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsArithmetic(this Type type)
        {
            type = GetNonNullable(type);
            return type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(double)
                   || type == typeof(float)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        public static bool IsBinaryPortable(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsBinaryPortableExtracted(type);
        }

        public static bool IsBlittable(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsBlittableExtracted(type);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsBool(this Type type)
        {
            return GetNonNullable(type) == typeof(bool);
        }

        public static bool IsByRefParameter(this ParameterInfo parameterInfo)
        {
            if (parameterInfo == null)
            {
                throw new ArgumentNullException(nameof(parameterInfo));
            }

            return parameterInfo.IsByRefParameterInternal();
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsConstructedGenericType(this Type type)
        {
            var info = type.GetTypeInfo();
            return info.IsGenericType && !info.IsGenericTypeDefinition;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsConvertible(this Type type)
        {
            type = type.GetNonNullable();
            var info = type.GetTypeInfo();
            if (info.IsEnum)
            {
                return true;
            }

            return type == typeof(bool)
                   || type == typeof(byte)
                   || type == typeof(sbyte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong)
                   || type == typeof(float)
                   || type == typeof(double)
                   || type == typeof(char);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsGenericInstanceOf(this Type type, Type genericTypeDefinition)
        {
            var info = type.GetTypeInfo();
            if (!info.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsInteger(this Type type)
        {
            type = GetNonNullable(type);
            return type.IsPrimitiveInteger();
        }

        public static bool IsInteger64(this Type type)
        {
            type = GetNonNullable(type);
            return !type.IsSameOrSubclassOfInternal(typeof(Enum)) && (type == typeof(long) || type == typeof(ulong));
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsIntegerOrBool(this Type type)
        {
            type = GetNonNullable(type);
            return type == typeof(bool)
                   || type == typeof(sbyte)
                   || type == typeof(byte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsNumeric(this Type type)
        {
            type = GetNonNullable(type);
            return type == typeof(char)
                   || type == typeof(sbyte)
                   || type == typeof(byte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(double)
                   || type == typeof(float)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsNumericOrBool(this Type type)
        {
            return IsNumeric(type) || IsBool(type);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsPrimitiveInteger(this Type type)
        {
            return type == typeof(sbyte)
                   || type == typeof(byte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static bool IsSafeArray(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
#if GREATERTHAN_NETCOREAPP11
            return type.IsSZArray;
#else
            return type.IsArray && type.GetElementType()?.MakeArrayType() == type;
#endif
        }

        public static bool IsSameOrSubclassOf(this Type type, Type baseType)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            return type.IsSameOrSubclassOfInternal(baseType);
        }

        public static bool IsValueTypeRecursive(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsValueTypeRecursiveExtracted(type);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type MakeNullable(this Type self)
        {
            return typeof(Nullable<>).MakeGenericType(self);
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static void SetValue(this PropertyInfo info, object obj, object value)
        {
            //Added in .NET 4.5
#if NET45
            info.SetValue(obj, value);
#else
            info.SetValue(obj, value, null);
#endif
        }

        internal static Type GetNonRefTypeInternal(this Type type)
        {
            return type.IsByRef ? type.GetElementType() : type;
        }

        internal static MethodInfo GetStaticMethodInternal(this Type type, string name)
        {
            // Don't use BindingFlags.Static
            return Array.Find(type.GetMethods(), method => string.Equals(method.Name, name, StringComparison.Ordinal) && method.IsStatic);
        }

        internal static MethodInfo[] GetStaticMethodsInternal(this Type type)
        {
            var methods = type.GetMethods();
            var list = new List<MethodInfo>(methods.Length);
            list.AddRange(methods.Where(method => method.IsStatic));
            return list.ToArray();
        }

        internal static bool IsByRefParameterInternal(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.ParameterType.IsByRef)
            {
                return true;
            }

            return (parameterInfo.Attributes & ParameterAttributes.Out) == ParameterAttributes.Out;
        }

        internal static bool IsFloatingPoint(this Type type)
        {
            type = GetNonNullable(type);
            return type == typeof(float)
                   || type == typeof(double);
        }

        internal static bool IsSameOrSubclassOfInternal(this Type type, Type baseType)
        {
            return type == baseType || type.GetTypeInfo().IsSubclassOf(baseType);
        }

        internal static bool IsUnsigned(this Type type)
        {
            // Including byte and char
            type = GetNonNullable(type);
            return type == typeof(byte)
                   || type == typeof(char)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        internal static bool IsUnsignedInteger(this Type type)
        {
            // Not including byte or char, by design - use IsUnsigned instead
            type = GetNonNullable(type);
            return type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong);
        }

        private static bool GetBinaryPortableResult(Type type)
        {
            var info = type.GetTypeInfo();
            if (info.IsPrimitive)
            {
                return type != typeof(IntPtr)
                       && type != typeof(UIntPtr)
                       && type != typeof(char)
                       && type != typeof(bool);
            }

            if (!info.IsValueType)
            {
                return false;
            }

            if (type.GetFields().Any(field => !IsBinaryPortableExtracted(field.FieldType)))
            {
                return false;
            }

            return !info.IsAutoLayout && type.GetStructLayoutAttribute().Pack > 0;
        }

        private static bool GetBlittableResult(Type type)
        {
            var info = type.GetTypeInfo();
            if (info.IsPrimitive)
            {
                return type != typeof(char)
                       && type != typeof(bool);
            }

            return info.IsValueType && type.GetFields().All(field => IsBlittableExtracted(field.FieldType));
        }

        private static bool GetValueTypeRecursiveResult(Type type)
        {
            var info = type.GetTypeInfo();
            if (info.IsPrimitive)
            {
                return true;
            }

            return info.IsValueType && type.GetFields().All(field => IsValueTypeRecursiveExtracted(field.FieldType));
        }

        private static bool IsBinaryPortableExtracted(Type type)
        {
            var info = type.GetTypeInfo();
            if (!info.IsValueType)
            {
                return false;
            }

            if (_binaryPortableCache.TryGetValue(type, out var result))
            {
                return result;
            }

            result = GetBinaryPortableResult(type);
            _binaryPortableCache[type] = result;
            return result;
        }

        private static bool IsBlittableExtracted(Type type)
        {
            var info = type.GetTypeInfo();
            if (!info.IsValueType)
            {
                return false;
            }

            if (_blittableCache.TryGetValue(type, out var result))
            {
                return result;
            }

            result = GetBlittableResult(type);
            _blittableCache[type] = result;
            return result;
        }

        private static bool IsValueTypeRecursiveExtracted(Type type)
        {
            var info = type.GetTypeInfo();
            if (!info.IsValueType)
            {
                return false;
            }

            if (_valueTypeRecursiveCache.TryGetValue(type, out var result))
            {
                return result;
            }

            result = GetValueTypeRecursiveResult(type);
            _valueTypeRecursiveCache[type] = result;
            return result;
        }
    }

    public static partial class TypeExtraExtensions
    {
        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static StructLayoutAttribute GetStructLayoutAttribute(this Type type)
        {
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4
            var attributes = type.GetAttributes<StructLayoutAttribute>(false);
            return attributes.FirstOrDefault();

#else
            return type.StructLayoutAttribute;
#endif
        }

        [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
        public static Type GetUnderlyingSystemType(this Type type)
        {
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4
            return type;
#else
            return type.UnderlyingSystemType;
#endif
        }
    }
}