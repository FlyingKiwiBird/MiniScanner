using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EveScanner.IoC.Extensions
{
    internal static class FieldInfoExtensions
    {
        internal static Func<T, object> GetValueGetter<T>(this FieldInfo fieldInfo)
        {
            if (typeof(T) != fieldInfo.DeclaringType)
            {
                throw new ArgumentException();
            }

            var target = Expression.Parameter(fieldInfo.DeclaringType, "target");
            var field = Expression.Field(target, fieldInfo);
            var convert = Expression.TypeAs(field, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, target).Compile();
        }

        internal static Action<T, object> GetValueSetter<T>(this FieldInfo fieldInfo)
        {
            if (typeof(T) != fieldInfo.DeclaringType)
            {
                throw new ArgumentException();
            }

            var target = Expression.Parameter(fieldInfo.DeclaringType, "target");
            var value = Expression.Parameter(typeof(object), "value");

            var field = Expression.Field(target, fieldInfo);
            var convert = Expression.Convert(value, fieldInfo.FieldType);
            var assign = Expression.Assign(field, convert);

            return Expression.Lambda<Action<T, object>>(assign, target, value).Compile();
        }
    }
}
