//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterEmploymentRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Row Set for the Character Employment History. Part of Character Info lookup.
    /// </summary>
    [XmlRoot("rowset")]
    public class CharacterEmploymentRowset : EveApiRowset<CharacterEmploymentRow>
    {
    }
}
