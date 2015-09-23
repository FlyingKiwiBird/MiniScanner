//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterEmploymentRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Character Employment Data from the EVE API
    /// </summary>
    [XmlRoot("row")]
    public class CharacterEmploymentRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Record of Employment Id
        /// </summary>
        [XmlAttribute("recordID")]
        public int RecordId { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Employed with.
        /// </summary>
        [XmlAttribute("corporationID")]
        public int CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the Corporation Employed with.
        /// </summary>
        [XmlAttribute("corporationName")]
        public string CorporationName { get; set; }

        /// <summary>
        /// Gets or sets the Start Date the corporation employed this Character from.
        /// </summary>
        [XmlAttribute("startDate")]
        public string StartDate { get; set; }
    }
}
