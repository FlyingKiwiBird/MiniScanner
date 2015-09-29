//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CallRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents a Call List Row.
    /// </summary>
    [XmlRoot("row")]
    public class CallRow : EveRow
    {
        /// <summary>
        /// Gets or sets the required access mask for the API key needed to call this API call.
        /// </summary>
        [XmlAttribute("accessMask")]
        public int AccessMask { get; set; }

        /// <summary>
        /// Gets or sets the type of API call.
        /// </summary>
        [XmlAttribute("type")]
        public string CallType { get; set; }

        /// <summary>
        /// Gets or sets the name of the API call.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Group this API call is a part of.
        /// </summary>
        [XmlAttribute("groupID")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a description of the API call.
        /// </summary>
        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
