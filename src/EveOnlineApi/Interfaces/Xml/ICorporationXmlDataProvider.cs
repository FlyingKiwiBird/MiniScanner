//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICorporationXmlDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces.Xml
{
    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines the interface for retrieving Eve XML API data for Corporations.
    /// </summary>
    public interface ICorporationXmlDataProvider
    {
        /// <summary>
        /// Gets the Corporation Sheet Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>CorporationSheet XML Object</returns>
        CorporationSheetApi GetCorporationInfo(int corporationId);
    }
}
