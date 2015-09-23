//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CorporationSheetResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Result tag for the EVE Corporation Sheet API
    /// </summary>
    [XmlRoot("result")]
    public class CorporationSheetResult : EveApiResult<CorporationSheetRowset, CorporationSheetRow>
    {
        /// <summary>
        /// Gets or sets the Corporation Id
        /// </summary>
        [XmlElement("corporationID")]
        public int CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Name
        /// </summary>
        [XmlElement("corporationName")]
        public string CorporationName { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Ticker
        /// </summary>
        [XmlElement("ticker")]
        public string Ticker { get; set; }

        /// <summary>
        /// Gets or sets the Character Id of the Corporation CEO
        /// </summary>
        [XmlElement("ceoID")]
        public int CeoId { get; set; }

        /// <summary>
        /// Gets or sets the name of the Corporation CEO
        /// </summary>
        [XmlElement("ceoName")]
        public string CeoName { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Home Station of the Corporation
        /// </summary>
        [XmlElement("stationID")]
        public int StationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the home station for the Corporation
        /// </summary>
        [XmlElement("stationName")]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Description
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Corporation URL
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Alliance the Corporation is a part of.
        /// </summary>
        [XmlElement("allianceID")]
        public int AllianceId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Faction the corporation is a part of.
        /// </summary>
        [XmlElement("factionID")]
        public int FactionId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Alliance the Corporation is a part of.
        /// </summary>
        [XmlElement("allianceName")]
        public string AllianceName { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Tax Rate.
        /// </summary>
        [XmlElement("taxRate")]
        public double TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the number of Members in the Corporation
        /// </summary>
        [XmlElement("memberCount")]
        public int MemberCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Shares in the Corporation
        /// </summary>
        [XmlElement("shares")]
        public long Shares { get; set; }

        /// <summary>
        /// Gets or sets the data required to render the corporation logo.
        /// </summary>
        [XmlElement("logo")]
        public CorporationLogo Logo { get; set; }
    }
}
