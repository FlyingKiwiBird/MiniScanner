//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="PersonalContactListRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents a Personal Contact Row - Same as the Group one with a Watch list parameter
    /// </summary>
    [XmlRoot("row")]
    public class PersonalContactListRow : GroupContactListRow
    {
        /// <summary>
        /// Gets or sets a value indicating if the contact is on a watch list.
        /// </summary>
        [XmlAttribute("inWatchlist")]
        public string InWatchlist { get; set; }
    }
}
