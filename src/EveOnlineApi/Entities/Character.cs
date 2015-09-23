//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Character.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System.Collections.Generic;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Interfaces;
    using System.Text;

    /// <summary>
    /// Represents the data exposed about an Eve Online character.
    /// </summary>
    public class Character : EveOnlineCacheable
    {
        /// <summary>
        /// Holds the lazy loaded Corporation object.
        /// </summary>
        private Corporation corporation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="apiResult">Xml API Result</param>
        public Character(CharacterInfoApi apiResult) 
            : base(apiResult)
        {
            this.Id = apiResult.Result.CharacterId;
            this.Name = apiResult.Result.CharacterName;
            this.CorporationId = apiResult.Result.CorporationId;
            this.SecurityStatus = apiResult.Result.SecurityStatus;

            this.EmploymentHistory = EveOnlineApi.Entities.EmploymentHistoryEntry.CreateEmploymentHistory(apiResult.Result.RowSet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="id">Character Id</param>
        /// <param name="name">Character Name</param>
        /// <param name="corporationId">Character Corporation Id</param>
        /// <param name="securityStatus">Character Security Status</param>
        /// <param name="employmentHistory">Character Employment History</param>
        /// <param name="apiVersion">Eve XML API Version</param>
        /// <param name="currentTime">Time XML API was Queried</param>
        /// <param name="cachedUntil">Time Object Cache Expires</param>
        public Character(int id, string name, int corporationId, double securityStatus, IEnumerable<EmploymentHistoryEntry> employmentHistory, int apiVersion, string currentTime, string cachedUntil)
            : base(apiVersion, currentTime, cachedUntil)
        {
            this.Id = id;
            this.Name = name;
            this.CorporationId = corporationId;
            this.SecurityStatus = securityStatus;
            this.EmploymentHistory = employmentHistory;

            this.AlliancePopulated = false;
        }
        
        /// <summary>
        /// Gets the Character Id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the Character Name;
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Character's Corporation Id.
        /// </summary>
        public int CorporationId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether we pulled corp/alliance data.
        /// </summary>
        public bool AlliancePopulated { get; private set; }

        /// <summary>
        /// Gets the Corporation associated with the Character.
        /// </summary>
        public Corporation Corporation 
        {
            get
            {
                if (this.corporation == null)
                {
                    this.corporation = EveOnlineApi.Entities.Corporation.GetCorporationByCorporationId(this.CorporationId);
                }

                return this.corporation;
            }
        }

        /// <summary>
        /// Gets the character's security status.
        /// </summary>
        public double SecurityStatus { get; private set; }

        /// <summary>
        /// Gets the character's employment history.
        /// </summary>
        public IEnumerable<EmploymentHistoryEntry> EmploymentHistory { get; private set; }

        /// <summary>
        /// Gets the Character data for a particular Character Id.
        /// </summary>
        /// <param name="characterId">Character Id to lookup.</param>
        /// <returns>Character object.</returns>
        public static Character GetCharacterByCharacterId(int characterId)
        {
            ICharacterDataProvider cdp = Injector.Resolve<ICharacterDataProvider>();
            return cdp.GetCharacterInfo(characterId);
        }

        /// <summary>
        /// Gets the Character data for a particular Character Name.
        /// </summary>
        /// <param name="characterName">Character Name to lookup.</param>
        /// <returns>Character object.</returns>
        public static Character GetCharacterByCharacterName(string characterName)
        {
            ICharacterDataProvider cdp = Injector.Resolve<ICharacterDataProvider>();
            int characterId = cdp.GetCharacterId(characterName);
            return cdp.GetCharacterInfo(characterId);
        }

        /// <summary>
        /// Forces the object to populate data down to the alliance level.
        /// </summary>
        public void PopulateCorpAllianceData()
        {
            if (this.AlliancePopulated)
            {
                return;
            }

            if (this.CorporationId > 0)
            {
                if (this.Corporation.Id > 0)
                {
                    if (this.corporation.AllianceId > 0)
                    {
                        if (this.corporation.Alliance.Id > 0)
                        {
                            this.AlliancePopulated = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns a String that represents the current Character.
        /// </summary>
        /// <returns>String describing Character</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Name);
            if (this.corporation != null)
            {
                sb.Append(" [");
                sb.Append(this.corporation.Ticker);
                sb.Append("]");

                if (this.corporation.Alliance != null)
                {
                    sb.Append("[");
                    sb.Append(this.corporation.Alliance.ShortName);
                    sb.Append("]");
                }
            }

            return sb.ToString();
        }
    }
}
