using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EveScanner.IoC.Extensions
{
    /// <summary>
    /// Class retrieved from http://weblogs.asp.net/marianor/using-expression-trees-to-get-property-getter-and-setters on 2016-02-19.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        internal static Func<T, object> GetValueGetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException("T must be of the declaring type of the Property", "propertyInfo");
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, instance).Compile();
        }

        internal static Action<T, object> GetValueSetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException("T must be of the declaring type of the Property", "propertyInfo");
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");

            var argument = Expression.Parameter(typeof(object), "a");
            var conversion = Expression.ConvertChecked(argument, propertyInfo.PropertyType);

            var setterCall = Expression.Call(
                instance,
                propertyInfo.GetSetMethod(true),
                conversion
            );
            return (Action<T, object>)Expression.Lambda(setterCall, instance, argument)
                                                .Compile();
        }
    }
}
