//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterInfoResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Result tag for the EVE Character Info API
    /// </summary>
    [XmlRoot("result")]
    public class CharacterInfoResult : EveApiResult<CharacterEmploymentRowset, CharacterEmploymentRow>
    {
        /// <summary>
        /// Gets or sets the Character Id
        /// </summary>
        [XmlElement("characterID")]
        public int CharacterId { get; set; }

        /// <summary>
        /// Gets or sets the Character Name
        /// </summary>
        [XmlElement("characterName")]
        public string CharacterName { get; set; }

        /// <summary>
        /// Gets or sets the Character Race
        /// </summary>
        [XmlElement("race")]
        public string Race { get; set; }

        /// <summary>
        /// Gets or sets the Character Bloodline Id
        /// </summary>
        [XmlElement("bloodlineID")]
        public int BloodlineId { get; set; }

        /// <summary>
        /// Gets or sets the Character Bloodline
        /// </summary>
        [XmlElement("bloodline")]
        public string Bloodline { get; set; }

        /// <summary>
        /// Gets or sets the Character Ancestry Id
        /// </summary>
        [XmlElement("ancestryID")]
        public int AncestryId { get; set; }

        /// <summary>
        /// Gets or sets the Character Ancestry
        /// </summary>
        [XmlElement("ancestry")]
        public string Ancestry { get; set; }

        /// <summary>
        /// Gets or sets the Character's Corporation Id
        /// </summary>
        [XmlElement("corporationID")]
        public int CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the Character's Corporation
        /// </summary>
        [XmlElement("corporation")]
        public string Corporation { get; set; }

        /// <summary>
        /// Gets or sets the date the Character joined the Corporation
        /// </summary>
        [XmlElement("corporationDate")]
        public string CorporationDate { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Id the Character is associated with.
        /// </summary>
        [XmlElement("allianceID")]
        public int AllianceId { get; set; }

        /// <summary>
        /// Gets or sets the Alliance the Character is associated with.
        /// </summary>
        [XmlElement("alliance")]
        public string Alliance { get; set; }

        /// <summary>
        /// Gets or sets the date the character's corporation joined the alliance.
        /// </summary>
        [XmlElement("allianceDate")]
        public string AllianceDate { get; set; }

        /// <summary>
        /// Gets or sets the Character's security status.
        /// </summary>
        [XmlElement("securityStatus")]
        public double SecurityStatus { get; set; }
    }
}
