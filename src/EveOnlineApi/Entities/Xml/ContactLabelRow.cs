//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ContactLabelRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents an EVE Online Contact Label Row.
    /// </summary>
    [XmlRoot("row")]
    public class ContactLabelRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Contact Label Id.
        /// </summary>
        [XmlAttribute("labelID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Contact Label Name
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
