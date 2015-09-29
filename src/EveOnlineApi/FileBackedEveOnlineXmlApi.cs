//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="FileBackedEveOnlineXmlApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Entities.Xml.Base;
    using EveOnlineApi.Interfaces.Xml;

    /// <summary>
    /// This is an interface to the Eve Online XML API Version 2. All responses are cached
    /// as objects in the filesystem to deter CCP from blocking your IP address from API
    /// calls since if you make too many, they will block you. Cache timers are set in the
    /// response, and tell you when the cache for the particular item you've asked for expire.
    /// The objects returned by this class are serialized XML structures, and are likely
    /// not the most friendly objects to work with. You likely want to work with the non-XML
    /// entities which are also defined in this project.
    /// </summary>
    public class FileBackedEveOnlineXmlApi : IAllianceXmlDataProvider, ICharacterXmlDataProvider, ICorporationXmlDataProvider, ICallListXmlDataProvider
    {
        /// <summary>
        /// Holds the directory where we're setting our cache.
        /// </summary>
        private string cacheDirectory = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBackedEveOnlineXmlApi"/> class.
        /// </summary>
        /// <param name="cacheDirectory">Directory to cache items in</param>
        public FileBackedEveOnlineXmlApi(string cacheDirectory)
        {
            if (!Directory.Exists(cacheDirectory))
            {
                Directory.CreateDirectory(cacheDirectory);
            }

            this.cacheDirectory = cacheDirectory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBackedEveOnlineXmlApi"/> class.
        /// </summary>
        public FileBackedEveOnlineXmlApi()
            : this(".\\ApiCache")
        {
        }

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
            string localCacheDirectory = Path.Combine(this.cacheDirectory, "AllianceList");
            if (!Directory.Exists(localCacheDirectory))
            {
                Directory.CreateDirectory(localCacheDirectory);
            }

            string allianceListFile = string.Empty;
            if (getVersion1Data == true)
            {
                allianceListFile = Path.Combine(localCacheDirectory, "AllianceList-v1.xml");
            }
            else
            {
                allianceListFile = Path.Combine(localCacheDirectory, "AllianceList-v2.xml");
            }

            if (this.NeedNewFile(allianceListFile))
            {
                if (getVersion1Data == true)
                {
                    this.DownloadFile(new Uri("http://api.eveonline.com/eve/AllianceList.xml.aspx?version=1"), allianceListFile);
                }
                else
                {
                    this.DownloadFile(new Uri("http://api.eveonline.com/eve/AllianceList.xml.aspx"), allianceListFile);
                }
            }

            AllianceListApi api = XmlSerialization.DeserializeFile<AllianceListApi>(allianceListFile);

            return api.Result.RowSet.Rows.Where(x => x.AllianceId == allianceId).FirstOrDefault();
        }

        /// <summary>
        /// Gets the Character Id for a particular Character Name
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Character Id</returns>
        public int GetCharacterId(string characterName)
        {
            string localCacheDirectory = Path.Combine(this.cacheDirectory, "CharacterId");
            if (!Directory.Exists(localCacheDirectory))
            {
                Directory.CreateDirectory(localCacheDirectory);
            }

            string characterIdFile = Path.Combine(localCacheDirectory, characterName + ".xml");

            if (this.NeedNewFile(characterIdFile))
            {
                this.DownloadFile(new Uri("https://api.eveonline.com/eve/CharacterID.xml.aspx?names=" + Uri.EscapeUriString(characterName)), characterIdFile);
            }

            CharacterIdApi api = XmlSerialization.DeserializeFile<CharacterIdApi>(characterIdFile);

            return api.Result.RowSet.Rows[0].CharacterId;
        }

        /// <summary>
        /// Gets the Character Information for a particular Character Id.
        /// </summary>
        /// <param name="characterId">Character Id</param>
        /// <returns>Character Info XML Object</returns>
        public CharacterInfoApi GetCharacterInfo(int characterId)
        {
            string localCacheDirectory = Path.Combine(this.cacheDirectory, "CharacterInfo");
            if (!Directory.Exists(localCacheDirectory))
            {
                Directory.CreateDirectory(localCacheDirectory);
            }

            string characterInfoFile = Path.Combine(localCacheDirectory, characterId.ToString(CultureInfo.InvariantCulture) + ".xml");

            if (this.NeedNewFile(characterInfoFile))
            {
                this.DownloadFile(new Uri("https://api.eveonline.com/eve/CharacterInfo.xml.aspx?characterID=" + characterId.ToString(CultureInfo.InvariantCulture)), characterInfoFile);
            }

            CharacterInfoApi api = XmlSerialization.DeserializeFile<CharacterInfoApi>(characterInfoFile);

            return api;
        }

        /// <summary>
        /// Gets the Corporation Sheet Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>CorporationSheet XML Object</returns>
        public CorporationSheetApi GetCorporationInfo(int corporationId)
        {
            string localCacheDirectory = Path.Combine(this.cacheDirectory, "CorporationInfo");
            if (!Directory.Exists(localCacheDirectory))
            {
                Directory.CreateDirectory(localCacheDirectory);
            }

            string corporationSheetFile = Path.Combine(localCacheDirectory, corporationId.ToString(CultureInfo.InvariantCulture) + ".xml");

            if (this.NeedNewFile(corporationSheetFile))
            {
                this.DownloadFile(new Uri("https://api.eveonline.com/corp/CorporationSheet.xml.aspx?corporationID=" + corporationId.ToString(CultureInfo.InvariantCulture)), corporationSheetFile);
            }

            CorporationSheetApi api = XmlSerialization.DeserializeFile<CorporationSheetApi>(corporationSheetFile);

            return api;
        }

        /// <summary>
        /// Gets the Call List with Permissions Listed as Well
        /// </summary>
        /// <returns>CallList XML Object</returns>
        public CallListApi GetCallList()
        {
            string localCacheDirectory = Path.Combine(this.cacheDirectory, "CallList");
            if (!Directory.Exists(localCacheDirectory))
            {
                Directory.CreateDirectory(localCacheDirectory);
            }

            string localCacheFile = Path.Combine(localCacheDirectory, "CallList.xml");

            if (this.NeedNewFile(localCacheFile))
            {
                this.DownloadFile(new Uri("https://api.eveonline.com/API/CallList.xml.aspx"), localCacheFile);
            }

            CallListApi api = XmlSerialization.DeserializeFile<CallListApi>(localCacheFile);

            return api;
        }

        /// <summary>
        /// Helper function to determine if we need to download a new file.
        /// </summary>
        /// <param name="path">Path to the File</param>
        /// <returns>True if file doesn't exist, or cache is expired.</returns>
        private bool NeedNewFile(string path)
        {
            if (!File.Exists(path))
            {
                return true;
            }

            EveApi api = XmlSerialization.DeserializeFile<EveApi>(path);
            if (DateTime.UtcNow > DateTime.Parse(api.CachedUntil + "Z", CultureInfo.InvariantCulture).ToUniversalTime())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Downloads a file from a given URI to a specific path.
        /// </summary>
        /// <param name="uri">URI to download from.</param>
        /// <param name="path">Path to write file to.</param>
        private void DownloadFile(Uri uri, string path)
        {
            using (WebClient cli = new WebClient())
            {
                cli.DownloadFile(uri, path);
            }
        }
    }
}
