//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryRepackaged.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.EDE
{
    /// <summary>
    /// Interface to Repackaged Inventory Values
    /// </summary>
    public interface IInventoryRepackaged
    {
        /// <summary>
        /// Gets the Group Id
        /// </summary>
        int GroupId { get; }

        /// <summary>
        /// Gets the Repackaged Volume for the Group Id
        /// </summary>
        double Volume { get; }
    }
}
