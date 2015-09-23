//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CallGroupRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents the Call Group row under the
    /// EVE Call List API XML call.
    /// </summary>
    [XmlRoot("row")]
    public class CallGroupRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Group Id
        /// </summary>
        [XmlAttribute("groupID")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Group Name
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Group Description
        /// </summary>
        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
