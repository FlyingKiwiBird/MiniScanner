//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveOnlineXmlApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Interfaces.Xml;

    /// <summary>
    /// Provides an interface to the EVE Online XML API
    /// </summary>
    public class EveOnlineXmlApi : IAllianceXmlDataProvider, ICharacterXmlDataProvider, ICorporationXmlDataProvider, ICallListXmlDataProvider
    {
        /// <summary>
        /// Gets alliance information from the XML API without Member Corp data.
        /// This is about 500kb of data. Don't call it THAT often if you can avoid it.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <returns>Alliance Row XML Object</returns>
        public AllianceRow GetAllianceData(int allianceId)
        {
            return this.GetAllianceData(allianceId, true);
        }

        /// <summary>
        /// Gets alliance information from the XML API. If you set getVersion1Data to false
        /// this will download a 1.8MB XML file once an hour with all the member corps.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <param name="getVersion1Data">Suppress Member Corps from Alliance Data</param>
        /// <returns>Alliance Row XML Object</returns>
        public AllianceRow GetAllianceData(int allianceId, bool getVersion1Data)
        {
            string url = "http://api.eveonline.com/eve/AllianceList.xml.aspx" + (getVersion1Data ? "?version=1" : string.Empty);
            Uri uri = new Uri(url);
            AllianceListApi api = this.DownloadAndDeserialize<AllianceListApi>(uri);
            return api.Result.RowSet.Rows.Where(x => x.AllianceId == allianceId).FirstOrDefault();
        }

        /// <summary>
        /// Gets the Call List with Permissions Listed as Well
        /// </summary>
        /// <returns>CallList XML Object</returns>
        public CallListApi GetCallList()
        {
            string url = "https://api.eveonline.com/API/CallList.xml.aspx";
            Uri uri = new Uri(url);
            CallListApi api = this.DownloadAndDeserialize<CallListApi>(uri);
            return api;
        }

        /// <summary>
        /// Gets the Character Id for a particular Character Name
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Character Id</returns>
        public int GetCharacterId(string characterName)
        {
            string url = string.Format(CultureInfo.InvariantCulture, "https://api.eveonline.com/eve/CharacterID.xml.aspx?names={0}", characterName);
            Uri uri = new Uri(url);
            CharacterIdApi api = this.DownloadAndDeserialize<CharacterIdApi>(uri);
            return api.Result.RowSet.Rows.First().CharacterId;
        }

        /// <summary>
        /// Gets the Character Information for a particular Character Id.
        /// </summary>
        /// <param name="characterId">Character Id</param>
        /// <returns>Character Info XML Object</returns>
        public CharacterInfoApi GetCharacterInfo(int characterId)
        {
            string url = string.Format(CultureInfo.InvariantCulture, "https://api.eveonline.com/eve/CharacterInfo.xml.aspx?characterID={0}", characterId);
            Uri uri = new Uri(url);
            CharacterInfoApi api = this.DownloadAndDeserialize<CharacterInfoApi>(uri);
            return api;
        }

        /// <summary>
        /// Gets the Corporation Sheet Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>CorporationSheet XML Object</returns>
        public CorporationSheetApi GetCorporationInfo(int corporationId)
        {
            string url = string.Format(CultureInfo.InvariantCulture, "https://api.eveonline.com/corp/CorporationSheet.xml.aspx?corporationID={0}", corporationId);
            Uri uri = new Uri(url);
            CorporationSheetApi api = this.DownloadAndDeserialize<CorporationSheetApi>(uri);
            return api;
        }

        /// <summary>
        /// Retrieves and deserializes data from the internet.
        /// </summary>
        /// <typeparam name="T">Type of object to output</typeparam>
        /// <param name="uri">URI to download</param>
        /// <returns>Deserialized object.</returns>
        private T DownloadAndDeserialize<T>(Uri uri)
        {
            using (WebClient cli = new WebClient())
            {
                using (Stream stream = cli.OpenRead(uri))
                {
                    T api = XmlSerialization.DeserializeStream<T>(stream);
                    return api;
                }
            }
        }
    }
}
