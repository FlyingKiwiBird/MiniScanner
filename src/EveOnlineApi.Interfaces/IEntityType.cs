//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IEntityType.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Represents an Eve Online Entity Type (which is just an integer) with some custom equality logic.
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// Gets the Type Id
        /// </summary>
        int TypeId { get; }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current <see cref="EntityType"/>.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current <see cref="EntityType"/>.</param>
        /// <returns>true if the specified System.Object is equal to the current <see cref="EntityType"/>; otherwise, false.</returns>
        bool Equals(object obj);
    }
}