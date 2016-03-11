//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICorporationDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Defines the interface for retrieving Corporation data.
    /// </summary>
    public interface ICorporationDataProvider
    {
        /// <summary>
        /// Gets the Corporation Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>Corporation Object</returns>
        ICorporation GetCorporationInfo(int corporationId);
    }
}
