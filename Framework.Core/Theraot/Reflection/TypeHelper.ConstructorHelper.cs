﻿// Needed for NET40

using System;
using System.Globalization;
using System.Reflection;
using Theraot.Collections.ThreadSafe;
using Theraot.Core;

namespace Theraot.Reflection
{
    public static partial class TypeHelper
    {
        public static TReturn Create<TReturn>()
        {
            if (ConstructorHelper<TReturn>.HasConstructor)
            {
                return ConstructorHelper<TReturn>.InvokeConstructor();
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with no type arguments.", typeof(TReturn)));
            }
        }

        public static TReturn CreateOrDefault<TReturn>()
        {
            return ConstructorHelper<TReturn>.CreateOrDefault();
        }

        public static TReturn CreateOrFail<TReturn>()
        {
            return ConstructorHelper<TReturn>.CreateOrFail();
        }

        public static Func<TReturn> GetCreate<TReturn>()
        {
            if (ConstructorHelper<TReturn>.HasConstructor)
            {
                return ConstructorHelper<TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with no type arguments.", typeof(TReturn)));
            }
        }

        public static bool TryGetCreate<TReturn>(out Func<TReturn> create)
        {
            if (ConstructorHelper<TReturn>.HasConstructor)
            {
                create = ConstructorHelper<TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<TReturn> GetCreateOrDefault<TReturn>()
        {
            return ConstructorHelper<TReturn>.CreateOrDefault;
        }

        public static Func<TReturn> GetCreateOrFail<TReturn>()
        {
            return ConstructorHelper<TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<TReturn>()
        {
            return ConstructorHelper<TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<TReturn> _create;
            private static readonly Func<TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = Type.EmptyTypes;
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<TReturn> Create => _create;

            public static Func<TReturn> CreateOrDefault => _createOrDefault;

            public static Func<TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor()
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with no type arguments.", typeof(TReturn)));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(ArrayReservoir<object>.EmptyArray);
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T, TReturn>(T obj)
        {
            if (ConstructorHelper<T, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T, TReturn>.InvokeConstructor(obj);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type argument {1}", typeof(TReturn), typeof(T).Name));
            }
        }

        public static TReturn CreateOrDefault<T, TReturn>(T obj)
        {
            return ConstructorHelper<T, TReturn>.CreateOrDefault(obj);
        }

        public static TReturn CreateOrFail<T, TReturn>(T obj)
        {
            return ConstructorHelper<T, TReturn>.CreateOrFail(obj);
        }

        public static Func<T, TReturn> GetCreate<T, TReturn>()
        {
            if (ConstructorHelper<T, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type argument {1}", typeof(TReturn), typeof(T).Name));
            }
        }

        public static bool TryGetCreate<T, TReturn>(out Func<T, TReturn> create)
        {
            if (ConstructorHelper<T, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T, TReturn> GetCreateOrDefault<T, TReturn>()
        {
            return ConstructorHelper<T, TReturn>.CreateOrDefault;
        }

        public static Func<T, TReturn> GetCreateOrFail<T, TReturn>()
        {
            return ConstructorHelper<T, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T, TReturn>()
        {
            return ConstructorHelper<T, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T, TReturn> _create;
            private static readonly Func<T, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T, TReturn> Create => _create;

            public static Func<T, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T obj)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type argument {1}", typeof(TReturn), typeof(T).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { obj });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, TReturn>(T1 arg1, T2 arg2)
        {
            if (ConstructorHelper<T1, T2, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, TReturn>.InvokeConstructor(arg1, arg2);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, TReturn>(T1 arg1, T2 arg2)
        {
            return ConstructorHelper<T1, T2, TReturn>.CreateOrDefault(arg1, arg2);
        }

        public static TReturn CreateOrFail<T1, T2, TReturn>(T1 arg1, T2 arg2)
        {
            return ConstructorHelper<T1, T2, TReturn>.CreateOrFail(arg1, arg2);
        }

        public static Func<T1, T2, TReturn> GetCreate<T1, T2, TReturn>()
        {
            if (ConstructorHelper<T1, T2, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, TReturn>(out Func<T1, T2, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, TReturn> GetCreateOrDefault<T1, T2, TReturn>()
        {
            return ConstructorHelper<T1, T2, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, TReturn> GetCreateOrFail<T1, T2, TReturn>()
        {
            return ConstructorHelper<T1, T2, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, TReturn>()
        {
            return ConstructorHelper<T1, T2, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, TReturn> _create;
            private static readonly Func<T1, T2, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, TReturn> Create => _create;

            public static Func<T1, T2, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, TReturn>(T1 arg1, T2 arg2, T3 arg3)
        {
            if (ConstructorHelper<T1, T2, T3, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, TReturn>.InvokeConstructor(arg1, arg2, arg3);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, TReturn>(T1 arg1, T2 arg2, T3 arg3)
        {
            return ConstructorHelper<T1, T2, T3, TReturn>.CreateOrDefault(arg1, arg2, arg3);
        }

        public static TReturn CreateOrFail<T1, T2, T3, TReturn>(T1 arg1, T2 arg2, T3 arg3)
        {
            return ConstructorHelper<T1, T2, T3, TReturn>.CreateOrFail(arg1, arg2, arg3);
        }

        public static Func<T1, T2, T3, TReturn> GetCreate<T1, T2, T3, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, TReturn>(out Func<T1, T2, T3, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, TReturn> GetCreateOrDefault<T1, T2, T3, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, TReturn> GetCreateOrFail<T1, T2, T3, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, TReturn> _create;
            private static readonly Func<T1, T2, T3, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, TReturn> Create => _create;

            public static Func<T1, T2, T3, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (ConstructorHelper<T1, T2, T3, T4, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return ConstructorHelper<T1, T2, T3, T4, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return ConstructorHelper<T1, T2, T3, T4, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4);
        }

        public static Func<T1, T2, T3, T4, TReturn> GetCreate<T1, T2, T3, T4, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, TReturn>(out Func<T1, T2, T3, T4, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, TReturn> GetCreateOrDefault<T1, T2, T3, T4, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, TReturn> GetCreateOrFail<T1, T2, T3, T4, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<T1, T2, T3, T4, T5, TReturn> GetCreate<T1, T2, T3, T4, T5, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, TReturn>(out Func<T1, T2, T3, T4, T5, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T1, T2, T3, T4, T5, T6, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, TReturn>(out Func<T1, T2, T3, T4, T5, T6, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }

        public static TReturn Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.InvokeConstructor(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name, typeof(T16).Name));
            }
        }

        public static TReturn CreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.CreateOrDefault(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }

        public static TReturn CreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.CreateOrFail(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> GetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>()
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.HasConstructor)
            {
                return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.Create;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name, typeof(T16).Name));
            }
        }

        public static bool TryGetCreate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> create)
        {
            if (ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.HasConstructor)
            {
                create = ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.Create;
                return true;
            }
            else
            {
                create = null;
                return false;
            }
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> GetCreateOrDefault<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.CreateOrDefault;
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> GetCreateOrFail<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.CreateOrFail;
        }

        public static bool HasConstructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>()
        {
            return ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>.HasConstructor;
        }

        private static class ConstructorHelper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>
        {
            private static readonly ConstructorInfo _constructorInfo;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> _create;
            private static readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> _createOrDefault;

            static ConstructorHelper()
            {
                var typeArguments = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16) };
                _constructorInfo = typeof(TReturn).GetConstructor(typeArguments);
                _create = InvokeConstructor;
                if (HasConstructor)
                {
                    _createOrDefault = _create;
                    CreateOrFail = _create;
                }
                else
                {
                    _createOrDefault = FuncHelper.GetDefaultFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>();
                    var info = typeof(TReturn).GetTypeInfo();
                    CreateOrFail = info.IsValueType ? _createOrDefault : FuncHelper.GetThrowFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(CreateMissingMemberException());
                }
            }

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> Create => _create;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> CreateOrDefault => _createOrDefault;

            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> CreateOrFail { get; }

            public static bool HasConstructor => _constructorInfo != null;

            public static TReturn InvokeConstructor(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            {
                if (_constructorInfo == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no constructor for {0} with the type arguments {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}", typeof(TReturn), typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, typeof(T9).Name, typeof(T10).Name, typeof(T11).Name, typeof(T12).Name, typeof(T13).Name, typeof(T14).Name, typeof(T15).Name, typeof(T16).Name));
                }
                else
                {
                    return (TReturn)_constructorInfo.Invoke(new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 });
                }
            }

            private static MissingMemberException CreateMissingMemberException()
            {
                return new MissingMemberException();
            }
        }
    }
}