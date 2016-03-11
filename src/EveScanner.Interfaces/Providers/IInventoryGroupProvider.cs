//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IInventoryGroupProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.Providers
{
    using System.Collections.Generic;
    using EveScanner.Interfaces.SDE;

    /// <summary>
    /// Interface for the Eve SDE Inventory Groups
    /// </summary>
    public interface IInventoryGroupProvider
    {
        /// <summary>
        /// Returns an Inventory Group object for the specified Group Id
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Inventory Group Object</returns>
        IInventoryGroup GetInventoryGroupById(int groupId);

        /// <summary>
        /// Returns all the Inventory Groups for a specific Category
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns>Inventory Group Objects</returns>
        IEnumerable<IInventoryGroup> GetInventoryGroupsByCategoryId(int categoryId);

        /// <summary>
        /// Returns an Inventory Group object for the specified Group Name
        /// </summary>
        /// <param name="groupName">Group Name</param>
        /// <returns>Group Object</returns>
        IInventoryGroup GetInventoryGroupByName(string groupName);
    }
}
