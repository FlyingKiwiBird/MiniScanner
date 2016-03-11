//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryTypeProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.Providers
{
    using System.Collections.Generic;

    using EveScanner.Interfaces.SDE;

    /// <summary>
    /// Interface for the Eve SDE Inventory Types
    /// </summary>
    public interface IInventoryTypeProvider
    {
        /// <summary>
        /// Returns an Inventory Type object for the specified Group Id
        /// </summary>
        /// <param name="typeId">Type Id</param>
        /// <returns>Inventory Type Object</returns>
        IInventoryType GetInventoryTypeByTypeId(int typeId);

        /// <summary>
        /// Returns all the Inventory Types for a specific Group
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Inventory Type Objects</returns>
        IEnumerable<IInventoryType> GetInventoryTypesByGroupId(int groupId);

        /// <summary>
        /// Returns an Inventory Type object for the specified Type Name
        /// </summary>
        /// <param name="typeName">Type Name</param>
        /// <returns>Inventory Type Object</returns>
        IInventoryType GetInventoryTypeByTypeName(string typeName);
    }
}
