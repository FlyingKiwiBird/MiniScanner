//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryType.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.SDE
{
    /// <summary>
    /// Interface for Read Only Inventory Type
    /// </summary>
    public interface IInventoryType
    {
        /// <summary>
        /// Gets the Type Id
        /// </summary>
        int TypeId { get; }

        /// <summary>
        /// Gets the Group Id
        /// </summary>
        int? GroupId { get; }

        /// <summary>
        /// Gets the Type Name
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets the Item Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the Mass of the Item
        /// </summary>
        double? Mass { get; }

        /// <summary>
        /// Gets the Assembled Volume of the Item
        /// </summary>
        double? Volume { get; }

        /// <summary>
        /// Gets the Cargo Capacity of the Item
        /// </summary>
        double? Capacity { get; }

        /// <summary>
        /// Gets the Portion Size of the Item
        /// </summary>
        int? PortionSize { get; }

        /// <summary>
        /// Gets the Associated Racial Id of the Item
        /// </summary>
        int? RaceId { get; }

        /// <summary>
        /// Gets the Base Price of the Item
        /// </summary>
        decimal? BasePrice { get; }

        /// <summary>
        /// Gets a value indicating whether the type is published.
        /// </summary>
        bool? Published { get; }

        /// <summary>
        /// Gets the Market Group Id
        /// </summary>
        int? MarketGroupId { get; }

        /// <summary>
        /// Gets the Icon Id.
        /// </summary>
        int? IconId { get; }

        /// <summary>
        /// Gets the Sound Id
        /// </summary>
        int? SoundId { get; }

        /// <summary>
        /// Gets the Graphic Id
        /// </summary>
        int? GraphicId { get; }
    }
}
