//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryRepackagedProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.Providers
{
    using EveScanner.Interfaces.EDE;

    /// <summary>
    /// Interface for Repackaged Volume Data
    /// </summary>
    public interface IInventoryRepackagedProvider
    {
        /// <summary>
        /// Returns the Repackaged Information for a specified Group Id
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Repackaged Volume Object</returns>
        IInventoryRepackaged GetRepackagedVolumesForGroup(int groupId);
    }
}
