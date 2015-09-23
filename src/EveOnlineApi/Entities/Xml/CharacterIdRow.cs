//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterIdRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Character to Id Data from the Eve API
    /// </summary>
    [XmlRoot("row")]
    public class CharacterIdRow : EveRow
    {
        /// <summary>
        /// Gets or sets the Character Name
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Character Id
        /// </summary>
        [XmlAttribute("characterID")]
        public int CharacterId { get; set; }
    }
}
