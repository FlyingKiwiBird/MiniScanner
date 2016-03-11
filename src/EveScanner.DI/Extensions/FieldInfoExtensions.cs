//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="FieldInfoExtensions.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Internal class for mapping a Field to a compiled method to speed up code execution.
    /// </summary>
    internal static class FieldInfoExtensions
    {
        /// <summary>
        /// Gets the value getter from a particular field info.
        /// </summary>
        /// <typeparam name="TTargetType">Type that the field belongs to</typeparam>
        /// <param name="fieldInfo">Field Info to extract from</param>
        /// <returns>Function to Get Value</returns>
        internal static Func<TTargetType, object> GetValueGetter<TTargetType>(this FieldInfo fieldInfo)
        {
            if (typeof(TTargetType) != fieldInfo.DeclaringType)
            {
                throw new ArgumentException("T must be of the declaring type of the field", "fieldInfo");
            }

            var target = Expression.Parameter(fieldInfo.DeclaringType, "target");
            var field = Expression.Field(target, fieldInfo);
            var convert = Expression.TypeAs(field, typeof(object));
            return (Func<TTargetType, object>)Expression.Lambda(convert, target).Compile();
        }

        /// <summary>
        /// Gets the value setter from a particular field info.
        /// </summary>
        /// <typeparam name="TTargetType">Type that the field belongs to</typeparam>
        /// <param name="fieldInfo">Field Info to extract from</param>
        /// <returns>Action to Set Value</returns>
        internal static Action<TTargetType, object> GetValueSetter<TTargetType>(this FieldInfo fieldInfo)
        {
            if (typeof(TTargetType) != fieldInfo.DeclaringType)
            {
                throw new ArgumentException("T must be of the declaring type of the field", "fieldInfo");
            }

            var target = Expression.Parameter(fieldInfo.DeclaringType, "target");
            var value = Expression.Parameter(typeof(object), "value");

            var field = Expression.Field(target, fieldInfo);
            var convert = Expression.Convert(value, fieldInfo.FieldType);
            var assign = Expression.Assign(field, convert);

            return Expression.Lambda<Action<TTargetType, object>>(assign, target, value).Compile();
        }
    }
}
