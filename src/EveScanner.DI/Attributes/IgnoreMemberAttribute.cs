﻿//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IgnoreMemberAttribute.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC.Attributes
{
    using System;

    /// <summary>
    /// Tells the Mappers to ignore the Property or Field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IgnoreMemberAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreMemberAttribute"/> class.
        /// </summary>
        public IgnoreMemberAttribute()
        {
        }
    }
}
