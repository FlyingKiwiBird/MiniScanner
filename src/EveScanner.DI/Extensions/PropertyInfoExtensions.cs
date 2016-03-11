//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="PropertyInfoExtensions.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Internal class for mapping a Property to a compiled method to speed up code execution.
    /// Class retrieved from http://weblogs.asp.net/marianor/using-expression-trees-to-get-property-getter-and-setters on 2016-02-19.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the value getter from a particular property info.
        /// </summary>
        /// <typeparam name="TTargetType">Type that the property belongs to</typeparam>
        /// <param name="propertyInfo">Property Info to extract from</param>
        /// <returns>Function to Get Value</returns>
        internal static Func<TTargetType, object> GetValueGetter<TTargetType>(this PropertyInfo propertyInfo)
        {
            if (typeof(TTargetType) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException("T must be of the declaring type of the Property", "propertyInfo");
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));
            return (Func<TTargetType, object>)Expression.Lambda(convert, instance).Compile();
        }

        /// <summary>
        /// Gets the value setter from a particular property info.
        /// </summary>
        /// <typeparam name="TTargetType">Type that the property belongs to</typeparam>
        /// <param name="propertyInfo">Property Info to extract from</param>
        /// <returns>Action to Set Value</returns>
        internal static Action<TTargetType, object> GetValueSetter<TTargetType>(this PropertyInfo propertyInfo)
        {
            if (typeof(TTargetType) != propertyInfo.DeclaringType)
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
            return (Action<TTargetType, object>)Expression.Lambda(setterCall, instance, argument)
                                                .Compile();
        }
    }
}
