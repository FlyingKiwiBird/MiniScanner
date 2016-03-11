//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IAllianceXmlDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces.Xml
{
    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines the interface for retrieving Eve XML API data for Alliances.
    /// </summary>
    public interface IAllianceXmlDataProvider
    {
        /// <summary>
        /// Gets alliance information from the XML API without Member Corp data.
        /// This is about 500kb of data. Don't call it THAT often if you can avoid it.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <returns>Alliance Row XML Object</returns>
        AllianceRow GetAllianceData(int allianceId);

        /// <summary>
        /// Gets alliance information from the XML API. If you set getVersion1Data to false
        /// this will download a 1.8MB XML file once an hour with all the member corps.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <param name="getVersion1Data">Suppress Member Corps from Alliance Data</param>
        /// <returns>Alliance Row XML Object</returns>
        AllianceRow GetAllianceData(int allianceId, bool getVersion1Data);
    }
}
