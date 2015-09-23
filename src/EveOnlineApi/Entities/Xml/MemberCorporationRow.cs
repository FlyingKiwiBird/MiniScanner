//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MemberCorporationRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents a row linking the member corporation to the alliance and the date on which they joined the alliance.
    /// </summary>
    [XmlRoot("row")]
    public class MemberCorporationRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Corporation Id
        /// </summary>
        [XmlAttribute("corporationID")]
        public int CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the date of the start of the relationship.
        /// </summary>
        [XmlAttribute("startDate")]
        public string StartDate { get; set; }
    }
}
