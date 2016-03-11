//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryGroup.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.SDE
{
    /// <summary>
    /// Interface for Read Only Inventory Group
    /// </summary>
    public interface IInventoryGroup
    {
        /// <summary>
        /// Gets the Group Id
        /// </summary>
        int GroupId { get; }

        /// <summary>
        /// Gets the Category Id
        /// </summary>
        int? CategoryId { get; }

        /// <summary>
        /// Gets the Group Name
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// Gets the Icon Id
        /// </summary>
        int? IconId { get; }

        /// <summary>
        /// Gets a value indicating whether the Base Price should be used.
        /// </summary>
        bool? UseBasePrice { get; }

        /// <summary>
        /// Gets a value indicating whether the group is anchored.
        /// </summary>
        bool? Anchored { get; }

        /// <summary>
        /// Gets a value indicating whether the group is anchorable.
        /// </summary>
        bool? Anchorable { get; }

        /// <summary>
        /// Gets a value indicating whether the group is fittable.
        /// </summary>
        bool? FittableNonSingleton { get; }

        /// <summary>
        /// Gets a value indicating whether the group is published.
        /// </summary>
        bool? Published { get; }
    }
}
