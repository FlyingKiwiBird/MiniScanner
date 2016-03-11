//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MapFromAttribute.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC.Attributes
{
    using System;

    /// <summary>
    /// Tells the mappers to use a different name when mapping this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MapFromAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapFromAttribute"/> class.
        /// </summary>
        public MapFromAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the Name to map as.
        /// </summary>
        public string Name { get; set; }
    }
}
