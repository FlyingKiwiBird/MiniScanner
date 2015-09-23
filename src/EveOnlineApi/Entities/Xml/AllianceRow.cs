//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="AllianceRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Alliance Data from the EVE API
    /// </summary>
    [XmlRoot("row")]
    public class AllianceRow : EveRow
    {
        /// <summary>
        /// Gets or sets the name of the Alliance.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ticker for the Alliance.
        /// </summary>
        [XmlAttribute("shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Id.
        /// </summary>
        [XmlAttribute("allianceID")]
        public int AllianceId { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Id for the Executor Corporation.
        /// </summary>
        [XmlAttribute("executorCorpID")]
        public int ExecutorCorpId { get; set; }

        /// <summary>
        /// Gets or sets the member count for the Alliance.
        /// </summary>
        [XmlAttribute("memberCount")]
        public int MemberCount { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the Alliance.
        /// </summary>
        [XmlAttribute("startDate")]
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the row set containing Alliance Member Corporations.
        /// </summary>
        [XmlElement("rowset")]
        public MemberCorporationsRowset MemberCorporations { get; set; }
    }
}
