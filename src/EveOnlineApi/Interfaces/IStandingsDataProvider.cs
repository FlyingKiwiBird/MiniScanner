//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IStandingsDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using EveOnlineApi.Entities;

    /// <summary>
    /// Data Provider for Eve Online Standings Data
    /// </summary>
    public interface IStandingsDataProvider
    {
        /// <summary>
        /// Gets Standings for a given Entity
        /// </summary>
        /// <param name="entityName">Name of Entity</param>
        /// <param name="entityType">Type of Entity</param>
        /// <returns>Standings Information</returns>
        Standings GetStandingsInfo(string entityName, EntityType entityType);
    }
}
