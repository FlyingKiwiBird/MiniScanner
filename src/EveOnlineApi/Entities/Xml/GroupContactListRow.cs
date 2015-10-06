//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="GroupContactListRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents a Contact List Row
    /// </summary>
    [XmlRoot("row")]
    public class GroupContactListRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Contact Id.
        /// </summary>
        [XmlAttribute("contactID")]
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the Contact Name.
        /// </summary>
        [XmlAttribute("contactName")]
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the Standing
        /// </summary>
        [XmlAttribute("standing")]
        public decimal Standing { get; set; }

        /// <summary>
        /// Gets or sets the Contact Entity Type Id.
        /// </summary>
        [XmlAttribute("contactTypeID")]
        public int ContactTypeId { get; set; }

        /// <summary>
        /// Gets or sets the mask which is used with Contact Labels
        /// </summary>
        [XmlAttribute("labelMask")]
        public int LabelMask { get; set; }
    }
}
