//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Corporation.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Interfaces;

    using EveScanner.IoC;

    /// <summary>
    /// Represents an EVE Online Corporation
    /// </summary>
    public class Corporation : EveOnlineCacheable
    {
        /// <summary>
        /// Holds the lazy loaded Alliance object.
        /// </summary>
        private Alliance alliance = null;

        /// <summary>
        /// Holds the lazy loaded CEO Character object.
        /// </summary>
        private Character ceoCharacter = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Corporation"/> class.
        /// </summary>
        /// <param name="apiResult">Xml API Result</param>
        public Corporation(CorporationSheetApi apiResult)
            : base(apiResult)
        {
            if (apiResult == null)
            {
                throw new ArgumentException("API Result cannot be null.", "apiResult");
            }

            this.Id = apiResult.Result.CorporationId;
            this.Name = apiResult.Result.CorporationName;
            this.Ticker = apiResult.Result.Ticker;
            this.CeoCharacterId = apiResult.Result.CeoId;
            this.HomeStationId = apiResult.Result.StationId;
            this.Description = apiResult.Result.Description;
            this.Url = apiResult.Result.Url;
            this.AllianceId = apiResult.Result.AllianceId;
            this.FactionId = apiResult.Result.FactionId;
            this.TaxRate = apiResult.Result.TaxRate;
            this.MemberCount = apiResult.Result.MemberCount;
            this.Shares = apiResult.Result.Shares;
            this.Logo = new CorporationLogo(apiResult.Result.Logo.GraphicId, apiResult.Result.Logo.Shape1, apiResult.Result.Logo.Shape2, apiResult.Result.Logo.Shape3, apiResult.Result.Logo.Color1, apiResult.Result.Logo.Color2, apiResult.Result.Logo.Color3);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Corporation"/> class.
        /// </summary>
        /// <param name="id">Corporation Id</param>
        /// <param name="name">Corporation Name</param>
        /// <param name="ticker">Corporation Ticket</param>
        /// <param name="ceoCharacterId">Corporation CEO Character Id</param>
        /// <param name="homeStationId">Corporation Home Station Id</param>
        /// <param name="description">Corporation Description</param>
        /// <param name="url">Corporation URL</param>
        /// <param name="allianceId">Corporation Alliance Id</param>
        /// <param name="factionId">Corporation Faction Id</param>
        /// <param name="taxRate">Corporation Tax Rate</param>
        /// <param name="memberCount">Corporation Member Count</param>
        /// <param name="shares">Corporation Share Count</param>
        /// <param name="logoGraphicId">Corporation Logo Graphic Id</param>
        /// <param name="logoShape1">Corporation Logo Shape 1</param>
        /// <param name="logoShape2">Corporation Logo Shape 2</param>
        /// <param name="logoShape3">Corporation Logo Shape 3</param>
        /// <param name="logoColor1">Corporation Logo Color 1</param>
        /// <param name="logoColor2">Corporation Logo Color 2</param>
        /// <param name="logoColor3">Corporation Logo Color 3</param>
        /// <param name="apiVersion">Eve XML API Version</param>
        /// <param name="currentTime">Time XML API was Queried</param>
        /// <param name="cachedUntil">Time Object Cache Expires</param>
        public Corporation(int id, string name, string ticker, int ceoCharacterId, int homeStationId, string description, string url, int allianceId, int factionId, int taxRate, int memberCount, int shares, int logoGraphicId, int logoShape1, int logoShape2, int logoShape3,  int logoColor1, int logoColor2, int logoColor3, int apiVersion, string currentTime, string cachedUntil)
            : base(apiVersion, currentTime, cachedUntil)
        {
            this.Id = id;
            this.Name = name;
            this.Ticker = ticker;
            this.CeoCharacterId = ceoCharacterId;
            this.HomeStationId = homeStationId;
            this.Description = description;
            this.Url = url;
            this.AllianceId = allianceId;
            this.FactionId = factionId;
            this.TaxRate = taxRate;
            this.MemberCount = memberCount;
            this.Shares = shares;
            this.Logo = new CorporationLogo(logoGraphicId, logoShape1, logoShape2, logoShape3, logoColor1, logoColor2, logoColor3);
        }

        /// <summary>
        /// Gets the Corporation Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the Corporation Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Corporation Ticket
        /// </summary>
        public string Ticker { get; private set; }

        /// <summary>
        /// Gets the Character Id for the Corporation CEO
        /// </summary>
        public int CeoCharacterId { get; private set; }

        /// <summary>
        /// Gets the Character Object for the CEO.
        /// </summary>
        public Character CeoCharacter
        {
            get
            {
                if (this.ceoCharacter == null)
                {
                    this.ceoCharacter = Character.GetCharacterByCharacterId(this.CeoCharacterId);
                }

                return this.ceoCharacter;
            }
        }

        /// <summary>
        /// Gets the Station Id for the Corporation Home Station
        /// </summary>
        public int HomeStationId { get; private set; }

        /// <summary>
        /// Gets the Corporation Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the Corporation URL
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the Corporation Alliance Id
        /// </summary>
        public int AllianceId { get; private set; }

        /// <summary>
        /// Gets the Alliance object associated with the Corporation
        /// </summary>
        public Alliance Alliance
        {
            get
            {
                if (this.AllianceId == 0)
                {
                    return null;
                }

                if (this.alliance == null)
                {
                    this.alliance = EveOnlineApi.Entities.Alliance.GetAllianceByAllianceId(this.AllianceId);
                }

                return this.alliance;
            }
        }

        /// <summary>
        /// Gets the FactionId the Corporation is associated with.
        /// </summary>
        public int FactionId { get; private set; }

        /// <summary>
        /// Gets the Tax Rate for the Corporation;
        /// </summary>
        public double TaxRate { get; private set; }

        /// <summary>
        /// Gets the number of members in the Corporation;
        /// </summary>
        public int MemberCount { get; private set; }

        /// <summary>
        /// Gets the number of shares assigned from the corporation,
        /// </summary>
        public long Shares { get; private set; }

        /// <summary>
        /// Gets information used to render the corporation logo.
        /// </summary>
        public CorporationLogo Logo { get; private set; }

        /// <summary>
        /// Gets corporation information for a particular corporation id.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>Corporation Object</returns>
        public static Corporation GetCorporationByCorporationId(int corporationId)
        {
            ICorporationDataProvider cdp = Injector.Create<ICorporationDataProvider>();
            return cdp.GetCorporationInfo(corporationId);
        }
    }
}
